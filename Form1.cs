using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ufa_nipi
{
    public partial class Form1 : Form
    {
        static List<Line> lines = new List<Line>(); // список отрезков
        private BufferedGraphics z; // для ручного управления двойной буферезацией, чтобы не было мерцания
        private bool isDrawLine = false; // флаг - нарисованы ли отрезки? true - нарисованы
        private bool isDrawRectangle = false; // флаг - нарисован ли квадрат? true - нарисован

        private int sizeRectangle = 100; // первоночальный размер квадрата

        public Form1()
        {
            InitializeComponent();
            InitializeGraphics(); 
        }

        /// <summary>
        /// Инициализация графического буфера, задаем нужные параметры
        /// </summary>
        private void InitializeGraphics()
        {
            BufferedGraphicsContext context = new BufferedGraphicsContext();
            z = context.Allocate(Graphics.FromHwnd(panelPaint.Handle), ClientRectangle);
            context.MaximumBuffer = ClientRectangle.Size;
        }

        #region Timer_Tick - отрисовка отрезков и квадрата
        /// <summary>
        /// Происходит отрисовка отрезков и квадрата, если отрезок внутри или пересекает квдарат, то отрезок закрашивается
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            z.Graphics.Clear(BackColor); // очищаем панель для рисования
            Graphics g = z.Graphics;

            Pen redPen = new Pen(Color.Red);
            Pen blackPen = new Pen(Color.Black);
            Pen bluePen = new Pen(Color.Blue, 2);

            Point localPosition = panelPaint.PointToClient(Cursor.Position); // получаем координаты мышки относительно панели для рисования

            if (isDrawRectangle) // рисуем квадрат, вершина квадрта совпадает с курсором
            {
                g.DrawRectangle(redPen, localPosition.X, localPosition.Y, sizeRectangle, sizeRectangle);
            }

            if (!isDrawRectangle) // рисуем отрезки, черным цветом, пока нет квадрата
            {
                foreach (var line in lines) g.DrawLine(blackPen, line.point1, line.point2);
            }


            if (isDrawLine && isDrawRectangle) // заходим, если отрезки и квадрат нарисованы
            {
                foreach (var line in lines) // проходимся по отрезкам
                {
                    //если одна из двух точек отрезка находится внутри квадрата, то закрашиваем отрезок синим цветом
                    if (IsInsideRectangle(line.point1, line.point2, new Point(localPosition.X, localPosition.Y), sizeRectangle))
                    {
                        g.DrawLine(bluePen, line.point1, line.point2);
                    }

                    //если отрезок пересекает ВЕРХНЮЮ сторону квадрата, то закрашиваем отрезок синим цветом
                    else if (CrossLines(line.point1, line.point2, new Point(localPosition.X, localPosition.Y), new Point(localPosition.X + sizeRectangle, localPosition.Y)))
                    {
                        g.DrawLine(bluePen, line.point1, line.point2);
                    }

                    //если отрезок пересекает ЛЕВУЮ сторону квадрата, то закрашиваем отрезок синим цветом
                    else if (CrossLines(line.point1, line.point2, new Point(localPosition.X, localPosition.Y), new Point(localPosition.X, localPosition.Y + sizeRectangle)))
                    {
                        g.DrawLine(bluePen, line.point1, line.point2);
                    }

                    //если отрезок пересекает ПРАВУЮ сторону квадрата, то закрашиваем отрезок синим цветом
                    else if (CrossLines(line.point1, line.point2, new Point(localPosition.X + sizeRectangle, localPosition.Y), new Point(localPosition.X + sizeRectangle, localPosition.Y + sizeRectangle)))
                    {
                        g.DrawLine(bluePen, line.point1, line.point2);
                    }

                    //если отрезок пересекает НИЖНЮЮ сторону квадрата, то закрашиваем отрезок синим цветом
                    else if (CrossLines(line.point1, line.point2, new Point(localPosition.X, localPosition.Y + sizeRectangle), new Point(localPosition.X + sizeRectangle, localPosition.Y + sizeRectangle)))
                    {
                        g.DrawLine(bluePen, line.point1, line.point2);
                    }

                    // иначе закрашиваем черным
                    else g.DrawLine(blackPen, line.point1, line.point2);
                }
            }

            z.Render(); //выводим то, что отрисовано в буфере 
        }
        #endregion

        #region RandomPoints - функция создания случайных отрезков 
        /// <summary>
        /// Создает список с координатами последовательно соединенных отрезков случайным образом
        /// </summary>
        /// <param name="n">Количество отрезков</param>
        /// <returns></returns>
        private List<Line> RandomPoints(int n)
        {
            List<Line> lines = new List<Line>();

            Random rand = new Random();

            int memoryX = -1; // для сохранения координаты X, чтобы она была первой точкой следующего отрезка
            int memoryY = -1; // тоже самое для Y

            for (int i = 0; i < n; i++)
            {
                int x1, y1;

                if (memoryX == -1)
                {
                    x1 = rand.Next(0, 630);
                    y1 = rand.Next(0, 500);
                }
                else
                {
                    x1 = memoryX;
                    y1 = memoryY;
                }

                int x2 = rand.Next(0, 630);
                int y2 = rand.Next(0, 500);

                Line line = new Line
                {
                    point1 = new Point(x1, y1),
                    point2 = new Point(x2, y2)
                };

                lines.Add(line);

                memoryX = x2;
                memoryY = y2;
            }

            return lines;
        }
        #endregion

        #region CrossLines - проверка на пересечение отрезка с другим отрезком
        /// <summary>
        /// Проверяет пересекаются ли отрезки методом векторного произведения
        /// </summary>
        /// <param name="linePoint1">Начальная точка отрезка(x1,y1)</param>
        /// <param name="linePoint2">Конечная точка отрезка(x2,y2)</param>
        /// <param name="rectPoint1">Начальная точка стороны квадрата(x3,y3)</param>
        /// <param name="rectPoint2">Конечная точка стороны квадрата(x4,y4)</param>
        /// <returns></returns>
        private bool CrossLines(Point linePoint1, Point linePoint2, Point rectPoint1, Point rectPoint2)
        {
            int x1 = linePoint1.X;
            int y1 = linePoint1.Y;
            int x2 = linePoint2.X;
            int y2 = linePoint2.Y;

            int x3 = rectPoint1.X;
            int y3 = rectPoint1.Y;
            int x4 = rectPoint2.X;
            int y4 = rectPoint2.Y;

            long v1 = VektorMulti(x4 - x3, y4 - y3, x1 - x3, y1 - y3);
            long v2 = VektorMulti(x4 - x3, y4 - y3, x2 - x3, y2 - y3);
            long v3 = VektorMulti(x2 - x1, y2 - y1, x3 - x1, y3 - y1);
            long v4 = VektorMulti(x2 - x1, y2 - y1, x4 - x1, y4 - y1);

            if (v1 * v2 < 0 && v3 * v4 < 0) return true;
            else return false;
        }

        /// <summary>
        /// Перемножение векторов a*b
        /// </summary>
        /// <param name="ax">Координата "х" вектора "a"</param>
        /// <param name="ay">Координата "y" вектора "a"</param>
        /// <param name="bx">Координата "х" вектора "b"</param>
        /// <param name="by">Координата "y" вектора "b"</param>
        /// <returns></returns>
        private int VektorMulti(int ax, int ay, int bx, int by)
        {
            return ax * by - bx * ay;
        }
        #endregion

        #region IsInsideRectangle - проверка на попадание отрезка хотя бы с одной из двух точек внутрь квадрата
        /// <summary>
        /// Проверяет попадает ли отрезок хотя бы с одной из двух точек внутрь квадрата
        /// </summary>
        /// <param name="linePoint1">Начальная точка отрезка</param>
        /// <param name="linePoint2">Конечная точка отреза</param>
        /// <param name="rectPoint">Верхняя левая вершина квадрата</param>
        /// <param name="size">Длина стороны квадрата</param>
        /// <returns></returns>
        private bool IsInsideRectangle(Point linePoint1, Point linePoint2, Point rectPoint, int size)
        {
            int x1 = linePoint1.X;
            int y1 = linePoint1.Y;
            int x2 = linePoint2.X;
            int y2 = linePoint2.Y;

            int x3 = rectPoint.X;
            int y3 = rectPoint.Y;

            if (IsPointInsideArea(x1, x3, size) && IsPointInsideArea(y1, y3, size)) return true; // попадает ли начальная точка внутрь квадрата
            else if (IsPointInsideArea(x2, x3, size) && IsPointInsideArea(y2, y3, size)) return true; // попадает ли конечная точка внутрь квадрата
            return false;
        }

        /// <summary>
        /// Проверяет входит ли число "а" в отрезок [b;b+size]
        /// </summary>
        /// <param name="a">Проверяемое число</param>
        /// <param name="b">Начало отрезка</param>
        /// <param name="size">Длина отрезка</param>
        /// <returns></returns>
        private bool IsPointInsideArea(int a, int b, int size)
        {
            if (a >= b && a <= b + size) return true;
            else return false;
        }
        #endregion

        #region Buttons - "Нарисовать отрезки", "Нарисовать квадрат", кнопка мышки
        /// <summary>
        /// Кнопка "Нарисовать отрезки случайным образом".
        /// Рисует последовательо соединенные орезки случайным образом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDrawRandomLine_Click(object sender, EventArgs e)
        {
            lines = RandomPoints(int.Parse(txtAmountPoint.Text));
            isDrawLine = true;
        }

        /// <summary>
        /// Кнопка "Нарисовать квадрат"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDrawRectangle_Click(object sender, EventArgs e)
        {
            isDrawRectangle = !isDrawRectangle;
            if (isDrawRectangle) btDrawRectangle.BackColor = Color.Green;
            else btDrawRectangle.BackColor = Color.Red;
        }

        /// <summary>
        /// Вызывается когда происходит событие "Нажатие кнопки мышки". Увеличивает/уменьшает размер квадрата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelPaint_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                sizeRectangle += 10;
            }
            else if(e.Button == MouseButtons.Middle)
            {
                sizeRectangle -= 10;
            }
        }
        #endregion

        /// <summary>
        /// Проверяет textBox - "Количество отрезков" на корректность данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAmountPoint_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtAmountPoint.Text, out int res))
            {
                txtAmountPoint.BackColor = Color.White;
                btDrawRandomLine.Enabled = true;
            }
            else
            {
                txtAmountPoint.BackColor = Color.Red;
                btDrawRandomLine.Enabled = false;
            }
        }
    }
}

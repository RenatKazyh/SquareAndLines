namespace Ufa_nipi
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelPaint = new System.Windows.Forms.Panel();
            this.panelControl = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAmountPoint = new System.Windows.Forms.TextBox();
            this.btDrawRectangle = new System.Windows.Forms.Button();
            this.btDrawRandomLine = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPaint
            // 
            this.panelPaint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPaint.Location = new System.Drawing.Point(286, 12);
            this.panelPaint.Name = "panelPaint";
            this.panelPaint.Size = new System.Drawing.Size(765, 500);
            this.panelPaint.TabIndex = 0;
            this.panelPaint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelPaint_MouseClick);
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.label3);
            this.panelControl.Controls.Add(this.label2);
            this.panelControl.Controls.Add(this.label1);
            this.panelControl.Controls.Add(this.txtAmountPoint);
            this.panelControl.Controls.Add(this.btDrawRectangle);
            this.panelControl.Controls.Add(this.btDrawRandomLine);
            this.panelControl.Location = new System.Drawing.Point(12, 12);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(268, 500);
            this.panelControl.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 34);
            this.label3.TabIndex = 7;
            this.label3.Text = "Нажатие на колёсико мышки\r\nуменьшает размер квадрата на 10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 34);
            this.label2.TabIndex = 6;
            this.label2.Text = "Правая кнопка мышки увеличивает\r\nразмер квадрата на 10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Количество отрезков:";
            // 
            // txtAmountPoint
            // 
            this.txtAmountPoint.Location = new System.Drawing.Point(163, 59);
            this.txtAmountPoint.Name = "txtAmountPoint";
            this.txtAmountPoint.Size = new System.Drawing.Size(99, 22);
            this.txtAmountPoint.TabIndex = 4;
            this.txtAmountPoint.Text = "20";
            this.txtAmountPoint.TextChanged += new System.EventHandler(this.txtAmountPoint_TextChanged);
            // 
            // btDrawRectangle
            // 
            this.btDrawRectangle.Location = new System.Drawing.Point(3, 99);
            this.btDrawRectangle.Name = "btDrawRectangle";
            this.btDrawRectangle.Size = new System.Drawing.Size(260, 35);
            this.btDrawRectangle.TabIndex = 2;
            this.btDrawRectangle.Text = "Нарисовать квадрат";
            this.btDrawRectangle.UseVisualStyleBackColor = true;
            this.btDrawRectangle.Click += new System.EventHandler(this.btDrawRectangle_Click);
            // 
            // btDrawRandomLine
            // 
            this.btDrawRandomLine.Location = new System.Drawing.Point(2, 3);
            this.btDrawRandomLine.Name = "btDrawRandomLine";
            this.btDrawRandomLine.Size = new System.Drawing.Size(260, 50);
            this.btDrawRandomLine.TabIndex = 1;
            this.btDrawRandomLine.Text = "Нарисовать отрезки \r\nслучайным образом";
            this.btDrawRandomLine.UseVisualStyleBackColor = true;
            this.btDrawRandomLine.Click += new System.EventHandler(this.btDrawRandomLine_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 529);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.panelPaint);
            this.Name = "Form1";
            this.Text = "Тестовое задание для ИТ-специалистов ";
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPaint;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Button btDrawRectangle;
        private System.Windows.Forms.Button btDrawRandomLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAmountPoint;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}


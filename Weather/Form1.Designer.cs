namespace Weather
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
            this.prognoz = new System.Windows.Forms.Button();
            this.city = new System.Windows.Forms.TextBox();
            this.min = new System.Windows.Forms.Label();
            this.max = new System.Windows.Forms.Label();
            this.осади = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prognoz
            // 
            this.prognoz.Location = new System.Drawing.Point(141, 253);
            this.prognoz.Name = "prognoz";
            this.prognoz.Size = new System.Drawing.Size(125, 61);
            this.prognoz.TabIndex = 0;
            this.prognoz.Text = "Forecast";
            this.prognoz.UseVisualStyleBackColor = true;
            this.prognoz.Click += new System.EventHandler(this.prognoz_Click_1);
            // 
            // city
            // 
            this.city.Location = new System.Drawing.Point(120, 40);
            this.city.Name = "city";
            this.city.Size = new System.Drawing.Size(205, 20);
            this.city.TabIndex = 1;
            // 
            // min
            // 
            this.min.AutoSize = true;
            this.min.Location = new System.Drawing.Point(189, 121);
            this.min.Name = "min";
            this.min.Size = new System.Drawing.Size(65, 13);
            this.min.TabIndex = 2;
            this.min.Text = "Min. temp.: -";
            // 
            // max
            // 
            this.max.AutoSize = true;
            this.max.Location = new System.Drawing.Point(189, 145);
            this.max.Name = "max";
            this.max.Size = new System.Drawing.Size(68, 13);
            this.max.TabIndex = 3;
            this.max.Text = "Max. temp.: -";
            // 
            // осади
            // 
            this.осади.AutoSize = true;
            this.осади.Location = new System.Drawing.Point(187, 170);
            this.осади.Name = "осади";
            this.осади.Size = new System.Drawing.Size(68, 13);
            this.осади.TabIndex = 5;
            this.осади.Text = "Precipitation:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 416);
            this.Controls.Add(this.осади);
            this.Controls.Add(this.max);
            this.Controls.Add(this.min);
            this.Controls.Add(this.city);
            this.Controls.Add(this.prognoz);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Weather";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button prognoz;
        private System.Windows.Forms.TextBox city;
        private System.Windows.Forms.Label min;
        private System.Windows.Forms.Label max;
        private System.Windows.Forms.Label осади;
    }
}


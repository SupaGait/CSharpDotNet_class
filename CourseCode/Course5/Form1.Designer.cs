namespace Course5
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_PressMe = new System.Windows.Forms.Button();
            this.colorDialog_ButtonBackground = new System.Windows.Forms.ColorDialog();
            this.button_setColor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_PressMe
            // 
            this.button_PressMe.Location = new System.Drawing.Point(115, 113);
            this.button_PressMe.Name = "button_PressMe";
            this.button_PressMe.Size = new System.Drawing.Size(197, 153);
            this.button_PressMe.TabIndex = 0;
            this.button_PressMe.Text = "PressMe";
            this.button_PressMe.UseVisualStyleBackColor = true;
            this.button_PressMe.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_PressMe_MouseUp);
            // 
            // button_setColor
            // 
            this.button_setColor.Location = new System.Drawing.Point(71, 42);
            this.button_setColor.Name = "button_setColor";
            this.button_setColor.Size = new System.Drawing.Size(75, 23);
            this.button_setColor.TabIndex = 1;
            this.button_setColor.Text = "Set Color";
            this.button_setColor.UseVisualStyleBackColor = true;
            this.button_setColor.Click += new System.EventHandler(this.button_setColor_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 399);
            this.Controls.Add(this.button_setColor);
            this.Controls.Add(this.button_PressMe);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_PressMe;
        private System.Windows.Forms.ColorDialog colorDialog_ButtonBackground;
        private System.Windows.Forms.Button button_setColor;
    }
}


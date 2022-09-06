
namespace Minesweeper
{
    partial class RuleForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.text_width = new System.Windows.Forms.RichTextBox();
            this.text_height = new System.Windows.Forms.RichTextBox();
            this.text_bombs = new System.Windows.Forms.RichTextBox();
            this.button_set = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Bombs";
            // 
            // text_width
            // 
            this.text_width.Location = new System.Drawing.Point(79, 12);
            this.text_width.Name = "text_width";
            this.text_width.Size = new System.Drawing.Size(100, 30);
            this.text_width.TabIndex = 1;
            this.text_width.Text = "";
            // 
            // text_height
            // 
            this.text_height.Location = new System.Drawing.Point(79, 48);
            this.text_height.Name = "text_height";
            this.text_height.Size = new System.Drawing.Size(100, 30);
            this.text_height.TabIndex = 2;
            this.text_height.Text = "";
            // 
            // text_bombs
            // 
            this.text_bombs.Location = new System.Drawing.Point(79, 84);
            this.text_bombs.Name = "text_bombs";
            this.text_bombs.Size = new System.Drawing.Size(100, 30);
            this.text_bombs.TabIndex = 3;
            this.text_bombs.Text = "";
            // 
            // button_set
            // 
            this.button_set.Location = new System.Drawing.Point(14, 120);
            this.button_set.Name = "button_set";
            this.button_set.Size = new System.Drawing.Size(79, 25);
            this.button_set.TabIndex = 4;
            this.button_set.Text = "Set";
            this.button_set.UseVisualStyleBackColor = true;
            this.button_set.Click += new System.EventHandler(this.Click_button_set);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(100, 120);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(79, 25);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.Click_button_cancel);
            // 
            // RuleSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(191, 151);
            this.ControlBox = false;
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_set);
            this.Controls.Add(this.text_bombs);
            this.Controls.Add(this.text_height);
            this.Controls.Add(this.text_width);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "RuleSetting";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormClosed_RuleForm);
            this.Load += new System.EventHandler(this.Load_RuleForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox text_width;
        private System.Windows.Forms.RichTextBox text_height;
        private System.Windows.Forms.RichTextBox text_bombs;
        private System.Windows.Forms.Button button_set;
        private System.Windows.Forms.Button button_cancel;
    }
}
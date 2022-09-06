namespace StickerMemo
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.main_text = new System.Windows.Forms.RichTextBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.tool_open = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_save = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_reset = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_focus = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_opacity = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_opacity_up = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_opacity_down = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_text
            // 
            this.main_text.AcceptsTab = true;
            this.main_text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.main_text.EnableAutoDragDrop = true;
            this.main_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.main_text.ForeColor = System.Drawing.SystemColors.Desktop;
            this.main_text.Location = new System.Drawing.Point(12, 27);
            this.main_text.Name = "main_text";
            this.main_text.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.main_text.Size = new System.Drawing.Size(357, 332);
            this.main_text.TabIndex = 0;
            this.main_text.Text = "";
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.SystemColors.HighlightText;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_open,
            this.tool_save,
            this.tool_reset,
            this.tool_focus,
            this.tool_opacity});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(381, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuStrip1";
            // 
            // tool_open
            // 
            this.tool_open.Name = "tool_open";
            this.tool_open.Size = new System.Drawing.Size(48, 20);
            this.tool_open.Text = "Open";
            // 
            // tool_save
            // 
            this.tool_save.Name = "tool_save";
            this.tool_save.Size = new System.Drawing.Size(44, 20);
            this.tool_save.Text = "Save";
            // 
            // tool_reset
            // 
            this.tool_reset.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tool_reset.Name = "tool_reset";
            this.tool_reset.Size = new System.Drawing.Size(47, 20);
            this.tool_reset.Text = "Reset";
            // 
            // tool_focus
            // 
            this.tool_focus.Name = "tool_focus";
            this.tool_focus.Size = new System.Drawing.Size(90, 20);
            this.tool_focus.Text = "Focus Toggle";
            // 
            // tool_opacity
            // 
            this.tool_opacity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_opacity_up,
            this.tool_opacity_down});
            this.tool_opacity.Name = "tool_opacity";
            this.tool_opacity.Size = new System.Drawing.Size(60, 20);
            this.tool_opacity.Text = "Opacity";
            // 
            // tool_opacity_up
            // 
            this.tool_opacity_up.Name = "tool_opacity_up";
            this.tool_opacity_up.Size = new System.Drawing.Size(180, 22);
            this.tool_opacity_up.Text = "Up(10%)";
            // 
            // tool_opacity_down
            // 
            this.tool_opacity_down.Name = "tool_opacity_down";
            this.tool_opacity_down.Size = new System.Drawing.Size(180, 22);
            this.tool_opacity_down.Text = "Down(10%)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(381, 371);
            this.Controls.Add(this.main_text);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.Text = "Sticker Memo";
            this.TopMost = true;
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox main_text;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem tool_reset;
        private System.Windows.Forms.ToolStripMenuItem tool_focus;
        private System.Windows.Forms.ToolStripMenuItem tool_save;
        private System.Windows.Forms.ToolStripMenuItem tool_open;
        private System.Windows.Forms.ToolStripMenuItem tool_opacity;
        private System.Windows.Forms.ToolStripMenuItem tool_opacity_up;
        private System.Windows.Forms.ToolStripMenuItem tool_opacity_down;
    }
}


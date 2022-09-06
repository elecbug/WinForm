
namespace Minesweeper
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
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menu_new = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_rule = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_log = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_log_rank = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_log_playtime = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_gameboard = new System.Windows.Forms.Panel();
            this.label_timer = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label_ranks = new System.Windows.Forms.Label();
            this.label_playtime = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_new,
            this.menu_rule,
            this.menu_log});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(424, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "Menu";
            // 
            // menu_new
            // 
            this.menu_new.Name = "menu_new";
            this.menu_new.Size = new System.Drawing.Size(43, 20);
            this.menu_new.Text = "New";
            this.menu_new.Click += new System.EventHandler(this.Click_menu_new);
            // 
            // menu_rule
            // 
            this.menu_rule.Name = "menu_rule";
            this.menu_rule.Size = new System.Drawing.Size(42, 20);
            this.menu_rule.Text = "Rule";
            this.menu_rule.Click += new System.EventHandler(this.Click_menu_rule);
            // 
            // menu_log
            // 
            this.menu_log.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_log_rank,
            this.menu_log_playtime});
            this.menu_log.Name = "menu_log";
            this.menu_log.Size = new System.Drawing.Size(39, 20);
            this.menu_log.Text = "Log";
            // 
            // menu_log_rank
            // 
            this.menu_log_rank.Name = "menu_log_rank";
            this.menu_log_rank.Size = new System.Drawing.Size(130, 22);
            this.menu_log_rank.Text = "Load Rank";
            this.menu_log_rank.Click += new System.EventHandler(this.Click_menu_log_rank);
            // 
            // menu_log_playtime
            // 
            this.menu_log_playtime.Name = "menu_log_playtime";
            this.menu_log_playtime.Size = new System.Drawing.Size(130, 22);
            this.menu_log_playtime.Text = "Play Time";
            this.menu_log_playtime.Click += new System.EventHandler(this.Click_menu_log_playtime);
            // 
            // panel_gameboard
            // 
            this.panel_gameboard.Location = new System.Drawing.Point(12, 27);
            this.panel_gameboard.Name = "panel_gameboard";
            this.panel_gameboard.Size = new System.Drawing.Size(400, 400);
            this.panel_gameboard.TabIndex = 1;
            // 
            // label_timer
            // 
            this.label_timer.AutoSize = true;
            this.label_timer.Location = new System.Drawing.Point(12, 430);
            this.label_timer.Name = "label_timer";
            this.label_timer.Size = new System.Drawing.Size(38, 12);
            this.label_timer.TabIndex = 2;
            this.label_timer.Text = "Timer";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.Tick_timer);
            // 
            // label_ranks
            // 
            this.label_ranks.AutoSize = true;
            this.label_ranks.Location = new System.Drawing.Point(152, 430);
            this.label_ranks.Name = "label_ranks";
            this.label_ranks.Size = new System.Drawing.Size(37, 12);
            this.label_ranks.TabIndex = 3;
            this.label_ranks.Text = "Rank:";
            // 
            // label_playtime
            // 
            this.label_playtime.AutoSize = true;
            this.label_playtime.Location = new System.Drawing.Point(12, 442);
            this.label_playtime.Name = "label_playtime";
            this.label_playtime.Size = new System.Drawing.Size(0, 12);
            this.label_playtime.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(424, 581);
            this.Controls.Add(this.label_playtime);
            this.Controls.Add(this.label_ranks);
            this.Controls.Add(this.label_timer);
            this.Controls.Add(this.panel_gameboard);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.Text = "Bombs";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormClosed_MainForm);
            this.Load += new System.EventHandler(this.Load_Minesweeper);
            this.ResizeEnd += new System.EventHandler(this.ResizeEnd_MainForm);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.Panel panel_gameboard;
        private System.Windows.Forms.Label label_timer;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem menu_new;
        private System.Windows.Forms.ToolStripMenuItem menu_rule;
        private System.Windows.Forms.ToolStripMenuItem menu_log;
        private System.Windows.Forms.ToolStripMenuItem menu_log_rank;
        private System.Windows.Forms.ToolStripMenuItem menu_log_playtime;
        private System.Windows.Forms.Label label_ranks;
        private System.Windows.Forms.Label label_playtime;
    }
}


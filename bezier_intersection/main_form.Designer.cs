namespace bezier_intersection
{
    partial class main_form
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stBezierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ptToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ptToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.ptToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.ptToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.ndBezierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ptToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ptToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ptToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.randomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.main_panel = new System.Windows.Forms.Panel();
            this.main_panel_mt_pic = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_mouseloc = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_zoom_value = new System.Windows.Forms.ToolStripStatusLabel();
            this.glControl_side_panel = new OpenTK.GLControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStripStatusLabel_sidepanel_coord = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.main_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.main_panel_mt_pic)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(950, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stBezierToolStripMenuItem,
            this.ndBezierToolStripMenuItem,
            this.randomToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // stBezierToolStripMenuItem
            // 
            this.stBezierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ptToolStripMenuItem3,
            this.ptToolStripMenuItem4,
            this.ptToolStripMenuItem5,
            this.ptToolStripMenuItem6});
            this.stBezierToolStripMenuItem.Name = "stBezierToolStripMenuItem";
            this.stBezierToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.stBezierToolStripMenuItem.Text = "1st Bezier";
            // 
            // ptToolStripMenuItem3
            // 
            this.ptToolStripMenuItem3.Name = "ptToolStripMenuItem3";
            this.ptToolStripMenuItem3.Size = new System.Drawing.Size(94, 22);
            this.ptToolStripMenuItem3.Text = "3 pt";
            this.ptToolStripMenuItem3.Click += new System.EventHandler(this.ptToolStripMenuItem3_Click);
            // 
            // ptToolStripMenuItem4
            // 
            this.ptToolStripMenuItem4.Name = "ptToolStripMenuItem4";
            this.ptToolStripMenuItem4.Size = new System.Drawing.Size(94, 22);
            this.ptToolStripMenuItem4.Text = "4 pt";
            this.ptToolStripMenuItem4.Click += new System.EventHandler(this.ptToolStripMenuItem4_Click);
            // 
            // ptToolStripMenuItem5
            // 
            this.ptToolStripMenuItem5.Name = "ptToolStripMenuItem5";
            this.ptToolStripMenuItem5.Size = new System.Drawing.Size(94, 22);
            this.ptToolStripMenuItem5.Text = "5 pt";
            this.ptToolStripMenuItem5.Click += new System.EventHandler(this.ptToolStripMenuItem5_Click);
            // 
            // ptToolStripMenuItem6
            // 
            this.ptToolStripMenuItem6.Name = "ptToolStripMenuItem6";
            this.ptToolStripMenuItem6.Size = new System.Drawing.Size(94, 22);
            this.ptToolStripMenuItem6.Text = "6 pt";
            this.ptToolStripMenuItem6.Click += new System.EventHandler(this.ptToolStripMenuItem6_Click);
            // 
            // ndBezierToolStripMenuItem
            // 
            this.ndBezierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ptToolStripMenuItem,
            this.ptToolStripMenuItem1,
            this.ptToolStripMenuItem2,
            this.ptToolStripMenuItem7});
            this.ndBezierToolStripMenuItem.Name = "ndBezierToolStripMenuItem";
            this.ndBezierToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.ndBezierToolStripMenuItem.Text = "2nd Bezier";
            // 
            // ptToolStripMenuItem
            // 
            this.ptToolStripMenuItem.Name = "ptToolStripMenuItem";
            this.ptToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.ptToolStripMenuItem.Text = "3 pt";
            this.ptToolStripMenuItem.Click += new System.EventHandler(this.ptToolStripMenuItem_Click);
            // 
            // ptToolStripMenuItem1
            // 
            this.ptToolStripMenuItem1.Name = "ptToolStripMenuItem1";
            this.ptToolStripMenuItem1.Size = new System.Drawing.Size(94, 22);
            this.ptToolStripMenuItem1.Text = "4 pt";
            this.ptToolStripMenuItem1.Click += new System.EventHandler(this.ptToolStripMenuItem1_Click);
            // 
            // ptToolStripMenuItem2
            // 
            this.ptToolStripMenuItem2.Name = "ptToolStripMenuItem2";
            this.ptToolStripMenuItem2.Size = new System.Drawing.Size(94, 22);
            this.ptToolStripMenuItem2.Text = "5 pt";
            this.ptToolStripMenuItem2.Click += new System.EventHandler(this.ptToolStripMenuItem2_Click);
            // 
            // ptToolStripMenuItem7
            // 
            this.ptToolStripMenuItem7.Name = "ptToolStripMenuItem7";
            this.ptToolStripMenuItem7.Size = new System.Drawing.Size(94, 22);
            this.ptToolStripMenuItem7.Text = "6 pt";
            this.ptToolStripMenuItem7.Click += new System.EventHandler(this.ptToolStripMenuItem7_Click);
            // 
            // randomToolStripMenuItem
            // 
            this.randomToolStripMenuItem.Name = "randomToolStripMenuItem";
            this.randomToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.randomToolStripMenuItem.Text = "Random";
            this.randomToolStripMenuItem.Click += new System.EventHandler(this.randomToolStripMenuItem_Click);
            // 
            // main_panel
            // 
            this.main_panel.BackColor = System.Drawing.Color.White;
            this.main_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.main_panel.Controls.Add(this.main_panel_mt_pic);
            this.main_panel.Location = new System.Drawing.Point(12, 27);
            this.main_panel.Name = "main_panel";
            this.main_panel.Size = new System.Drawing.Size(377, 411);
            this.main_panel.TabIndex = 1;
            this.main_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.main_panel_Paint);
            this.main_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.main_panel_MouseDown);
            this.main_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.main_panel_MouseMove);
            this.main_panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.main_panel_MouseUp);
            // 
            // main_panel_mt_pic
            // 
            this.main_panel_mt_pic.BackColor = System.Drawing.Color.Transparent;
            this.main_panel_mt_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panel_mt_pic.Enabled = false;
            this.main_panel_mt_pic.Location = new System.Drawing.Point(0, 0);
            this.main_panel_mt_pic.Name = "main_panel_mt_pic";
            this.main_panel_mt_pic.Size = new System.Drawing.Size(373, 407);
            this.main_panel_mt_pic.TabIndex = 0;
            this.main_panel_mt_pic.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_mouseloc,
            this.toolStripStatusLabel_zoom_value,
            this.toolStripStatusLabel_sidepanel_coord});
            this.statusStrip1.Location = new System.Drawing.Point(0, 519);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(950, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_mouseloc
            // 
            this.toolStripStatusLabel_mouseloc.Name = "toolStripStatusLabel_mouseloc";
            this.toolStripStatusLabel_mouseloc.Size = new System.Drawing.Size(15, 17);
            this.toolStripStatusLabel_mouseloc.Text = "[]";
            // 
            // toolStripStatusLabel_zoom_value
            // 
            this.toolStripStatusLabel_zoom_value.Name = "toolStripStatusLabel_zoom_value";
            this.toolStripStatusLabel_zoom_value.Size = new System.Drawing.Size(81, 17);
            this.toolStripStatusLabel_zoom_value.Text = "Zoom = 100%";
            // 
            // glControl_side_panel
            // 
            this.glControl_side_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl_side_panel.BackColor = System.Drawing.Color.Black;
            this.glControl_side_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControl_side_panel.Location = new System.Drawing.Point(561, 105);
            this.glControl_side_panel.Name = "glControl_side_panel";
            this.glControl_side_panel.Size = new System.Drawing.Size(377, 411);
            this.glControl_side_panel.TabIndex = 4;
            this.glControl_side_panel.VSync = false;
            this.glControl_side_panel.Load += new System.EventHandler(this.glControl_side_panel_Load);
            this.glControl_side_panel.SizeChanged += new System.EventHandler(this.glControl_side_panel_SizeChanged);
            this.glControl_side_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_side_panel_Paint);
            this.glControl_side_panel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl_side_panel_KeyDown);
            this.glControl_side_panel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl_side_panel_KeyUp);
            this.glControl_side_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl_side_panel_MouseDown);
            this.glControl_side_panel.MouseEnter += new System.EventHandler(this.glControl_side_panel_MouseEnter);
            this.glControl_side_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl_side_panel_MouseMove);
            this.glControl_side_panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_side_panel_MouseUp);
            this.glControl_side_panel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glControl_side_panel_MouseWheel);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolStripStatusLabel_sidepanel_coord
            // 
            this.toolStripStatusLabel_sidepanel_coord.Name = "toolStripStatusLabel_sidepanel_coord";
            this.toolStripStatusLabel_sidepanel_coord.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel_sidepanel_coord.Text = "[ex,ey]";
            // 
            // main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 541);
            this.Controls.Add(this.glControl_side_panel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.main_panel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 350);
            this.Name = "main_form";
            this.Text = "Bezier Intersection";
            this.Load += new System.EventHandler(this.main_form_Load);
            this.Shown += new System.EventHandler(this.main_form_Shown);
            this.SizeChanged += new System.EventHandler(this.main_form_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.main_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.main_panel_mt_pic)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stBezierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ptToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ptToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem ptToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem ptToolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem ndBezierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ptToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ptToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ptToolStripMenuItem7;
        private System.Windows.Forms.Panel main_panel;
        private System.Windows.Forms.PictureBox main_panel_mt_pic;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_mouseloc;
        private System.Windows.Forms.ToolStripMenuItem randomToolStripMenuItem;
        private OpenTK.GLControl glControl_side_panel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_zoom_value;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_sidepanel_coord;
    }
}
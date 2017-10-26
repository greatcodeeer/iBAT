namespace iBAT
{
    partial class FrmCodeUnit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCodeUnit));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Edit_Enable = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Copy = new System.Windows.Forms.ToolStripButton();
            this.textBox_Code = new System.Windows.Forms.TextBox();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.textBox_Code);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(629, 377);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(629, 427);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Edit_Enable,
            this.toolStripButton_Copy});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(629, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            // 
            // toolStripButton_Edit_Enable
            // 
            this.toolStripButton_Edit_Enable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Edit_Enable.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Edit_Enable.Image")));
            this.toolStripButton_Edit_Enable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Edit_Enable.Name = "toolStripButton_Edit_Enable";
            this.toolStripButton_Edit_Enable.Size = new System.Drawing.Size(84, 22);
            this.toolStripButton_Edit_Enable.Text = "点击编辑文本";
            // 
            // toolStripButton_Copy
            // 
            this.toolStripButton_Copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_Copy.ForeColor = System.Drawing.Color.DarkGreen;
            this.toolStripButton_Copy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Copy.Image")));
            this.toolStripButton_Copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Copy.Name = "toolStripButton_Copy";
            this.toolStripButton_Copy.Size = new System.Drawing.Size(96, 22);
            this.toolStripButton_Copy.Text = "复制全文并关闭";
            // 
            // textBox_Code
            // 
            this.textBox_Code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Code.Location = new System.Drawing.Point(0, 0);
            this.textBox_Code.Multiline = true;
            this.textBox_Code.Name = "textBox_Code";
            this.textBox_Code.ReadOnly = true;
            this.textBox_Code.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Code.Size = new System.Drawing.Size(629, 377);
            this.textBox_Code.TabIndex = 3;
            this.textBox_Code.TabStop = false;
            // 
            // FrmCodeUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 427);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCodeUnit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "代码块";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        public System.Windows.Forms.TextBox textBox_Code;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Edit_Enable;
        private System.Windows.Forms.ToolStripButton toolStripButton_Copy;




    }
}
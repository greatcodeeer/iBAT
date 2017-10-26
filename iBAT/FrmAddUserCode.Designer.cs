namespace iBAT
{
    partial class FrmAddUserCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddUserCode));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Add = new System.Windows.Forms.Button();
            this.comboBox_Group = new System.Windows.Forms.ComboBox();
            this.textBox_Title = new System.Windows.Forms.TextBox();
            this.button_Group_Add = new System.Windows.Forms.Button();
            this.button_Group_Delete = new System.Windows.Forms.Button();
            this.errorProvider_Check = new System.Windows.Forms.ErrorProvider(this.components);
            this.timer_Check = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_Check)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "所在的组";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "代码标题\n";
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(83, 165);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(121, 30);
            this.button_Add.TabIndex = 2;
            this.button_Add.Text = "添加";
            this.button_Add.UseVisualStyleBackColor = true;
            // 
            // comboBox_Group
            // 
            this.comboBox_Group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Group.FormattingEnabled = true;
            this.comboBox_Group.Location = new System.Drawing.Point(26, 45);
            this.comboBox_Group.Name = "comboBox_Group";
            this.comboBox_Group.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Group.TabIndex = 3;
            // 
            // textBox_Title
            // 
            this.textBox_Title.Location = new System.Drawing.Point(26, 111);
            this.textBox_Title.Name = "textBox_Title";
            this.textBox_Title.Size = new System.Drawing.Size(223, 21);
            this.textBox_Title.TabIndex = 4;
            // 
            // button_Group_Add
            // 
            this.button_Group_Add.Location = new System.Drawing.Point(175, 37);
            this.button_Group_Add.Name = "button_Group_Add";
            this.button_Group_Add.Size = new System.Drawing.Size(34, 30);
            this.button_Group_Add.TabIndex = 5;
            this.button_Group_Add.Text = "+";
            this.button_Group_Add.UseVisualStyleBackColor = true;
            // 
            // button_Group_Delete
            // 
            this.button_Group_Delete.Enabled = false;
            this.button_Group_Delete.Location = new System.Drawing.Point(215, 37);
            this.button_Group_Delete.Name = "button_Group_Delete";
            this.button_Group_Delete.Size = new System.Drawing.Size(34, 30);
            this.button_Group_Delete.TabIndex = 6;
            this.button_Group_Delete.Text = "-";
            this.button_Group_Delete.UseVisualStyleBackColor = true;
            // 
            // errorProvider_Check
            // 
            this.errorProvider_Check.BlinkRate = 100;
            this.errorProvider_Check.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider_Check.ContainerControl = this;
            this.errorProvider_Check.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider_Check.Icon")));
            // 
            // timer_Check
            // 
            this.timer_Check.Enabled = true;
            this.timer_Check.Tick += new System.EventHandler(this.timer_Check_Tick);
            // 
            // FrmAddUserCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 215);
            this.Controls.Add(this.button_Group_Delete);
            this.Controls.Add(this.button_Group_Add);
            this.Controls.Add(this.textBox_Title);
            this.Controls.Add(this.comboBox_Group);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddUserCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加代码";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_Check)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.ComboBox comboBox_Group;
        private System.Windows.Forms.TextBox textBox_Title;
        private System.Windows.Forms.Button button_Group_Add;
        private System.Windows.Forms.Button button_Group_Delete;
        private System.Windows.Forms.ErrorProvider errorProvider_Check;
        private System.Windows.Forms.Timer timer_Check;
    }
}
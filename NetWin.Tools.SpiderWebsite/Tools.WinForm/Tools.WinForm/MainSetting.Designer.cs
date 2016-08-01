namespace Tools.WinForm
{
    partial class MainSetting
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PrimaryUrl_Text = new System.Windows.Forms.TextBox();
            this.Add_PrimaryUrl_Btn = new System.Windows.Forms.Button();
            this.PrimaryUrl_ListView = new System.Windows.Forms.ListView();
            this.PrimaryUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Primary_ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.移除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部移除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KeyWord_Text = new System.Windows.Forms.TextBox();
            this.Add_KeyWord_Btn = new System.Windows.Forms.Button();
            this.KeyWord_ListView = new System.Windows.Forms.ListView();
            this.KeyWord_Header = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type_Header = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Position_Header = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Filter_ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.移除ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.全部移除ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Stop_Btn = new System.Windows.Forms.Button();
            this.Start_Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Level_Text = new System.Windows.Forms.TextBox();
            this.Filter_Type_ComboBox = new System.Windows.Forms.ComboBox();
            this.Filter_Title_Radio = new System.Windows.Forms.RadioButton();
            this.Filter_Url_Radio = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.ProcessMessae_LisView = new System.Windows.Forms.ListView();
            this.ViewMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Export_Btn = new System.Windows.Forms.Button();
            this.Import_Btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PrimaryCount_Label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.UserdTime_Label = new System.Windows.Forms.Label();
            this.Reset_Btn = new System.Windows.Forms.Button();
            this.Primary_ContextMenuStrip.SuspendLayout();
            this.Filter_ContextMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrimaryUrl_Text
            // 
            this.PrimaryUrl_Text.Location = new System.Drawing.Point(33, 84);
            this.PrimaryUrl_Text.Multiline = true;
            this.PrimaryUrl_Text.Name = "PrimaryUrl_Text";
            this.PrimaryUrl_Text.Size = new System.Drawing.Size(266, 88);
            this.PrimaryUrl_Text.TabIndex = 0;
            // 
            // Add_PrimaryUrl_Btn
            // 
            this.Add_PrimaryUrl_Btn.Location = new System.Drawing.Point(311, 120);
            this.Add_PrimaryUrl_Btn.Name = "Add_PrimaryUrl_Btn";
            this.Add_PrimaryUrl_Btn.Size = new System.Drawing.Size(75, 23);
            this.Add_PrimaryUrl_Btn.TabIndex = 1;
            this.Add_PrimaryUrl_Btn.Text = "添加";
            this.Add_PrimaryUrl_Btn.UseVisualStyleBackColor = true;
            this.Add_PrimaryUrl_Btn.Click += new System.EventHandler(this.Add_PrimaryUrl_Btn_Click);
            // 
            // PrimaryUrl_ListView
            // 
            this.PrimaryUrl_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PrimaryUrl});
            this.PrimaryUrl_ListView.ContextMenuStrip = this.Primary_ContextMenuStrip;
            this.PrimaryUrl_ListView.Location = new System.Drawing.Point(33, 190);
            this.PrimaryUrl_ListView.Name = "PrimaryUrl_ListView";
            this.PrimaryUrl_ListView.Size = new System.Drawing.Size(326, 97);
            this.PrimaryUrl_ListView.TabIndex = 2;
            this.PrimaryUrl_ListView.UseCompatibleStateImageBehavior = false;
            this.PrimaryUrl_ListView.View = System.Windows.Forms.View.Details;
            this.PrimaryUrl_ListView.SelectedIndexChanged += new System.EventHandler(this.PrimaryUrl_ListView_SelectedIndexChanged);
            this.PrimaryUrl_ListView.MouseEnter += new System.EventHandler(this.PrimaryUrl_ListView_MouseEnter);
            // 
            // PrimaryUrl
            // 
            this.PrimaryUrl.Text = "种子地址队列";
            this.PrimaryUrl.Width = 319;
            // 
            // Primary_ContextMenuStrip
            // 
            this.Primary_ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.移除ToolStripMenuItem,
            this.全部移除ToolStripMenuItem});
            this.Primary_ContextMenuStrip.Name = "Primary_ContextMenuStrip";
            this.Primary_ContextMenuStrip.ShowCheckMargin = true;
            this.Primary_ContextMenuStrip.Size = new System.Drawing.Size(147, 48);
            // 
            // 移除ToolStripMenuItem
            // 
            this.移除ToolStripMenuItem.Name = "移除ToolStripMenuItem";
            this.移除ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.移除ToolStripMenuItem.Text = "移除";
            this.移除ToolStripMenuItem.Click += new System.EventHandler(this.移除ToolStripMenuItem_Click);
            // 
            // 全部移除ToolStripMenuItem
            // 
            this.全部移除ToolStripMenuItem.Name = "全部移除ToolStripMenuItem";
            this.全部移除ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.全部移除ToolStripMenuItem.Text = "全部移除";
            this.全部移除ToolStripMenuItem.Click += new System.EventHandler(this.全部移除ToolStripMenuItem_Click);
            // 
            // KeyWord_Text
            // 
            this.KeyWord_Text.Location = new System.Drawing.Point(137, 354);
            this.KeyWord_Text.Name = "KeyWord_Text";
            this.KeyWord_Text.Size = new System.Drawing.Size(162, 21);
            this.KeyWord_Text.TabIndex = 3;
            // 
            // Add_KeyWord_Btn
            // 
            this.Add_KeyWord_Btn.Location = new System.Drawing.Point(311, 352);
            this.Add_KeyWord_Btn.Name = "Add_KeyWord_Btn";
            this.Add_KeyWord_Btn.Size = new System.Drawing.Size(75, 23);
            this.Add_KeyWord_Btn.TabIndex = 4;
            this.Add_KeyWord_Btn.Text = "添加";
            this.Add_KeyWord_Btn.UseVisualStyleBackColor = true;
            this.Add_KeyWord_Btn.Click += new System.EventHandler(this.Add_KeyWord_Btn_Click);
            // 
            // KeyWord_ListView
            // 
            this.KeyWord_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.KeyWord_Header,
            this.Type_Header,
            this.Position_Header});
            this.KeyWord_ListView.ContextMenuStrip = this.Filter_ContextMenuStrip;
            this.KeyWord_ListView.FullRowSelect = true;
            this.KeyWord_ListView.Location = new System.Drawing.Point(33, 392);
            this.KeyWord_ListView.Name = "KeyWord_ListView";
            this.KeyWord_ListView.Size = new System.Drawing.Size(326, 97);
            this.KeyWord_ListView.TabIndex = 5;
            this.KeyWord_ListView.UseCompatibleStateImageBehavior = false;
            this.KeyWord_ListView.View = System.Windows.Forms.View.Details;
            this.KeyWord_ListView.SelectedIndexChanged += new System.EventHandler(this.KeyWord_ListView_SelectedIndexChanged);
            this.KeyWord_ListView.MouseEnter += new System.EventHandler(this.KeyWord_ListView_MouseEnter);
            // 
            // KeyWord_Header
            // 
            this.KeyWord_Header.Text = "关键字";
            this.KeyWord_Header.Width = 87;
            // 
            // Type_Header
            // 
            this.Type_Header.Text = "筛选类型";
            this.Type_Header.Width = 118;
            // 
            // Position_Header
            // 
            this.Position_Header.Text = "筛选对象";
            this.Position_Header.Width = 105;
            // 
            // Filter_ContextMenuStrip
            // 
            this.Filter_ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.移除ToolStripMenuItem1,
            this.全部移除ToolStripMenuItem1});
            this.Filter_ContextMenuStrip.Name = "Filter_ContextMenuStrip";
            this.Filter_ContextMenuStrip.Size = new System.Drawing.Size(125, 48);
            // 
            // 移除ToolStripMenuItem1
            // 
            this.移除ToolStripMenuItem1.Name = "移除ToolStripMenuItem1";
            this.移除ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.移除ToolStripMenuItem1.Text = "移除";
            this.移除ToolStripMenuItem1.Click += new System.EventHandler(this.移除ToolStripMenuItem1_Click);
            // 
            // 全部移除ToolStripMenuItem1
            // 
            this.全部移除ToolStripMenuItem1.Name = "全部移除ToolStripMenuItem1";
            this.全部移除ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.全部移除ToolStripMenuItem1.Text = "全部移除";
            this.全部移除ToolStripMenuItem1.Click += new System.EventHandler(this.全部移除ToolStripMenuItem1_Click);
            // 
            // Stop_Btn
            // 
            this.Stop_Btn.Location = new System.Drawing.Point(171, 551);
            this.Stop_Btn.Name = "Stop_Btn";
            this.Stop_Btn.Size = new System.Drawing.Size(75, 23);
            this.Stop_Btn.TabIndex = 6;
            this.Stop_Btn.Text = "暂停";
            this.Stop_Btn.UseVisualStyleBackColor = true;
            this.Stop_Btn.Click += new System.EventHandler(this.Stop_Btn_Click);
            // 
            // Start_Btn
            // 
            this.Start_Btn.Location = new System.Drawing.Point(33, 551);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(75, 23);
            this.Start_Btn.TabIndex = 7;
            this.Start_Btn.Text = "开始";
            this.Start_Btn.UseVisualStyleBackColor = true;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 512);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "深度:";
            // 
            // Level_Text
            // 
            this.Level_Text.Location = new System.Drawing.Point(79, 508);
            this.Level_Text.Name = "Level_Text";
            this.Level_Text.Size = new System.Drawing.Size(280, 21);
            this.Level_Text.TabIndex = 9;
            // 
            // Filter_Type_ComboBox
            // 
            this.Filter_Type_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Filter_Type_ComboBox.FormattingEnabled = true;
            this.Filter_Type_ComboBox.Location = new System.Drawing.Point(33, 354);
            this.Filter_Type_ComboBox.Name = "Filter_Type_ComboBox";
            this.Filter_Type_ComboBox.Size = new System.Drawing.Size(82, 20);
            this.Filter_Type_ComboBox.TabIndex = 10;
            // 
            // Filter_Title_Radio
            // 
            this.Filter_Title_Radio.AutoSize = true;
            this.Filter_Title_Radio.Location = new System.Drawing.Point(122, 319);
            this.Filter_Title_Radio.Name = "Filter_Title_Radio";
            this.Filter_Title_Radio.Size = new System.Drawing.Size(71, 16);
            this.Filter_Title_Radio.TabIndex = 11;
            this.Filter_Title_Radio.TabStop = true;
            this.Filter_Title_Radio.Text = "网站标题";
            this.Filter_Title_Radio.UseVisualStyleBackColor = true;
            // 
            // Filter_Url_Radio
            // 
            this.Filter_Url_Radio.AutoSize = true;
            this.Filter_Url_Radio.Location = new System.Drawing.Point(228, 319);
            this.Filter_Url_Radio.Name = "Filter_Url_Radio";
            this.Filter_Url_Radio.Size = new System.Drawing.Size(71, 16);
            this.Filter_Url_Radio.TabIndex = 12;
            this.Filter_Url_Radio.TabStop = true;
            this.Filter_Url_Radio.Text = "网址链接";
            this.Filter_Url_Radio.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "筛选对象:";
            // 
            // ProcessMessae_LisView
            // 
            this.ProcessMessae_LisView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ViewMessage});
            this.ProcessMessae_LisView.Location = new System.Drawing.Point(33, 596);
            this.ProcessMessae_LisView.Name = "ProcessMessae_LisView";
            this.ProcessMessae_LisView.Size = new System.Drawing.Size(326, 164);
            this.ProcessMessae_LisView.TabIndex = 14;
            this.ProcessMessae_LisView.UseCompatibleStateImageBehavior = false;
            this.ProcessMessae_LisView.View = System.Windows.Forms.View.Details;
            // 
            // ViewMessage
            // 
            this.ViewMessage.Text = "日志信息";
            this.ViewMessage.Width = 315;
            // 
            // Export_Btn
            // 
            this.Export_Btn.Location = new System.Drawing.Point(21, 20);
            this.Export_Btn.Name = "Export_Btn";
            this.Export_Btn.Size = new System.Drawing.Size(100, 23);
            this.Export_Btn.TabIndex = 15;
            this.Export_Btn.Text = "导出采集结果";
            this.Export_Btn.UseVisualStyleBackColor = true;
            this.Export_Btn.Click += new System.EventHandler(this.Export_Btn_Click);
            // 
            // Import_Btn
            // 
            this.Import_Btn.Location = new System.Drawing.Point(195, 20);
            this.Import_Btn.Name = "Import_Btn";
            this.Import_Btn.Size = new System.Drawing.Size(103, 23);
            this.Import_Btn.TabIndex = 16;
            this.Import_Btn.Text = "导入排除地址";
            this.Import_Btn.UseVisualStyleBackColor = true;
            this.Import_Btn.Click += new System.EventHandler(this.Import_Btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Import_Btn);
            this.groupBox1.Controls.Add(this.Export_Btn);
            this.groupBox1.Location = new System.Drawing.Point(33, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 56);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 773);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "当前需要抓取的种子数:";
            // 
            // PrimaryCount_Label
            // 
            this.PrimaryCount_Label.AutoSize = true;
            this.PrimaryCount_Label.Location = new System.Drawing.Point(169, 773);
            this.PrimaryCount_Label.Name = "PrimaryCount_Label";
            this.PrimaryCount_Label.Size = new System.Drawing.Size(11, 12);
            this.PrimaryCount_Label.TabIndex = 19;
            this.PrimaryCount_Label.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 773);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "本次运行用时:";
            // 
            // UserdTime_Label
            // 
            this.UserdTime_Label.AutoSize = true;
            this.UserdTime_Label.Location = new System.Drawing.Point(318, 773);
            this.UserdTime_Label.Name = "UserdTime_Label";
            this.UserdTime_Label.Size = new System.Drawing.Size(11, 12);
            this.UserdTime_Label.TabIndex = 21;
            this.UserdTime_Label.Text = "0";
            // 
            // Reset_Btn
            // 
            this.Reset_Btn.Location = new System.Drawing.Point(311, 551);
            this.Reset_Btn.Name = "Reset_Btn";
            this.Reset_Btn.Size = new System.Drawing.Size(75, 23);
            this.Reset_Btn.TabIndex = 22;
            this.Reset_Btn.Text = "重置";
            this.Reset_Btn.UseVisualStyleBackColor = true;
            this.Reset_Btn.Click += new System.EventHandler(this.Reset_Btn_Click);
            // 
            // MainSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 803);
            this.Controls.Add(this.Reset_Btn);
            this.Controls.Add(this.UserdTime_Label);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PrimaryCount_Label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ProcessMessae_LisView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Filter_Url_Radio);
            this.Controls.Add(this.Filter_Title_Radio);
            this.Controls.Add(this.Filter_Type_ComboBox);
            this.Controls.Add(this.Level_Text);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Start_Btn);
            this.Controls.Add(this.Stop_Btn);
            this.Controls.Add(this.KeyWord_ListView);
            this.Controls.Add(this.Add_KeyWord_Btn);
            this.Controls.Add(this.KeyWord_Text);
            this.Controls.Add(this.PrimaryUrl_ListView);
            this.Controls.Add(this.PrimaryUrl_Text);
            this.Controls.Add(this.Add_PrimaryUrl_Btn);
            this.Name = "MainSetting";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainSetting_Load);
            this.Primary_ContextMenuStrip.ResumeLayout(false);
            this.Filter_ContextMenuStrip.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PrimaryUrl_Text;
        private System.Windows.Forms.Button Add_PrimaryUrl_Btn;
        private System.Windows.Forms.ListView PrimaryUrl_ListView;
        private System.Windows.Forms.ColumnHeader PrimaryUrl;
        private System.Windows.Forms.TextBox KeyWord_Text;
        private System.Windows.Forms.Button Add_KeyWord_Btn;
        private System.Windows.Forms.ListView KeyWord_ListView;
        private System.Windows.Forms.ColumnHeader KeyWord_Header;
        private System.Windows.Forms.ColumnHeader Type_Header;
        private System.Windows.Forms.Button Stop_Btn;
        private System.Windows.Forms.Button Start_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Level_Text;
        private System.Windows.Forms.ComboBox Filter_Type_ComboBox;
        private System.Windows.Forms.ColumnHeader Position_Header;
        private System.Windows.Forms.RadioButton Filter_Title_Radio;
        private System.Windows.Forms.RadioButton Filter_Url_Radio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView ProcessMessae_LisView;
        private System.Windows.Forms.ColumnHeader ViewMessage;
        private System.Windows.Forms.Button Export_Btn;
        private System.Windows.Forms.Button Import_Btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label PrimaryCount_Label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label UserdTime_Label;
        private System.Windows.Forms.Button Reset_Btn;
        private System.Windows.Forms.ContextMenuStrip Primary_ContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 移除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部移除ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip Filter_ContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 移除ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 全部移除ToolStripMenuItem1;

    }
}


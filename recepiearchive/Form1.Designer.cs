namespace RecepieScraper
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
            this.components = new System.ComponentModel.Container();
            this.thumbNails = new System.Windows.Forms.ImageList(this.components);
            this.lstRecepieCatalogue = new System.Windows.Forms.ListView();
            this.lstSelection = new System.Windows.Forms.ListView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.recepieMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ratingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.orderByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ratingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.caloriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prepTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dontShow1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openInRecepieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recepiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reScrapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.recepieMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // thumbNails
            // 
            this.thumbNails.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.thumbNails.ImageSize = new System.Drawing.Size(16, 16);
            this.thumbNails.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lstRecepieCatalogue
            // 
            this.lstRecepieCatalogue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstRecepieCatalogue.LargeImageList = this.thumbNails;
            this.lstRecepieCatalogue.Location = new System.Drawing.Point(12, 88);
            this.lstRecepieCatalogue.Name = "lstRecepieCatalogue";
            this.lstRecepieCatalogue.Size = new System.Drawing.Size(337, 690);
            this.lstRecepieCatalogue.TabIndex = 2;
            this.lstRecepieCatalogue.UseCompatibleStateImageBehavior = false;
            this.lstRecepieCatalogue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstRecepieCatalogue_MouseClick);
            // 
            // lstSelection
            // 
            this.lstSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSelection.LargeImageList = this.thumbNails;
            this.lstSelection.Location = new System.Drawing.Point(355, 88);
            this.lstSelection.Name = "lstSelection";
            this.lstSelection.Size = new System.Drawing.Size(338, 690);
            this.lstSelection.TabIndex = 3;
            this.lstSelection.UseCompatibleStateImageBehavior = false;
            this.lstSelection.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstRecepieCatalogue_MouseClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(13, 59);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(336, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "ADD >>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(355, 59);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(338, 23);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "<< REMOVE";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(699, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(530, 818);
            this.dataGridView1.TabIndex = 6;
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(13, 33);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(336, 20);
            this.tbSearch.TabIndex = 7;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // recepieMenu
            // 
            this.recepieMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.toolStripSeparator2,
            this.ratingToolStripMenuItem,
            this.orderByToolStripMenuItem,
            this.filtersToolStripMenuItem,
            this.toolStripSeparator1,
            this.openInRecepieToolStripMenuItem});
            this.recepieMenu.Name = "recepieMenu";
            this.recepieMenu.Size = new System.Drawing.Size(161, 148);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.addToolStripMenuItem.Text = "Add >>";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.removeToolStripMenuItem.Text = "<< Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // ratingToolStripMenuItem
            // 
            this.ratingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.ratingToolStripMenuItem.Name = "ratingToolStripMenuItem";
            this.ratingToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ratingToolStripMenuItem.Text = "Rating";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(93, 22);
            this.toolStripMenuItem2.Text = "1 ★";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.starButton_click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(93, 22);
            this.toolStripMenuItem3.Text = "2 ★";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.starButton_click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(93, 22);
            this.toolStripMenuItem4.Text = "3 ★";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.starButton_click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(93, 22);
            this.toolStripMenuItem5.Text = "4 ★";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.starButton_click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(93, 22);
            this.toolStripMenuItem6.Text = "5 ★";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.starButton_click);
            // 
            // orderByToolStripMenuItem
            // 
            this.orderByToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameToolStripMenuItem,
            this.ratingToolStripMenuItem1,
            this.caloriesToolStripMenuItem,
            this.prepTimeToolStripMenuItem});
            this.orderByToolStripMenuItem.Name = "orderByToolStripMenuItem";
            this.orderByToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.orderByToolStripMenuItem.Text = "Order By";
            // 
            // nameToolStripMenuItem
            // 
            this.nameToolStripMenuItem.Name = "nameToolStripMenuItem";
            this.nameToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.nameToolStripMenuItem.Text = "Name";
            this.nameToolStripMenuItem.Click += new System.EventHandler(this.orderByName_click);
            // 
            // ratingToolStripMenuItem1
            // 
            this.ratingToolStripMenuItem1.Name = "ratingToolStripMenuItem1";
            this.ratingToolStripMenuItem1.Size = new System.Drawing.Size(128, 22);
            this.ratingToolStripMenuItem1.Text = "Rating";
            this.ratingToolStripMenuItem1.Click += new System.EventHandler(this.ratingToolStripMenuItem1_Click);
            // 
            // caloriesToolStripMenuItem
            // 
            this.caloriesToolStripMenuItem.Name = "caloriesToolStripMenuItem";
            this.caloriesToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.caloriesToolStripMenuItem.Text = "Calories";
            this.caloriesToolStripMenuItem.Click += new System.EventHandler(this.caloriesToolStripMenuItem_Click);
            // 
            // prepTimeToolStripMenuItem
            // 
            this.prepTimeToolStripMenuItem.Name = "prepTimeToolStripMenuItem";
            this.prepTimeToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.prepTimeToolStripMenuItem.Text = "Prep Time";
            this.prepTimeToolStripMenuItem.Click += new System.EventHandler(this.prepTimeToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dontShow1ToolStripMenuItem});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // dontShow1ToolStripMenuItem
            // 
            this.dontShow1ToolStripMenuItem.Name = "dontShow1ToolStripMenuItem";
            this.dontShow1ToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.dontShow1ToolStripMenuItem.Text = "Don\'t Show 1 ★";
            this.dontShow1ToolStripMenuItem.Click += new System.EventHandler(this.dontShow1ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // openInRecepieToolStripMenuItem
            // 
            this.openInRecepieToolStripMenuItem.Name = "openInRecepieToolStripMenuItem";
            this.openInRecepieToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.openInRecepieToolStripMenuItem.Text = "Open in Recepie";
            this.openInRecepieToolStripMenuItem.Click += new System.EventHandler(this.openInRecepieToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.recepiesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1243, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // recepiesToolStripMenuItem
            // 
            this.recepiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reScrapeToolStripMenuItem});
            this.recepiesToolStripMenuItem.Name = "recepiesToolStripMenuItem";
            this.recepiesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.recepiesToolStripMenuItem.Text = "Recepies";
            // 
            // reScrapeToolStripMenuItem
            // 
            this.reScrapeToolStripMenuItem.Name = "reScrapeToolStripMenuItem";
            this.reScrapeToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.reScrapeToolStripMenuItem.Text = "ReScrape";
            this.reScrapeToolStripMenuItem.Click += new System.EventHandler(this.reScrapeToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.Location = new System.Drawing.Point(13, 785);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(680, 92);
            this.tbLog.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 889);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstSelection);
            this.Controls.Add(this.lstRecepieCatalogue);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Food Picker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.recepieMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList thumbNails;
        private System.Windows.Forms.ListView lstRecepieCatalogue;
        private System.Windows.Forms.ListView lstSelection;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.ContextMenuStrip recepieMenu;
        private System.Windows.Forms.ToolStripMenuItem ratingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderByToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ratingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem caloriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prepTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInRecepieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dontShow1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recepiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reScrapeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox tbLog;
    }
}


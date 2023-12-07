namespace AmadiaVente.Winforms.functionality
{
    partial class gestionMembre
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            guna2GradientPanel2 = new Guna.UI2.WinForms.Guna2GradientPanel();
            btnAddMember = new Guna.UI2.WinForms.Guna2GradientButton();
            btnDeleteMember = new Guna.UI2.WinForms.Guna2GradientButton();
            dataGridViewListMember = new Guna.UI2.WinForms.Guna2DataGridView();
            guna2GradientPanel1.SuspendLayout();
            guna2GradientPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewListMember).BeginInit();
            SuspendLayout();
            // 
            // guna2GradientPanel1
            // 
            guna2GradientPanel1.BackColor = Color.White;
            guna2GradientPanel1.Controls.Add(guna2GradientPanel2);
            guna2GradientPanel1.CustomizableEdges = customizableEdges7;
            guna2GradientPanel1.Dock = DockStyle.Fill;
            guna2GradientPanel1.FillColor = Color.FromArgb(191, 210, 255);
            guna2GradientPanel1.FillColor2 = Color.FromArgb(151, 170, 205);
            guna2GradientPanel1.Location = new Point(0, 0);
            guna2GradientPanel1.Name = "guna2GradientPanel1";
            guna2GradientPanel1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2GradientPanel1.Size = new Size(886, 478);
            guna2GradientPanel1.TabIndex = 8;
            // 
            // guna2GradientPanel2
            // 
            guna2GradientPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            guna2GradientPanel2.BackColor = Color.Transparent;
            guna2GradientPanel2.BorderRadius = 15;
            guna2GradientPanel2.Controls.Add(dataGridViewListMember);
            guna2GradientPanel2.Controls.Add(btnAddMember);
            guna2GradientPanel2.Controls.Add(btnDeleteMember);
            guna2GradientPanel2.CustomizableEdges = customizableEdges5;
            guna2GradientPanel2.FillColor = Color.WhiteSmoke;
            guna2GradientPanel2.FillColor2 = SystemColors.ButtonFace;
            guna2GradientPanel2.Location = new Point(24, 24);
            guna2GradientPanel2.Name = "guna2GradientPanel2";
            guna2GradientPanel2.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2GradientPanel2.Size = new Size(821, 442);
            guna2GradientPanel2.TabIndex = 40;
            // 
            // btnAddMember
            // 
            btnAddMember.Anchor = AnchorStyles.Top;
            btnAddMember.BorderColor = Color.FromArgb(191, 210, 255);
            btnAddMember.BorderRadius = 15;
            btnAddMember.BorderThickness = 2;
            btnAddMember.CustomizableEdges = customizableEdges1;
            btnAddMember.DisabledState.BorderColor = Color.DarkGray;
            btnAddMember.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAddMember.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAddMember.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnAddMember.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAddMember.FillColor = Color.Transparent;
            btnAddMember.FillColor2 = Color.Transparent;
            btnAddMember.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnAddMember.ForeColor = Color.White;
            btnAddMember.Image = Properties.Resources.icons8_checkmark_blue_96;
            btnAddMember.ImageSize = new Size(30, 30);
            btnAddMember.Location = new Point(134, 88);
            btnAddMember.Name = "btnAddMember";
            btnAddMember.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAddMember.Size = new Size(60, 55);
            btnAddMember.TabIndex = 32;
            btnAddMember.Click += btnAddMember_Click;
            // 
            // btnDeleteMember
            // 
            btnDeleteMember.Anchor = AnchorStyles.Top;
            btnDeleteMember.BorderColor = Color.FromArgb(191, 210, 255);
            btnDeleteMember.BorderRadius = 15;
            btnDeleteMember.BorderThickness = 2;
            btnDeleteMember.CustomizableEdges = customizableEdges3;
            btnDeleteMember.DisabledState.BorderColor = Color.DarkGray;
            btnDeleteMember.DisabledState.CustomBorderColor = Color.DarkGray;
            btnDeleteMember.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnDeleteMember.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            btnDeleteMember.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnDeleteMember.FillColor = Color.Transparent;
            btnDeleteMember.FillColor2 = Color.Transparent;
            btnDeleteMember.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnDeleteMember.ForeColor = Color.White;
            btnDeleteMember.Image = Properties.Resources.icons8_effacer_96;
            btnDeleteMember.ImageSize = new Size(30, 30);
            btnDeleteMember.Location = new Point(218, 88);
            btnDeleteMember.Name = "btnDeleteMember";
            btnDeleteMember.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnDeleteMember.Size = new Size(60, 55);
            btnDeleteMember.TabIndex = 33;
            btnDeleteMember.Click += btnDeleteMember_Click;
            // 
            // dataGridViewListMember
            // 
            dataGridViewListMember.AllowUserToAddRows = false;
            dataGridViewListMember.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewListMember.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewListMember.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewListMember.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewListMember.ColumnHeadersHeight = 30;
            dataGridViewListMember.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewListMember.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewListMember.GridColor = Color.FromArgb(231, 229, 255);
            dataGridViewListMember.Location = new Point(19, 149);
            dataGridViewListMember.Name = "dataGridViewListMember";
            dataGridViewListMember.ReadOnly = true;
            dataGridViewListMember.RowHeadersVisible = false;
            dataGridViewListMember.RowTemplate.Height = 25;
            dataGridViewListMember.Size = new Size(783, 277);
            dataGridViewListMember.TabIndex = 34;
            dataGridViewListMember.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dataGridViewListMember.ThemeStyle.AlternatingRowsStyle.Font = null;
            dataGridViewListMember.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dataGridViewListMember.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dataGridViewListMember.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dataGridViewListMember.ThemeStyle.BackColor = Color.White;
            dataGridViewListMember.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dataGridViewListMember.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewListMember.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewListMember.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewListMember.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dataGridViewListMember.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewListMember.ThemeStyle.HeaderStyle.Height = 30;
            dataGridViewListMember.ThemeStyle.ReadOnly = true;
            dataGridViewListMember.ThemeStyle.RowsStyle.BackColor = Color.White;
            dataGridViewListMember.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewListMember.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewListMember.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewListMember.ThemeStyle.RowsStyle.Height = 25;
            dataGridViewListMember.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewListMember.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // gestionMembre
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(886, 478);
            Controls.Add(guna2GradientPanel1);
            Name = "gestionMembre";
            Text = "gestionMembre";
            Load += gestionMembre_Load;
            guna2GradientPanel1.ResumeLayout(false);
            guna2GradientPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewListMember).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel2;
        private Guna.UI2.WinForms.Guna2GradientButton btnAddMember;
        private Guna.UI2.WinForms.Guna2GradientButton btnDeleteMember;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridViewListMember;
    }
}
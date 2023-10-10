namespace AmadiaVente.Winforms.functionality
{
    partial class rendu
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            dateTimeDebutDashboard = new Guna.UI2.WinForms.Guna2DateTimePicker();
            dateTimeFinDashboard = new Guna.UI2.WinForms.Guna2DateTimePicker();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            dataGridViewDashboard = new Guna.UI2.WinForms.Guna2DataGridView();
            guna2GradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDashboard).BeginInit();
            SuspendLayout();
            // 
            // dateTimeDebutDashboard
            // 
            dateTimeDebutDashboard.Checked = true;
            dateTimeDebutDashboard.CustomizableEdges = customizableEdges13;
            dateTimeDebutDashboard.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimeDebutDashboard.Format = DateTimePickerFormat.Long;
            dateTimeDebutDashboard.Location = new Point(189, 18);
            dateTimeDebutDashboard.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dateTimeDebutDashboard.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dateTimeDebutDashboard.Name = "dateTimeDebutDashboard";
            dateTimeDebutDashboard.ShadowDecoration.CustomizableEdges = customizableEdges14;
            dateTimeDebutDashboard.Size = new Size(215, 36);
            dateTimeDebutDashboard.TabIndex = 0;
            dateTimeDebutDashboard.Value = new DateTime(2023, 9, 17, 22, 39, 39, 427);
            // 
            // dateTimeFinDashboard
            // 
            dateTimeFinDashboard.Checked = true;
            dateTimeFinDashboard.CustomizableEdges = customizableEdges15;
            dateTimeFinDashboard.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimeFinDashboard.Format = DateTimePickerFormat.Long;
            dateTimeFinDashboard.Location = new Point(459, 18);
            dateTimeFinDashboard.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dateTimeFinDashboard.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dateTimeFinDashboard.Name = "dateTimeFinDashboard";
            dateTimeFinDashboard.ShadowDecoration.CustomizableEdges = customizableEdges16;
            dateTimeFinDashboard.Size = new Size(215, 36);
            dateTimeFinDashboard.TabIndex = 1;
            dateTimeFinDashboard.Value = new DateTime(2023, 9, 17, 22, 39, 39, 427);
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            guna2HtmlLabel1.Location = new Point(19, 23);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(141, 22);
            guna2HtmlLabel1.TabIndex = 2;
            guna2HtmlLabel1.Text = "Compte Rendu du :";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            guna2HtmlLabel2.Location = new Point(410, 23);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(31, 22);
            guna2HtmlLabel2.TabIndex = 3;
            guna2HtmlLabel2.Text = "Au :";
            // 
            // guna2GradientPanel1
            // 
            guna2GradientPanel1.Controls.Add(dataGridViewDashboard);
            guna2GradientPanel1.Controls.Add(dateTimeDebutDashboard);
            guna2GradientPanel1.Controls.Add(dateTimeFinDashboard);
            guna2GradientPanel1.Controls.Add(guna2HtmlLabel2);
            guna2GradientPanel1.Controls.Add(guna2HtmlLabel1);
            guna2GradientPanel1.CustomizableEdges = customizableEdges17;
            guna2GradientPanel1.Location = new Point(12, 49);
            guna2GradientPanel1.Name = "guna2GradientPanel1";
            guna2GradientPanel1.ShadowDecoration.CustomizableEdges = customizableEdges18;
            guna2GradientPanel1.Size = new Size(862, 417);
            guna2GradientPanel1.TabIndex = 5;
            // 
            // dataGridViewDashboard
            // 
            dataGridViewDashboard.AllowUserToAddRows = false;
            dataGridViewDashboard.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = Color.White;
            dataGridViewDashboard.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = Color.White;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            dataGridViewDashboard.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewDashboard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.White;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            dataGridViewDashboard.DefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewDashboard.GridColor = Color.FromArgb(231, 229, 255);
            dataGridViewDashboard.Location = new Point(19, 138);
            dataGridViewDashboard.Name = "dataGridViewDashboard";
            dataGridViewDashboard.ReadOnly = true;
            dataGridViewDashboard.RowHeadersVisible = false;
            dataGridViewDashboard.RowTemplate.Height = 25;
            dataGridViewDashboard.Size = new Size(819, 263);
            dataGridViewDashboard.TabIndex = 4;
            dataGridViewDashboard.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dataGridViewDashboard.ThemeStyle.AlternatingRowsStyle.Font = null;
            dataGridViewDashboard.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dataGridViewDashboard.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dataGridViewDashboard.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dataGridViewDashboard.ThemeStyle.BackColor = Color.White;
            dataGridViewDashboard.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dataGridViewDashboard.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewDashboard.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewDashboard.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewDashboard.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dataGridViewDashboard.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDashboard.ThemeStyle.HeaderStyle.Height = 4;
            dataGridViewDashboard.ThemeStyle.ReadOnly = true;
            dataGridViewDashboard.ThemeStyle.RowsStyle.BackColor = Color.White;
            dataGridViewDashboard.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewDashboard.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewDashboard.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewDashboard.ThemeStyle.RowsStyle.Height = 25;
            dataGridViewDashboard.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewDashboard.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // rendu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(886, 478);
            Controls.Add(guna2GradientPanel1);
            Name = "rendu";
            Text = "rendu";
            Load += rendu_Load;
            guna2GradientPanel1.ResumeLayout(false);
            guna2GradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDashboard).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2DateTimePicker dateTimeDebutDashboard;
        private Guna.UI2.WinForms.Guna2DateTimePicker dateTimeFinDashboard;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridViewDashboard;
    }
}
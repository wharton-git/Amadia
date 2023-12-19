namespace AmadiaVente.Winforms.popUp
{
    partial class popUpRecuperationMdp
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            panelRecupMdp = new Guna.UI2.WinForms.Guna2GradientPanel();
            btnQuitList = new Guna.UI2.WinForms.Guna2ImageButton();
            panelRecoveryMdpChild = new Guna.UI2.WinForms.Guna2Panel();
            panelRecupMdp.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 15;
            guna2Elipse1.TargetControl = this;
            // 
            // panelRecupMdp
            // 
            panelRecupMdp.BackColor = Color.White;
            panelRecupMdp.BorderColor = Color.FromArgb(23, 117, 197);
            panelRecupMdp.BorderRadius = 6;
            panelRecupMdp.BorderThickness = 4;
            panelRecupMdp.Controls.Add(panelRecoveryMdpChild);
            panelRecupMdp.Controls.Add(btnQuitList);
            panelRecupMdp.CustomizableEdges = customizableEdges4;
            panelRecupMdp.Dock = DockStyle.Fill;
            panelRecupMdp.Location = new Point(0, 0);
            panelRecupMdp.Name = "panelRecupMdp";
            panelRecupMdp.ShadowDecoration.CustomizableEdges = customizableEdges5;
            panelRecupMdp.Size = new Size(425, 341);
            panelRecupMdp.TabIndex = 2;
            panelRecupMdp.MouseDown += panelRecupMdp_MouseDown;
            panelRecupMdp.MouseMove += panelRecupMdp_MouseMove;
            panelRecupMdp.MouseUp += panelRecupMdp_MouseUp;
            // 
            // btnQuitList
            // 
            btnQuitList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuitList.CheckedState.ImageSize = new Size(64, 64);
            btnQuitList.HoverState.ImageSize = new Size(64, 64);
            btnQuitList.Image = Properties.Resources.icons8_fermer_la_fenêtre_96;
            btnQuitList.ImageOffset = new Point(0, 0);
            btnQuitList.ImageRotate = 0F;
            btnQuitList.ImageSize = new Size(30, 30);
            btnQuitList.Location = new Point(386, 12);
            btnQuitList.Name = "btnQuitList";
            btnQuitList.PressedState.ImageSize = new Size(64, 64);
            btnQuitList.ShadowDecoration.CustomizableEdges = customizableEdges3;
            btnQuitList.Size = new Size(27, 26);
            btnQuitList.TabIndex = 0;
            btnQuitList.Click += btnQuitList_Click;
            // 
            // panelRecoveryMdpChild
            // 
            panelRecoveryMdpChild.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelRecoveryMdpChild.CustomizableEdges = customizableEdges1;
            panelRecoveryMdpChild.Location = new Point(32, 39);
            panelRecoveryMdpChild.Name = "panelRecoveryMdpChild";
            panelRecoveryMdpChild.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelRecoveryMdpChild.Size = new Size(348, 275);
            panelRecoveryMdpChild.TabIndex = 1;
            // 
            // popUpRecuperationMdp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 341);
            Controls.Add(panelRecupMdp);
            FormBorderStyle = FormBorderStyle.None;
            Name = "popUpRecuperationMdp";
            Text = "popUpRecuperationMdp";
            Load += popUpRecuperationMdp_Load;
            panelRecupMdp.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2GradientPanel panelRecupMdp;
        private Guna.UI2.WinForms.Guna2ImageButton btnQuitList;
        private Guna.UI2.WinForms.Guna2Panel panelRecoveryMdpChild;
    }
}
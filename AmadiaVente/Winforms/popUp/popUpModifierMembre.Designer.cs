﻿namespace AmadiaVente.Winforms.popUp
{
    partial class popUpModifierMembre
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelAddList = new Guna.UI2.WinForms.Guna2GradientPanel();
            btnQuit = new Guna.UI2.WinForms.Guna2ImageButton();
            btnQuitList = new Guna.UI2.WinForms.Guna2ImageButton();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            test = new Guna.UI2.WinForms.Guna2HtmlLabel();
            panelAddList.SuspendLayout();
            SuspendLayout();
            // 
            // panelAddList
            // 
            panelAddList.BackColor = Color.White;
            panelAddList.BorderColor = Color.FromArgb(23, 117, 197);
            panelAddList.BorderRadius = 6;
            panelAddList.BorderThickness = 4;
            panelAddList.Controls.Add(test);
            panelAddList.Controls.Add(btnQuit);
            panelAddList.Controls.Add(btnQuitList);
            panelAddList.CustomizableEdges = customizableEdges3;
            panelAddList.Dock = DockStyle.Fill;
            panelAddList.Location = new Point(0, 0);
            panelAddList.Name = "panelAddList";
            panelAddList.ShadowDecoration.CustomizableEdges = customizableEdges4;
            panelAddList.Size = new Size(800, 450);
            panelAddList.TabIndex = 2;
            panelAddList.MouseDown += panelAddList_MouseDown;
            panelAddList.MouseMove += panelAddList_MouseMove;
            panelAddList.MouseUp += panelAddList_MouseUp;
            // 
            // btnQuit
            // 
            btnQuit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnQuit.CheckedState.ImageSize = new Size(64, 64);
            btnQuit.HoverState.ImageSize = new Size(64, 64);
            btnQuit.Image = Properties.Resources.icons8_fermer_la_fenêtre_96;
            btnQuit.ImageOffset = new Point(0, 0);
            btnQuit.ImageRotate = 0F;
            btnQuit.ImageSize = new Size(30, 30);
            btnQuit.Location = new Point(761, 12);
            btnQuit.Name = "btnQuit";
            btnQuit.PressedState.ImageSize = new Size(64, 64);
            btnQuit.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btnQuit.Size = new Size(27, 26);
            btnQuit.TabIndex = 1;
            btnQuit.Click += btnQuit_Click;
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
            btnQuitList.Location = new Point(857, 12);
            btnQuitList.Name = "btnQuitList";
            btnQuitList.PressedState.ImageSize = new Size(64, 64);
            btnQuitList.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnQuitList.Size = new Size(27, 26);
            btnQuitList.TabIndex = 0;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 15;
            guna2Elipse1.TargetControl = this;
            // 
            // test
            // 
            test.BackColor = Color.Transparent;
            test.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            test.Location = new Point(335, 141);
            test.Name = "test";
            test.Size = new Size(46, 23);
            test.TabIndex = 38;
            test.Text = "TEST : ";
            // 
            // popUpModifierMembre
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelAddList);
            FormBorderStyle = FormBorderStyle.None;
            Name = "popUpModifierMembre";
            StartPosition = FormStartPosition.CenterParent;
            Text = "popUpModifierMembre";
            Load += popUpModifierMembre_Load;
            panelAddList.ResumeLayout(false);
            panelAddList.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel panelAddList;
        private Guna.UI2.WinForms.Guna2ImageButton btnQuitList;
        private Guna.UI2.WinForms.Guna2ImageButton btnQuit;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2HtmlLabel test;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmadiaVente.Winforms
{
    public partial class main : Form
    {
        //Déclaration Globale
        private Size originalSize;
        private Form activeForm;
        public string sessionUserId;
        String sessionNom;
        String sessionPrenom;
        String sessionFunction;
        String sessionId;

        //Méthodes (fonctions)
        public main(string sessionNom, string sessionPrenom, string sessionFunction, string sessionId)
        {
            InitializeComponent();
            originalSize = this.Size;

            this.sessionNom = sessionNom;
            this.sessionPrenom = sessionPrenom;
            this.sessionFunction = sessionFunction;
            this.sessionId = sessionId;
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            //ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelMainChild.Controls.Add(childForm);
            this.panelMainChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void disconnectAction()
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = this.Location;
            form1.Size = this.Size;
            if (this.WindowState == FormWindowState.Maximized)
            {
                form1.WindowState = FormWindowState.Maximized;
                form1.Size = originalSize;
            }
            else
            {
                form1.Size = this.Size;
            }
            form1.FormClosed += (s, args) => Application.Exit();
            form1.Show();
            this.Hide();
        }

        //Evénements
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Voulez-vous vous déconnecter ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                disconnectAction();
            }
        }
        private void main_Load(object sender, EventArgs e)
        {
            labelSession.Text = sessionId;
            sessionUserId = sessionId;
            btnHidePanel.Visible = false;
            OpenChildForm(new functionality.achat(), sender);
        }

        private void btnAchat_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.achat(), sender);
        }

        private void btnCompteRendu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.rendu(), sender);
        }

        private void btnShowPanel_Click(object sender, EventArgs e)
        {
            btnShowPanel.Visible = false;
            panelLeftMenu.Width = 175;
            btnHidePanel.Visible = true;
        }

        private void btnHidePanel_Click(object sender, EventArgs e)
        {
            btnShowPanel.Visible = true;
            panelLeftMenu.Width = 40;
            btnHidePanel.Visible = false;
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.profil(), sender);
        }

        private void btnFournisseur_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.Fournisseur1(), sender);
        }
    }
}

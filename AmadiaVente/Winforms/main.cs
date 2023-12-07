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

        //Méthodes (fonctions)
        public main()
        {
            InitializeComponent();
            originalSize = this.Size;

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
            OpenChildForm(new functionality.achat(), sender);
        }

        private void btnAchat_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.achat(), sender);
            btnAchat.FillColor = btnAchat.FillColor2 = Color.FromArgb(191, 210, 255);
            btnAchat.ForeColor = Color.White;

            btnList.ForeColor = btnCompteRendu.ForeColor = btnStock.ForeColor = btnFournisseur.ForeColor = btnProfil.ForeColor = Color.FromArgb(23, 117, 197);
            btnList.FillColor = btnList.FillColor2 = btnCompteRendu.FillColor = btnStock.FillColor2 = btnStock.FillColor = btnCompteRendu.FillColor2 = btnFournisseur.FillColor = btnFournisseur.FillColor2 = btnProfil.FillColor = btnProfil.FillColor2 = Color.WhiteSmoke;

        }

        private void btnCompteRendu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.rendu(), sender);
            btnCompteRendu.FillColor = btnCompteRendu.FillColor2 = Color.FromArgb(191, 210, 255);
            btnCompteRendu.ForeColor = Color.White;

            btnList.ForeColor = btnAchat.ForeColor = btnStock.ForeColor = btnFournisseur.ForeColor = btnProfil.ForeColor = Color.FromArgb(23, 117, 197);
            btnList.FillColor = btnList.FillColor2 = btnAchat.FillColor = btnStock.FillColor2 = btnStock.FillColor = btnAchat.FillColor2 = btnFournisseur.FillColor = btnFournisseur.FillColor2 = btnProfil.FillColor = btnProfil.FillColor2 = Color.WhiteSmoke;

        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.profil(), sender);
            btnProfil.FillColor = btnProfil.FillColor2 = Color.FromArgb(191, 210, 255);
            btnProfil.ForeColor = Color.White;

            btnList.ForeColor = btnCompteRendu.ForeColor = btnStock.ForeColor = btnFournisseur.ForeColor = btnAchat.ForeColor = Color.FromArgb(23, 117, 197);
            btnList.FillColor = btnList.FillColor2 = btnCompteRendu.FillColor = btnStock.FillColor2 = btnStock.FillColor = btnCompteRendu.FillColor2 = btnFournisseur.FillColor = btnFournisseur.FillColor2 = btnAchat.FillColor = btnAchat.FillColor2 = Color.WhiteSmoke;

        }

        private void btnFournisseur_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.Fournisseur1(), sender);
            btnFournisseur.FillColor = btnFournisseur.FillColor2 = Color.FromArgb(191, 210, 255);
            btnFournisseur.ForeColor = Color.White;

            btnList.ForeColor = btnCompteRendu.ForeColor = btnStock.ForeColor = btnAchat.ForeColor = btnProfil.ForeColor = Color.FromArgb(23, 117, 197);
            btnList.FillColor = btnList.FillColor2 = btnCompteRendu.FillColor = btnStock.FillColor2 = btnStock.FillColor = btnCompteRendu.FillColor2 = btnAchat.FillColor = btnAchat.FillColor2 = btnProfil.FillColor = btnProfil.FillColor2 = Color.WhiteSmoke;

        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.stock(), sender);
            btnStock.FillColor = btnStock.FillColor2 = Color.FromArgb(191, 210, 255);
            btnStock.ForeColor = Color.White;

            btnFournisseur.ForeColor = btnList.ForeColor = btnCompteRendu.ForeColor = btnAchat.ForeColor = btnProfil.ForeColor = Color.FromArgb(23, 117, 197);
            btnFournisseur.FillColor = btnFournisseur.FillColor2 = btnList.FillColor = btnList.FillColor2 = btnCompteRendu.FillColor = btnCompteRendu.FillColor2 = btnAchat.FillColor = btnAchat.FillColor2 = btnProfil.FillColor = btnProfil.FillColor2 = Color.WhiteSmoke;

        }

        private void btnList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.listeStock(), sender);
            btnList.FillColor = btnList.FillColor2 = Color.FromArgb(191, 210, 255);
            btnList.ForeColor = Color.White;

            btnStock.ForeColor = btnFournisseur.ForeColor = btnCompteRendu.ForeColor = btnAchat.ForeColor = btnProfil.ForeColor = Color.FromArgb(23, 117, 197);
            btnStock.FillColor = btnStock.FillColor2 = btnFournisseur.FillColor = btnFournisseur.FillColor2 = btnCompteRendu.FillColor = btnCompteRendu.FillColor2 = btnAchat.FillColor = btnAchat.FillColor2 = btnProfil.FillColor = btnProfil.FillColor2 = Color.WhiteSmoke;
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.gestionMembre(), sender);
            btnList.ForeColor = btnStock.ForeColor = btnFournisseur.ForeColor = btnCompteRendu.ForeColor = btnAchat.ForeColor = btnProfil.ForeColor = Color.FromArgb(23, 117, 197);
            btnList.FillColor = btnList.FillColor2 = btnStock.FillColor = btnStock.FillColor2 = btnFournisseur.FillColor = btnFournisseur.FillColor2 = btnCompteRendu.FillColor = btnCompteRendu.FillColor2 = btnAchat.FillColor = btnAchat.FillColor2 = btnProfil.FillColor = btnProfil.FillColor2 = Color.WhiteSmoke;
        }

        private void btnCotisation_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.cotisation(), sender);
            btnList.ForeColor = btnStock.ForeColor = btnFournisseur.ForeColor = btnCompteRendu.ForeColor = btnAchat.ForeColor = btnProfil.ForeColor = Color.FromArgb(23, 117, 197);
            btnList.FillColor = btnList.FillColor2 = btnStock.FillColor = btnStock.FillColor2 = btnFournisseur.FillColor = btnFournisseur.FillColor2 = btnCompteRendu.FillColor = btnCompteRendu.FillColor2 = btnAchat.FillColor = btnAchat.FillColor2 = btnProfil.FillColor = btnProfil.FillColor2 = Color.WhiteSmoke;
        }
    }
}

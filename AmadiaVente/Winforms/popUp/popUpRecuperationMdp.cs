using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace AmadiaVente.Winforms.popUp
{
    public partial class popUpRecuperationMdp : Form
    {
        //Déclaration Globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");

        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;

        //Constructeur
        public popUpRecuperationMdp()
        {
            InitializeComponent();
        }
        //Méthodes
        private void verifyLogin(string login)
        {

        }

        //Evenements
        private void popUpRecuperationMdp_Load(object sender, EventArgs e)
        {
            panelChangeMdp.Visible = false;
            panelVerifiyAccount.Visible = true;
        }

        private void btnQuitList_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelRecupMdp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPos = Cursor.Position;
                lastFormPos = this.Location;
            }
        }

        private void panelRecupMdp_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                this.Location = new Point(lastFormPos.X + delta.X, lastFormPos.Y + delta.Y);
            }
        }

        private void panelRecupMdp_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void brnCheckMdpRecovery_Click(object sender, EventArgs e)
        {
            string login = "Vide";
            string recoveryCode = "Vide";
            bool loginStat = false;
            bool codeStat = false;

            if (!string.IsNullOrEmpty(txtBoxLogin.Text))
            {
                login = txtBoxLogin.Text.ToString();
                loginStat = true;
            }
            if (!string.IsNullOrEmpty(txtBoxRecoveryCode.Text))
            {
                recoveryCode = txtBoxRecoveryCode.Text.ToString();
                codeStat = true;
            }

            if (loginStat)
            {
                if (codeStat)
                {
                    DialogResult confirm = MessageBox.Show("Procéder à la verification ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirm == DialogResult.Yes)
                    {
                        panelChangeMdp.Visible = true;
                        panelVerifiyAccount.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Veuiller entrer votre Code de récupération", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veuiller entrer votre nom d'utilisateur (Login)", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}

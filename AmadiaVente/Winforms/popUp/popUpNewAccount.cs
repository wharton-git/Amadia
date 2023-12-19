using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace AmadiaVente.Winforms.popUp
{
    public partial class popUpNewAccount : Form
    {
        //Declaration Globales
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");

        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;

        //Constructeur
        public popUpNewAccount()
        {
            InitializeComponent();
        }
        //Méthodes
        private bool sameMdp(string a, string b)
        {
            return (a == b);
        }

        static string generationRandomCode(int length)
        {
            Random random = new Random();
            const string chars = "0123456789";

            char[] code = new char[length];
            for (int i = 0; i < length; i++)
            {
                code[i] = chars[random.Next(chars.Length)];
            }

            return new string(code);
        }

        private void AjouterUtilisateur(string username, string password, string nom, string prenom, string codeRecup)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cs))
            {
                connection.Open();

                string insertQuery = "INSERT INTO user (username, password, nom_user, prenom_user, code_recup) VALUES (@username, @password, @nom, @prenom, @codeRecup)";

                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@nom", nom);
                    command.Parameters.AddWithValue("@prenom", prenom);
                    command.Parameters.AddWithValue("@codeRecup", codeRecup);

                    command.ExecuteNonQuery();
                }
            }
        }

        //Evenements
        private void popUpNewAccount_Load(object sender, EventArgs e)
        {
            txtBoxNewMdp.UseSystemPasswordChar = true;
            txtBoxNewConfirmMdp.UseSystemPasswordChar = true;

            panelNewAccountSecond.Visible = false;

            btnHideNewMdp.Visible = btnHideConfirmMdp.Visible = false;

            btnSaveNewAccount.Enabled = false;
            string codeRandom = generationRandomCode(5);
            labelCodeRecupMdp.Text = codeRandom;
        }

        private void btnQuitList_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelAddList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPos = Cursor.Position;
                lastFormPos = this.Location;
            }
        }

        private void panelAddList_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                this.Location = new Point(lastFormPos.X + delta.X, lastFormPos.Y + delta.Y);
            }
        }

        private void panelAddList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void btnShowNewMpd_Click(object sender, EventArgs e)
        {
            txtBoxNewMdp.UseSystemPasswordChar = false;
            btnShowNewMpd.Visible = false;
            btnHideNewMdp.Visible = true;
        }

        private void btnHideNewMdp_Click(object sender, EventArgs e)
        {
            txtBoxNewMdp.UseSystemPasswordChar = true;
            btnShowNewMpd.Visible = true;
            btnHideNewMdp.Visible = false;
        }

        private void btnShowConfirmMdp_Click(object sender, EventArgs e)
        {
            txtBoxNewConfirmMdp.UseSystemPasswordChar = false;
            btnShowConfirmMdp.Visible = false;
            btnHideConfirmMdp.Visible = true;
        }

        private void btnHideConfirmMdp_Click(object sender, EventArgs e)
        {
            txtBoxNewConfirmMdp.UseSystemPasswordChar = true;
            btnShowConfirmMdp.Visible = true;
            btnHideConfirmMdp.Visible = false;
        }

        private void txtBoxNewConfirmMdp_TextChanged(object sender, EventArgs e)
        {
            string mdp = txtBoxNewMdp.Text.ToString();
            string confirmMdp = txtBoxNewConfirmMdp.Text.ToString();

            if (sameMdp(mdp, confirmMdp))
            {
                txtBoxNewConfirmMdp.FillColor = Color.PaleGreen;
            }
            else
            {
                txtBoxNewConfirmMdp.FillColor = Color.LightPink;
            }

            if (string.IsNullOrEmpty(txtBoxNewConfirmMdp.Text))
            {
                txtBoxNewConfirmMdp.FillColor = Color.White;
            }
        }

        private void btnNewAccountNext_Click(object sender, EventArgs e)
        {
            if (btnNewAccountNext.Text == "Suivant")
            {
                if (!String.IsNullOrEmpty(txtBoxNewNom.Text) && !String.IsNullOrEmpty(txtBoxNewPrenom.Text) && !String.IsNullOrEmpty(txtBoxNewLogin.Text) && !String.IsNullOrEmpty(txtBoxNewMdp.Text) && !String.IsNullOrEmpty(txtBoxNewConfirmMdp.Text))
                {
                    string mdp = txtBoxNewMdp.Text.ToString();
                    string confirmMdp = txtBoxNewConfirmMdp.Text.ToString();
                    if (sameMdp(mdp, confirmMdp))
                    {
                        panelNewAccountFirst.Visible = false;
                        panelNewAccountSecond.Visible = true;
                        btnNewAccountNext.Text = "Retour";

                        btnSaveNewAccount.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Les mots de passes ne correspondent pas !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("Veuillez remplir les champs avant de continuer ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                panelNewAccountFirst.Visible = true;

                panelNewAccountSecond.Visible = false;
                btnNewAccountNext.Text = "Suivant";

                btnSaveNewAccount.Enabled = false;
            }
        }

        private void btnChangeCode_Click_1(object sender, EventArgs e)
        {
            string codeRandom = generationRandomCode(5);
            labelCodeRecupMdp.Text = codeRandom;
        }

        private void txtBoxNewNom_TextChanged(object sender, EventArgs e)
        {
            string pseudo = txtBoxNewNom.Text.ToLower() + "_" + DateTime.Now.ToString("ss");
            txtBoxNewLogin.Text = pseudo;
        }

        private void btnSaveNewAccount_Click(object sender, EventArgs e)
        {
            String nom = txtBoxNewNom.Text.ToString();
            String prenom = txtBoxNewPrenom.Text.ToString();
            String login = txtBoxNewLogin.Text.ToString();
            String mdp = txtBoxNewConfirmMdp.Text.ToString();
            String codeRecup = labelCodeRecupMdp.Text.ToString();

            try
            {
                AjouterUtilisateur(login, mdp, nom, prenom, codeRecup);
                MessageBox.Show("Compte crée, vous pouvez maintenant vous connecter à l'application", "Informaation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création du compte : " + ex, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

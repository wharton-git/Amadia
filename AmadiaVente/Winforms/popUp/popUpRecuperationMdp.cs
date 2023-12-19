using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmadiaVente.Winforms.functionality;
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
        private string[] verifyLogin(string login)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT username, code_recup FROM user WHERE username = @username";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", login);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            string username = reader.GetString(0);
                            string recoveryCode = reader.GetString(1);

                            return new string[] { username, recoveryCode };
                        }
                    }
                }
            }
            return null;
        }

        private bool verifyCode(string enterCode, string recoveryCode)
        {
            return enterCode == recoveryCode;
        }

        private void reinitialiseMdp(string mdp, string username)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();
                string query = "UPDATE user SET password = @mdp WHERE username = @login";

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@mdp", mdp);
                    command.Parameters.AddWithValue("@login", username);

                    command.ExecuteNonQuery();
                }
            }
        }

        //Evenements
        private void popUpRecuperationMdp_Load(object sender, EventArgs e)
        {
            panelChangeMdp.Visible = false;
            panelVerifiyAccount.Visible = true;

            txtBoxRecoveryMdp.UseSystemPasswordChar = true;
            txtBoxRecoveryConfirmMdp.UseSystemPasswordChar = true;

            btnHideConfirmMdp.Visible = false;
            btnHideNewMdp.Visible = false;
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
                        string[] takeInfo = verifyLogin(login);

                        if (takeInfo != null)
                        {
                            string takeLogin = takeInfo[0];
                            string takeReciveryCode = takeInfo[1];

                            if (verifyCode(recoveryCode, takeReciveryCode))
                            {
                                panelChangeMdp.Visible = true;
                                panelVerifiyAccount.Visible = false;
                                MessageBox.Show("Créer votre nouveau mot de passe !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Code de récuperation erroné.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Utilisateur introuvable.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veuiller entrer votre Code de récupération.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veuiller entrer votre nom d'utilisateur (Login).", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnSaveRecoveryMdp_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxRecoveryMdp.Text))
            {
                if (!string.IsNullOrEmpty(txtBoxRecoveryConfirmMdp.Text))
                {
                    string newConfirmMdp = txtBoxRecoveryConfirmMdp.Text.ToString();
                    string newMdp = txtBoxRecoveryMdp.Text.ToString();
                    string username = txtBoxLogin.Text.ToString();

                    if (verifyCode(newConfirmMdp, newMdp))
                    {
                        try
                        {
                            reinitialiseMdp(newConfirmMdp, username);
                            MessageBox.Show("Votre mot de passe a été réinitilisé avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erreur lors de la réinitialisation du mot de passe", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Veuillez confirmez votre nouveau mot de passe.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nouveau mot de passe vide !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBoxRecoveryConfirmMdp_TextChanged(object sender, EventArgs e)
        {
            string mdp = txtBoxRecoveryMdp.Text.ToString();
            string confirmMdp = txtBoxRecoveryConfirmMdp.Text.ToString();

            if (verifyCode(mdp, confirmMdp))
            {
                txtBoxRecoveryConfirmMdp.FillColor = Color.PaleGreen;
            }
            else
            {
                txtBoxRecoveryConfirmMdp.FillColor = Color.LightPink;
            }

            if (string.IsNullOrEmpty(txtBoxRecoveryConfirmMdp.Text))
            {
                txtBoxRecoveryConfirmMdp.FillColor = Color.White;
            }
        }

        private void btnHideNewMdp_Click(object sender, EventArgs e)
        {
            txtBoxRecoveryMdp.UseSystemPasswordChar = true;
            btnShowNewMpd.Visible = true;
            btnHideNewMdp.Visible = false;
        }

        private void btnHideConfirmMdp_Click(object sender, EventArgs e)
        {
            txtBoxRecoveryConfirmMdp.UseSystemPasswordChar = true;
            btnShowConfirmMdp.Visible = true;
            btnHideConfirmMdp.Visible = false;
        }

        private void btnShowConfirmMdp_Click(object sender, EventArgs e)
        {
            txtBoxRecoveryConfirmMdp.UseSystemPasswordChar = false;
            btnShowConfirmMdp.Visible = false;
            btnHideConfirmMdp.Visible = true;
        }

        private void btnShowNewMpd_Click(object sender, EventArgs e)
        {
            txtBoxRecoveryMdp.UseSystemPasswordChar = false;
            btnShowNewMpd.Visible = false;
            btnHideNewMdp.Visible = true;
        }
    }
}

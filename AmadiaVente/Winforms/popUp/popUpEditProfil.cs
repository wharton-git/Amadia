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
using Microsoft.VisualBasic.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AmadiaVente.Winforms.popUp
{
    public partial class popUpEditProfil : Form
    {
        //Declaration globale
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");
        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;
        string userId;

        //Constructeur
        public popUpEditProfil(string id)
        {

            InitializeComponent();
            userId = id;

            txtBoxEditCurrentMdp.UseSystemPasswordChar = true;
            txtBoxEditNewMdp.UseSystemPasswordChar = true;
            txtBoxEditConfirmMdp.UseSystemPasswordChar = true;

            this.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    isDragging = true;
                    lastCursorPos = Cursor.Position;
                    lastFormPos = this.Location;
                }
            };

            // Gérez l'événement MouseMove pour déplacer le formulaire
            this.MouseMove += (sender, e) =>
            {
                if (isDragging)
                {
                    Point delta = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                    this.Location = new Point(lastFormPos.X + delta.X, lastFormPos.Y + delta.Y);
                }
            };

            // Gérez l'événement MouseUp pour arrêter de déplacer le formulaire
            this.MouseUp += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    isDragging = false;
                }
            };
        }

        //Methodes
        private string[] afficheInfo(string id)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT username,password,nom_user,prenom_user,fonction_user FROM user WHERE id_user = @id";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            string loginUser = reader.GetString(0);
                            string passUser = reader.GetString(1);
                            string nomUser = reader.GetString(2);
                            string prenomUser = reader.GetString(3);
                            string fonctionUser = reader.GetString(4);

                            return new string[] { loginUser, passUser, nomUser, prenomUser, fonctionUser };
                        }
                    }
                }
            }
            return null;
        }

        private void updateInfo(string login, string nom, string prenom, string fonction, string id)
        {
            try
            {
                using (SqliteConnection con = new SqliteConnection(cs))
                {
                    con.Open();
                    string updateInfoQuery = "UPDATE user SET username = @login, nom_user = @nom, prenom_user = @prenom, fonction_user = @fonction WHERE id_user = @id";

                    using (SqliteCommand comm = new SqliteCommand(updateInfoQuery, con))
                    {
                        comm.Parameters.AddWithValue("@login", login);
                        comm.Parameters.AddWithValue("@nom", nom);
                        comm.Parameters.AddWithValue("@prenom", prenom);
                        comm.Parameters.AddWithValue("@fonction", fonction);
                        comm.Parameters.AddWithValue("@id", id);

                        comm.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Information de Profil Mis à Jour", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateMdp(string mdp, string id)
        {
            try
            {
                using (SqliteConnection con = new SqliteConnection(cs))
                {
                    con.Open();
                    string updateInfoQuery = "UPDATE user SET password = @mdp WHERE id_user = @id";

                    using (SqliteCommand comm = new SqliteCommand(updateInfoQuery, con))
                    {
                        comm.Parameters.AddWithValue("@mdp", mdp);
                        comm.Parameters.AddWithValue("@id", id);

                        comm.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Mot de passe Mis à Jour", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool sameMdp(string a, string b)
        {
            return (a == b);
        }

        private bool isCurrentMdp(string mdp, string id)
        {
            string trueMdp = afficheInfo(id)[1];
            return sameMdp(trueMdp, mdp);
        }


        private void clearMdp()
        {
            txtBoxEditCurrentMdp.Text = txtBoxEditNewMdp.Text = txtBoxEditConfirmMdp.Text = string.Empty;
            txtBoxEditConfirmMdp.FillColor = Color.White;
        }

        //Evénements
        private void popUpEditProfil_Load(object sender, EventArgs e)
        {
            string[] info = afficheInfo(userId);
            panelEditMdp.Visible = false;
            txtBoxEditNom.Text = info[2];
            txtBoxEditPrenom.Text = info[3];
            txtBoxNewFonction.Text = info[4];
            txtBoxEditUsername.Text = info[0];

            btnHideCurrentMdp.Visible = btnHideNewMdp.Visible = btnHideConfirmMdp.Visible = false;
        }

        private void btnQuitPopUp_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditSave_Click(object sender, EventArgs e)
        {
            string nvNom = txtBoxEditNom.Text.ToString();
            string nvPrenom = txtBoxEditPrenom.Text.ToString();
            string nvUserName = txtBoxEditUsername.Text.ToString();
            string nvFonction = txtBoxNewFonction.Text.ToString();

            if (!string.IsNullOrEmpty(txtBoxEditNom.Text) && !string.IsNullOrEmpty(txtBoxEditPrenom.Text) && !string.IsNullOrEmpty(txtBoxEditUsername.Text) && !string.IsNullOrEmpty(txtBoxNewFonction.Text))
            {

                if (!string.IsNullOrEmpty(txtBoxEditCurrentMdp.Text))
                {
                    string currentPass = txtBoxEditCurrentMdp.Text.ToString();
                    string newPass = txtBoxEditNewMdp.Text.ToString();
                    string confirmPass = txtBoxEditConfirmMdp.Text.ToString();


                    if (isCurrentMdp(currentPass, userId))
                    {

                        if (!string.IsNullOrEmpty(txtBoxEditNewMdp.Text) && !string.IsNullOrEmpty(txtBoxEditConfirmMdp.Text))
                        {
                            if (sameMdp(newPass, confirmPass))
                            {
                                updateMdp(newPass, userId);
                                updateInfo(nvUserName, nvNom, nvPrenom, nvFonction, userId);
                                clearMdp();
                            }
                            else
                            {
                                MessageBox.Show("Les nouveaux Mot de passe ne se correspondent pas !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Veuillez Completer les champs !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vérifier votre mot de passe actuel", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    updateInfo(nvUserName, nvNom, nvPrenom, nvFonction, userId);
                }
;
            }
            else
            {
                MessageBox.Show("Veuillez Remplir les champs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void guna2GradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPos = Cursor.Position;
                lastFormPos = this.Location;
            }
        }

        private void guna2GradientPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                this.Location = new Point(lastFormPos.X + delta.X, lastFormPos.Y + delta.Y);
            }
        }

        private void guna2GradientPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void btnShowCurrentMdp_Click(object sender, EventArgs e)
        {
            txtBoxEditCurrentMdp.UseSystemPasswordChar = false;
            btnShowCurrentMdp.Visible = false;
            btnHideCurrentMdp.Visible = true;
        }

        private void btnShowNewMpd_Click(object sender, EventArgs e)
        {
            txtBoxEditNewMdp.UseSystemPasswordChar = false;
            btnShowNewMpd.Visible = false;
            btnHideNewMdp.Visible = true;
        }

        private void btnShowConfirmMdp_Click(object sender, EventArgs e)
        {
            txtBoxEditConfirmMdp.UseSystemPasswordChar = false;
            btnShowConfirmMdp.Visible = false;
            btnHideConfirmMdp.Visible = true;
        }

        private void btnHideCurrentMdp_Click(object sender, EventArgs e)
        {
            txtBoxEditCurrentMdp.UseSystemPasswordChar = true;
            btnShowCurrentMdp.Visible = true;
            btnHideCurrentMdp.Visible = false;
        }

        private void btnHideNewMdp_Click(object sender, EventArgs e)
        {
            txtBoxEditNewMdp.UseSystemPasswordChar = true;
            btnShowNewMpd.Visible = true;
            btnHideNewMdp.Visible = false;
        }

        private void btnHideConfirmMdp_Click(object sender, EventArgs e)
        {
            txtBoxEditConfirmMdp.UseSystemPasswordChar = true;
            btnShowConfirmMdp.Visible = true;
            btnHideConfirmMdp.Visible = false;
        }

        private void txtBoxEditConfirmMdp_TextChanged(object sender, EventArgs e)
        {
            string newMdp = txtBoxEditNewMdp.Text.ToString();
            string confirmMdp = txtBoxEditConfirmMdp.Text.ToString();

            if (sameMdp(newMdp, confirmMdp))
            {
                txtBoxEditConfirmMdp.FillColor = Color.PaleGreen;
            }
            else
            {
                txtBoxEditConfirmMdp.FillColor = Color.LightPink;
            }

            if (string.IsNullOrEmpty(txtBoxEditConfirmMdp.Text))
            {
                txtBoxEditConfirmMdp.FillColor = Color.White;
            }
        }

        private void btnChangeMdp_Click(object sender, EventArgs e)
        {
            if (!panelEditMdp.Visible)
            {
                panelEditMdp.Visible = true;
                btnChangeMdp.Text = "Annuler modification du mot de passe ?";
                clearMdp();
            }
            else
            {
                panelEditMdp.Visible = false;
                btnChangeMdp.Text = "Changer le mot de passe ?";
                clearMdp();
            }
        }

        private void btnChangeMdp_MouseEnter(object sender, EventArgs e)
        {
            btnChangeMdp.ForeColor = Color.RoyalBlue;
        }

        private void btnChangeMdp_MouseLeave(object sender, EventArgs e)
        {
            btnChangeMdp.ForeColor = Color.Black;
        }
    }
}

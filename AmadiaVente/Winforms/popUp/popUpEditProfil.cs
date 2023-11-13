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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AmadiaVente.Winforms.popUp
{
    public partial class popUpEditProfil : Form
    {
        //Declaration globale
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "../../../database.db");
        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;
        string userId;

        //Constructeur
        public popUpEditProfil(string id)
        {

            InitializeComponent();
            userId = id;

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

        //Evénements
        private void popUpEditProfil_Load(object sender, EventArgs e)
        {
            panelEditMdp.Visible = false;
            afficheInfo(userId);
        }

        private void labelChangeMdp_MouseEnter(object sender, EventArgs e)
        {
            labelChangeMdp.ForeColor = Color.RoyalBlue;
            labelChangeMdp.Cursor = Cursors.Hand;
        }

        private void labelChangeMdp_MouseLeave(object sender, EventArgs e)
        {
            labelChangeMdp.ForeColor = Color.Black;
            labelChangeMdp.Cursor = Cursors.Default;
        }

        private void labelChangeMdp_Click(object sender, EventArgs e)
        {
            if (!panelEditMdp.Visible)
            {
                panelEditMdp.Visible = true;
                labelChangeMdp.Text = "Annuler modification du mot de passe ?";
            }
            else
            {
                panelEditMdp.Visible = false;
                labelChangeMdp.Text = "Changer le mot de passe ?";
            }
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

            MessageBox.Show(userId + " " + nvNom + nvPrenom + nvUserName);
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
    }
}

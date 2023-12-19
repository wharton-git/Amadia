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

namespace AmadiaVente.Winforms.functionality
{
    public partial class profil : Form
    {
        //declaration global
        string sessionId;
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");


        //Méthodes
        public profil()
        {
            InitializeComponent();
            labelProfilId.Visible = false;

        }

        private string[] afficheDetail(string id)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT * FROM user WHERE id_user=@id";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            int userId = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string password = reader.GetString(2);
                            string nomUser = reader.GetString(3);
                            string prenomUser = reader.GetString(4);

                            return new string[] { userId.ToString(), username, password, nomUser, prenomUser };
                        }
                    }
                }
            }
            return null;
        }

        //Evenements
        private void profil_Load(object sender, EventArgs e)
        {
            sessionId = Classes.Storage.SessionId;
            string[] info = afficheDetail(sessionId);
            labelProfilId.Text = "Id : " + info[0];
            labelProfilUsername.Text = "Nom d'utilisateur : " + info[1];
            labelProfilNom.Text = info[3];
            labelProfilPrenom.Text = info[4];
        }

        private void btnEditProfil_Click(object sender, EventArgs e)
        {

            Winforms.popUp.popUpEditProfil popup = new Winforms.popUp.popUpEditProfil(sessionId);

            // Affichez la fenêtre contextuelle en mode modal (PopUp)
            popup.ShowDialog();

            // Libérez les ressources de la fenêtre contextuelle après qu'elle a été fermée
            popup.Dispose();

            sessionId = Classes.Storage.SessionId;
            string[] info = afficheDetail(sessionId);
            labelProfilId.Text = "Id : " + info[0];
            labelProfilUsername.Text = "Nom d'utilisateur : " + info[1];
            labelProfilNom.Text = info[3];
            labelProfilPrenom.Text = info[4];
        }
    }
}

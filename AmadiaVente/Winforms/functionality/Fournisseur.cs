using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;

namespace AmadiaVente.Winforms.functionality
{
    public partial class Fournisseur : Form
    {
        public Fournisseur()
        {
            InitializeComponent();
        }

        private void btnAjoutFornisseur_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=mydatabase.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // 3. Créez une commande SQL d'insertion
                string sql = "INSERT INTO fournisseur (nomFournisseur, contact, email, adresse) VALUES (@NomFournisseur, @contact, @email, @adresse)";
                using (SQLiteCommand co = new SQLiteCommand(sql, connection))
                {
                    // 4. Ajoutez des paramètres pour les valeurs que vous souhaitez insérer
                    co.Parameters.AddWithValue("@nomFournisseur", NomFournisseur.Text); // Utilisez le contrôle NomFournisseur.Text ici
                    co.Parameters.AddWithValue("@contact", ContactFourisseur.Text); // Utilisez le contrôle ContactFourisseur.Text ici
                    co.Parameters.AddWithValue("@email", EmailFournisseur.Text); // Utilisez le contrôle EmailFournisseur.Text ici
                    co.Parameters.AddWithValue("@adresse", AdresseFournisseur.Text); // Utilisez le contrôle AdresseFournisseur.Text ici

                    // 5. Exécutez la commande SQL
                    co.ExecuteNonQuery();
                }
            }
        }


        private void btnSupFournisseur_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=mydatabase.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // 3. Créez une commande SQL de suppression
                string sql = "DELETE FROM Fournisseur WHERE NomFournisseur = @NomFournisseur";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    // 4. Ajoutez un paramètre pour la valeur que vous souhaitez supprimer
                    command.Parameters.AddWithValue("@NomFournisseur", NomFournisseur);

                    // 5. Exécutez la commande SQL
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

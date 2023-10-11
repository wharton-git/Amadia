﻿using System;
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

namespace AmadiaVente.Winforms.functionality
{
    public partial class rendu : Form
    {
        // Déclaration globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "../../../database.db");
        int id_commande = 0;
        // Constructeur
        public rendu()
        {
            InitializeComponent();
        }

        //Méthodes
        void afficherRecetteDuJour(DateTime date)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT id_commande, COALESCE(nom_membre, 'Non'),COALESCE(prenom_membre, 'Membre'), nom_user, prenom_user, c.date_achat FROM commande c LEFT JOIN membre m ON m.id_membre = c.id_membre INNER JOIN user u ON u.id_user = c.id_responsable WHERE date_achat > @dateNow";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@dateNow", date);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        dataGridViewDashboard.DataSource = dataTable;
                    }
                }
            }
        }

        // Événements

        private void rendu_Load_1(object sender, EventArgs e)
        {
            DateTime dateActuelle = DateTime.Today;
            afficherRecetteDuJour(dateActuelle);
            btnInfoCommande.Enabled = false;
            dateTimeDebutDashboard.CustomFormat = "yyyy";
        }

        private void dataGridViewDashboard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnInfoCommande.Enabled = true;
            if (e.RowIndex >= 0)
            {

                DataGridViewRow selectedRow = dataGridViewDashboard.Rows[e.RowIndex];

                string idCommande = selectedRow.Cells["id_commande"].Value.ToString();
                id_commande = Convert.ToInt32(idCommande);
            }
        }

        private void btnInfoCommande_Click(object sender, EventArgs e)
        {
            popUp.commandeDetails popup = new popUp.commandeDetails(id_commande);

            // Affichez la fenêtre contextuelle en mode modal (PopUp)
            popup.ShowDialog();

            // Libérez les ressources de la fenêtre contextuelle après qu'elle a été fermée
            popup.Dispose();
        }
    }
}

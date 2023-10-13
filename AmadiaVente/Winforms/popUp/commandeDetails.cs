﻿using System;
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
    public partial class commandeDetails : Form
    {
        //Déclaration Global
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "../../../database.db");

        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;

        int idCommande = 0;

        //Constructeur
        public commandeDetails(int id_commande)
        {
            InitializeComponent();
            idCommande = id_commande;

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

        //Méthodes
        void detailleCommande(int idCmd)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT a.designation, qte_acheter, a.type_article, prix FROM ligneCommande lc INNER JOIN article a ON lc.id_article = a.id_article WHERE id_commande = @idCommande";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@idCommande", idCmd);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        dataGridViewDetail.DataSource = dataTable;
                    }
                }
            }
        }

        //Evénements
        private void commandeDetails_Load(object sender, EventArgs e)
        {
            detailleCommande(idCommande);

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmadiaVente.Winforms.functionality
{
    public partial class achat : Form
    {
        //Déclaration Globale


        //Méthodes (fonctions)
        public achat()
        {
            InitializeComponent();
            dataGridViewPanier.Columns.Add("NomProduit", "Nom Du Produit"); // Colonne pour le nom du produit
            dataGridViewPanier.Columns.Add("Quantite", "Quantité"); // Colonne pour la quantité du produit
            
            dataGridViewPanier.Columns.Add("Prix", "Prix"); // Colonne pour le prix unitaire du produit
        }

        private void disableFunction()
        {
            comboBoxNumeroMembre.Enabled = false;
            comboBoxNomMembre.Enabled = false;
            txtBoxPrix.Enabled = false;
        }

        private void enableFunction()
        {
            comboBoxNumeroMembre.Enabled = true;
            comboBoxNomMembre.Enabled = true;
            txtBoxPrix.Enabled = true;
        }

        //Evénements
        private void achat_Load(object sender, EventArgs e)
        {
            comboBoxMembre.Items.Add("Oui");
            comboBoxMembre.Items.Add("Non");
            txtBoxPU.Text = "300";
            txtBoxPU.Enabled = false;
        }

        private void comboBoxMembre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMembre.SelectedItem != null && comboBoxMembre.SelectedItem.ToString() == "Oui")
            {
                enableFunction();
            }
            else if (comboBoxMembre.SelectedItem != null && comboBoxMembre.SelectedItem.ToString() == "Non")
            {
                disableFunction();
            }
        }
    }
}

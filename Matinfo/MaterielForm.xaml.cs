﻿using Matinfo.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Matinfo
{
    /// <summary>
    /// Logique d'interaction pour MaterielForm.xaml
    /// </summary>
    public partial class MaterielForm : Window
    {
        public Materiel materiel {  get; set; }

        public MaterielForm(Materiel materiel, bool estFormModification)
        {
            InitializeComponent();
            this.DataContext = materiel;
            this.materiel = materiel;
            if (estFormModification)
            {
                this.Title = "Formulaire modification materiel";
                btnConfirmer.Content = "Modifier";
                btnConfirmer.Click -= Button_Click_Ajouter;
                btnConfirmer.Click += Button_Click_Modifier;
            }
        }

        private void Button_Click_Ajouter(object sender, RoutedEventArgs e)
        {
            Materiel materielActuel = new Materiel(materiel.IdMateriel, int.Parse(tbCategorieMateriel.Text), tbCodeBarre.Text, tbNomMateriel.Text, tbRefConstructeur.Text);
            /// si le matériel a bien été créé dans la base, on l'ajoute dans l'application
            if (materielActuel.Create())
            {
                tbCategorieMateriel.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                tbNomMateriel.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                tbCodeBarre.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                tbRefConstructeur.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                DialogResult = true;
            }
        }

        private void Button_Click_Modifier(object sender, RoutedEventArgs e)
        {
            Materiel materielActuel = new Materiel(materiel.IdMateriel, int.Parse(tbCategorieMateriel.Text), tbCodeBarre.Text, tbNomMateriel.Text, tbRefConstructeur.Text);
            /// si le matériel a bien été mis à jour, on le met à jour dans l'application
            if (materielActuel.Update())
            {
                tbCategorieMateriel.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                tbNomMateriel.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                tbCodeBarre.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                tbRefConstructeur.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                DialogResult = true;
            }
        }

        private void Button_Click_Annuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
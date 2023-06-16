﻿using Matinfo.Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Matinfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ApplicationData data = new ApplicationData();
        }

        private void Button_Click_FormPersonnel(object sender, RoutedEventArgs e)
        {
            PersonnelForm formPersonnel = new PersonnelForm();
            formPersonnel.Show();
        }

        private void Button_Click_RemovePersonnel(object sender, RoutedEventArgs e)
        {
            applicationData.LesPersonnels.Remove((Personnel)lvPersonnel.SelectedItem);
        }

        private void Button_Click_FormMateriel_Ajout(object sender, RoutedEventArgs e)
        {
            Materiel materielCree = new Materiel();
            MaterielForm formMateriel = new MaterielForm(materielCree, false);
            formMateriel.Owner = this;
            bool result = (bool)formMateriel.ShowDialog();
            if (result)
            {
                applicationData.LesMateriaux.Add(materielCree);
                applicationData.RefreshAssociationsMateriaux();
            }
        }

        private void Button_Click_FormMateriel_Modif(object sender, RoutedEventArgs e)
        {
            MaterielForm formMateriel = new MaterielForm((Materiel)lvMateriel.SelectedItem, true);
            formMateriel.Owner = this;
            bool result = (bool)formMateriel.ShowDialog();
            if (result)
            {
                lvMateriel.Items.Refresh();
            }
        }

        private void Button_Click_RemoveMateriel(object sender, RoutedEventArgs e)
        {
            applicationData.LesMateriaux[lvMateriel.SelectedIndex].Delete();
            applicationData.LesMateriaux.Remove((Materiel)lvMateriel.SelectedItem);
        }

        private void Button_Click_FormCategorie(object sender, RoutedEventArgs e)
        {
            CategorieForm formCategorie = new CategorieForm();
            formCategorie.Show();
        }

        private void Button_Click_RemoveCategorie(object sender, RoutedEventArgs e)
        {
            lvCategorie.Items.Remove(lvCategorie.SelectedItem);
        }

        private void Button_Click_FormAjoutAttribution(object sender, RoutedEventArgs e)
        {
            AttributionFormAjout formAjoutAttribution = new AttributionFormAjout();
            formAjoutAttribution.Show();
        }

        private void Button_Click_FormModifAttribution(object sender, RoutedEventArgs e)
        {
            AttributionFormModif formModifAttribution = new AttributionFormModif();
            formModifAttribution.Show();
        }

        private void Button_Click_RemoveAttribution(object sender, RoutedEventArgs e)
        {
            lvAttribution.Items.Remove(lvAttribution.SelectedItem);
        }
    }
}
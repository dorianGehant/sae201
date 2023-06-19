﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Matinfo.Metier
{
    public class Materiel : Crud<Materiel>
    {
        /// <summary>
        /// Stocke 6 informations :
        /// 3 chaine : le nom, le codebarre, la réference constructeur
        /// 2 entier : l'id de la catégorie et l'id du matériel
        /// </summary>
        private int idMateriel;
        private int idCategorie;        
        private string nom;
        private string codeBarre;
        private string referenceConstructeur;
        private ObservableCollection<Attribution> lesAttributions;
        private CategorieMateriel uneCategorie;

        public Materiel()
        {

        }

        public Materiel(int idMateriel, int idCategorie, string codeBarre, string nom, string referenceConstructeur)
        {
            this.IdMateriel = idMateriel;
            this.IdCategorie = idCategorie;
            this.CodeBarre = codeBarre;
            this.Nom = nom;
            this.ReferenceConstructeur = referenceConstructeur;
        }

        public string CodeBarre
        {
            /// <summary>
            /// Obtient ou définit le code barre
            /// </summary>
            get
            {
                return this.codeBarre;
            }

            set
            {
                this.codeBarre = value;
            }
        }

        public string Nom
        {
            /// <summary>
            /// Obtient ou définit le nom
            /// </summary>
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public string ReferenceConstructeur
        {
            /// <summary>
            /// Obtient ou définit la référence constructeur
            /// </summary>
            get
            {
                return this.referenceConstructeur;
            }

            set
            {
                this.referenceConstructeur = value;
            }
        }

        public int IdMateriel
        {
            /// <summary>
            /// Obtient ou définit l'id du matériel
            /// </summary>
            get
            {
                return this.idMateriel;
            }

            set
            {
                this.idMateriel = value;
            }
        }

        public int IdCategorie
        {

            /// <summary>
            /// Obtient ou définit l'id de la catégorie
            /// </summary>
            get
            {
                return this.idCategorie;
            }

            set
            {
                this.idCategorie = value;
            }
        }

        public CategorieMateriel UneCategorie
        {
            /// <summary>
            /// Obtient ou définit la catégorie du matériel
            /// </summary>

            get
            {
                return this.uneCategorie;
            }

            set
            {
                this.uneCategorie = value;
            }
        }

        public ObservableCollection<Attribution> LesAttributions
        {
            /// <summary>
            /// Obtient la base de données des atttributions
            /// </summary>
            get
            {
                return this.lesAttributions;
            }

            set
            {
                this.lesAttributions = value;
            }
        }

        public bool Create()
        {
            /// <summary>
            ///Creation de la catégorie du matériel
            /// </summary>
            /// <returns>Un vrai si la création à bien marché ou un faux si cela n'a pas marché</returns>



            ///verification que le codebarre n'existe pas déjà
            if (this.Read())
            {
                MessageBox.Show("Erreur lors de la création du matériel, le code barre existe déjà, veuillez le changer", "Problème lors de la création", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            ///creation du materiel
            DataAccess accessDB = new DataAccess();
            string requete = string.Format("INSERT INTO materiel(idcategorie, nommateriel, referenceconstructeur, codebarre) VALUES({0}, '{1}', '{2}', '{3}')", + this.IdCategorie, this.Nom, this.ReferenceConstructeur, this.CodeBarre);
            accessDB.SetData(requete);
            return true;
        }

        public void Delete()
        {
            DataAccess accessDB = new DataAccess();
            string requete = "DELETE FROM materiel WHERE \"idmateriel\"=" + this.IdMateriel;
            accessDB.SetData(requete);
        }

        public ObservableCollection<Materiel> FindAll()
        {
            ObservableCollection<Materiel> LesMateriaux = new ObservableCollection<Materiel>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idmateriel, idcategorie, nommateriel, referenceconstructeur, codebarre from materiel ;";
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Materiel m = new Materiel(int.Parse(row["idmateriel"].ToString()), int.Parse(row["idcategorie"].ToString()), (String)row["codebarre"], (String)row["nommateriel"], (String)row["referenceconstructeur"]);
                    LesMateriaux.Add(m);
                }
            }
            return LesMateriaux;
        }

        public ObservableCollection<Materiel> FindBySelection(string criteres)
        {
            ObservableCollection<Materiel> LesMateriaux = new ObservableCollection<Materiel>();
            DataAccess accesBD = new DataAccess();
            String requete = "select idmateriel, idcategorie, nommateriel, referenceconstructeur, codebarre from materiel " + criteres;
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                foreach (DataRow row in datas.Rows)
                {
                    Materiel m = new Materiel(int.Parse(row["idmateriel"].ToString()), int.Parse(row["idcategorie"].ToString()), (String)row["codebarre"], (String)row["nommateriel"], (String)row["referenceconstructeur"]);
                    LesMateriaux.Add(m);
                }
            }
            return LesMateriaux;
        }

        public bool Read()
        {
            DataAccess accesBD = new DataAccess();
            String requete = string.Format("select idmateriel from materiel where codebarre = '{0}'", this.CodeBarre);
            DataTable datas = accesBD.GetData(requete);
            if (datas != null)
            {
                if(datas.Rows.Count > 0)
                {
                    this.IdMateriel = int.Parse(datas.Rows[0]["idmateriel"].ToString());
                    return true;
                }
            }
            return false;
        }

        public bool Update()
        {
            int idMaterielmodifie = this.IdMateriel;
            /// verification que les nouvelles valeurs respectent l'unicité
            if(this.Read())
            {
                MessageBox.Show("Erreur lors de la modification du matériel, le nouveau code barre existe déjà", "Problème lors de la modification", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                this.IdMateriel = idMaterielmodifie;
                DataAccess accesBD = new DataAccess();
                String requete = string.Format("update materiel SET idcategorie = {0}, nommateriel = '{1}', referenceconstructeur = '{2}', codebarre = '{3}' WHERE idmateriel = {4}", this.IdCategorie, this.Nom, this.ReferenceConstructeur, this.CodeBarre, this.IdMateriel);
                accesBD.SetData(requete);
            }
            return true;
        }
    }
}

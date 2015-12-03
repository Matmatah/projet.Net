﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BusinessLogicLayer;
using BusinessLogicLayer.DTO;
using System.Data.Linq;


namespace WindowsCommunicationFoundation
{
    public class ServiceWCFSmartCity : IServiceWCFSmartCity
    {
        #region Connexion

        public PersonneWCF Connexion(string m, string pwd)
        {            
            PersonneDTO p = BLL.Connexion(m, pwd);

            if (p == null)
                return null;
            else
            {
                return new PersonneWCF
                {
                    Mail = p.Mail,
                    Password = p.Password,
                    Nom = p.Nom,
                    Prenom = p.Prenom,
                    Type = p.Type
                };
            }
        }

        #endregion


        #region Gestion des défauts

        public List<DefautWCF> GetAllDefauts()
        {
            List<DefautDTO> listBLL = BLL.SelectAllDefauts();

            if (listBLL == null)
                return null;
            else
            {
                List<DefautWCF> listWCF = new List<DefautWCF>();

                foreach (DefautDTO d in listBLL)
                {
                    DefautWCF dWCF = new DefautWCF();
                    dWCF.IdDefaut = d.IdDefaut;
                    dWCF.Photo = d.Photo;
                    dWCF.Description = d.Description;
                    dWCF.Position = d.Position;
                    listWCF.Add(dWCF);
                }

                return listWCF;
            }
        }

        #endregion


        #region Gestion des interventions

        public List<InterventionWCF> GetAllInterventionsOrderByDate()
        {
            List<InterventionDTO> listBLL = BLL.SelectAllInterventionsOrderByDate();

            if (listBLL == null)
                return null;
            else
            {
                List<InterventionWCF> listWCF = new List<InterventionWCF>();

                foreach (InterventionDTO i in listBLL)
                {
                    InterventionWCF iWCF = new InterventionWCF();
                    iWCF.IdIntervention = i.IdIntervention;
                    iWCF.Etat = i.Etat;
                    iWCF.Commentaire = i.Commentaire;
                    iWCF.DateIntervention = i.DateIntervention;
                    iWCF.Defaut = i.Defaut;
                    iWCF.Personne = i.Personne;
                    listWCF.Add(iWCF);
                }

                return listWCF;
            }
        }

        public InterventionWCF GetInterventionByDefaut(int d)
        {
            InterventionDTO i = BLL.SelectInterventionByDefaut(d);

            if (i == null)
                return null;
            else
            {
                return new InterventionWCF
                {
                    IdIntervention = i.IdIntervention,
                    Etat = i.Etat,
                    Commentaire = i.Commentaire,
                    DateIntervention = i.DateIntervention,
                    Defaut = i.Defaut,
                    Personne = i.Personne
                };
            }
        }

        #endregion


        #region Gestion des personnes

        /*public List<PersonneWCF> GetAllPersonnes()
        {
            return BLL.SelectAllPersonnes().Select
            (
                p => new PersonneWCF
                {
                    Mail = p.Mail,
                    Password = p.Password,
                    Nom = p.Nom,
                    Prenom = p.Prenom,
                    Type = p.Type
                }
            ).ToList();
        }

        public PersonneWCF GetPersonneByMail(string m)
        {
            return BLL.SelectPersonneByMail(m).Select
            (
                p => new PersonneWCF
                {
                    Mail = p.Mail,
                    Password = p.Password,
                    Nom = p.Nom,
                    Prenom = p.Prenom,
                    Type = p.Type
                }
            ).ToList();
        }*/

        #endregion
    }
}

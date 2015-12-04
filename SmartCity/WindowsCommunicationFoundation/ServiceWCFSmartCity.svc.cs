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
                    dWCF.DateDefaut = d.DateDefaut;
                    listWCF.Add(dWCF);
                }

                return listWCF;
            }
        }

        public DefautWCF GetDefautById(int id)
        {
            DefautDTO d = BLL.SelectDefautById(id);

            if (d == null)
                return null;
            else
            {
                return new DefautWCF
                {
                    IdDefaut = d.IdDefaut,
                    Photo = d.Photo,
                    Description = d.Description,
                    Position = d.Position,
                    DateDefaut = d.DateDefaut
                };
            }
        }

        #endregion


        #region Gestion des interventions

        public List<InterventionWCF> GetInterventionsByDefautOrderByDate(int d)
        {
            return null;
            /*List<InterventionDTO> i = BLL.SelectInterventionByDefaut(d);

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
            }*/
        }

        public InterventionWCF GetLastInterventionByDefaut(int d)
        {
            InterventionDTO i = BLL.SelectLastInterventionByDefaut(d);

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
    }
}

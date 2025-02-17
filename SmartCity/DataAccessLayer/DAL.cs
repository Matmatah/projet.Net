﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;


namespace DataAccessLayer
{
    public static class DAL
    {
        private static DataContextDALDataContext instanceDC = null;

        public static DataContextDALDataContext InstanceDC
        {
            get { return instanceDC ?? (instanceDC = new DataContextDALDataContext()); }
        }


        #region SELECT

        public static List<Defaut> SelectAllDefauts()
        {
            return InstanceDC.Defauts.OrderByDescending(d => d.DateDefaut).ThenByDescending(d => d.IdDefaut).ToList();
        }
        
        public static Defaut SelectDefautById(int id)
        {
            return InstanceDC.Defauts.Where(d => d.IdDefaut == id).SingleOrDefault();
        }

        public static List<Intervention> SelectInterventionsByDefautOrderByDate(int d)
        {
            return InstanceDC.Interventions.Where(i => i.IdDefaut == d).OrderByDescending(i => i.DateIntervention).ThenByDescending(i => i.IdIntervention).ToList();
        }

        public static Intervention SelectLastInterventionByDefaut(int d)
        {
            List<Intervention> list = InstanceDC.Interventions.Where(i => i.IdDefaut == d).OrderByDescending(i => i.DateIntervention).ThenByDescending(i => i.IdIntervention).ToList();
            return list[0];
        }

        public static Personne SelectPersonneByMail(string m)
        {
            return InstanceDC.Personnes.Where(p => p.Mail.Equals(m)).SingleOrDefault();
        }

        public static List<Personne> SelectAllOuvriers()
        {
            return InstanceDC.Personnes.Where(p => p.Type.Equals("OUVRIER")).ToList();
        }

        #endregion


        #region INSERT

        public static void AddIntervention(string e, string c, DateTime d, int def, string p)
        {
            Intervention i = new Intervention
            {
                Etat = e,
                Commentaire = c,
                DateIntervention = d,
                IdDefaut = def,
                Mail = p
            };

            InstanceDC.Interventions.InsertOnSubmit(i);
            InstanceDC.SubmitChanges();
        }

        public static int AddDefaut(Binary ph, string descr, string po)
        {
            Defaut d = new Defaut
            {
                Photo = ph,
                Description = descr,
                Position = po
            };

            InstanceDC.Defauts.InsertOnSubmit(d);
            InstanceDC.SubmitChanges();

            return d.IdDefaut;
        }


        public static int AddDefaut(Binary ph, string descr, string po, DateTime t)
        {
            Defaut d = new Defaut
            {
                Photo = ph,
                Description = descr,
                Position = po,
                DateDefaut = t  
            };

            InstanceDC.Defauts.InsertOnSubmit(d);
            InstanceDC.SubmitChanges();

            return d.IdDefaut;
        }

        public static void AddPersonne(string m, string pwd, string n, string pr, string t)
        {
            Personne p = new Personne
            {
                Mail = m,
                Password = pwd,
                Nom = n,
                Prenom = pr,
                Type = t
            };

            InstanceDC.Personnes.InsertOnSubmit(p);
            InstanceDC.SubmitChanges();
        }

        #endregion

    }
}

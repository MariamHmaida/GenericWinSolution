﻿using App.Gwin.Application;
using App.Gwin.Attributes;
using App.Gwin.Entities.Persons;
using App.Gwin.Entities.Secrurity.Autorizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Gwin.Entities.Secrurity.Authentication
{
    [GwinEntity(Localizable = true, isMaleName = true, DisplayMember = "Login")]
    [Menu]
    public class User : Person
    {

        public User() : base()
        {

        }

        // Authentification
        // La création d'un index dans la classe Utilisateur
        // créer un index pour chaque classe fille comme 
        // la classe Stagiaire et Formateur
        // ce qui générer une exéception lors de la Migration automatique 
        // de la base de donénes 
        // parceque les tous les index des tables prend le même nom
        // comme solution il faut nommer l'index selon le type de la classe
        // [Index("LoginIndex"  , IsUnique = true)]
        //[StringLength(450)]

        [DisplayProperty(isInGlossary = true)]
        [EntryForm(GroupeBox = "authentication")]
        public string Login { set; get; }

        [DisplayProperty(isInGlossary = true)]
        [EntryForm(GroupeBox = "authentication")]
        public string Password { set; get; }


        public virtual List<Role> Roles { set; get; }

        public GwinApp.Languages Language { set; get; }



        public Boolean HasAccess(string BusinessEntity, string action)
        {
            if(this.Roles == null) return false;
            if (this.Roles.Any(r => r.Reference == "root")) return true;
            foreach (Role role in this.Roles)
            {
                foreach (Authorization authorization in role.Authorizations)
                {
                    if (authorization.BusinessEntity == BusinessEntity)
                        if (authorization.ActionsNames != null && authorization.ActionsNames.Count > 0)
                        {
                            if (authorization.ActionsNames.Contains(action))
                                return true;
                        }
                        else // Has All Actions 
                        {
                            
                            return true;
                        }
                           

                }
            }
            return false;
        }


    }
}

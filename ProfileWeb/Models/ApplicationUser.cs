﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileWeb.Models
{
    public class ApplicationUser: IdentityUser<int>
    {

        public string Firstname_kz { get; set; } 
        public string Lastname_kz { get; set; }
        public string Middlename_kz { get; set; } 
        public string Firstname_ru { get; set; } 
        public string Lastname_ru { get; set; } 
        public string Middlename_ru { get; set; } 
        public string Firstname_en { get; set; } 
        public string Lastname_en { get; set; } 
        public string Middlename_en { get; set; } 
        public string Firstname_tr { get; set; } 
        public string Lastname_tr { get; set; } 
        public string Middlename_tr { get; set; } 
        public int UsernameChangeLimit { get; set; } 
        public string PhotoUrl { get; set; }
        [Display(Name = "Profile Photo")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }
        //sex
        [ForeignKey("Sex")]//very important
        public int? Sex_ID { get; set; }
        public Sex Sex { get; set; }
        //Kafedra
        [ForeignKey("Kafedra")]//very important
        public int? Department_ID { get; set; } 
        public Kafedra Kafedra { get; set; }
        //Faculties
        [ForeignKey("Faculties")]//very important
        public int? Faculty_ID { get; set; } 
        public Faculties Faculties { get; set; }
        //Ranks
        [ForeignKey("Ranks")]//very important
        public int? Rank_ID { get; set; } 
        public Ranks Ranks  { get; set; }
        public string Address { get; set; }
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        //Degree
         [ForeignKey("Degree")]//very important
        public int? Degree_ID { get; set; }
        public Degree Degree { get; set; }
        public virtual ICollection<Workway> Workways { get; set; }
        public virtual ICollection<Qulification> Qulifications { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }//detail very important

        public virtual ICollection<Research> Researches { get; set; }
    }

}

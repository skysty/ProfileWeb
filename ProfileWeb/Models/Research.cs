using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileWeb.Models
{
    public class Research
    {
        public Research()
        {

        }
        [Key]
        public int Res_Id { get; set; }
        public string KZ_Title { get; set; }
        public string FileUrl { get; set; }
   
        [ForeignKey("ApplicationUser")]//very important
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}

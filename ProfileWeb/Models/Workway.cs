using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileWeb.Models
{
    public class Workway
    {
        public Workway()
        {

        }
        [Key]
        public int Work_Id { get; set; }
        public string TR_Work { get; set; }
        public string KZ_Work { get; set; }
        public string RU_Work { get; set; }
        public string EN_Work { get; set; }
        [ForeignKey("ApplicationUser")]//very important
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

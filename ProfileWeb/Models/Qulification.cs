using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileWeb.Models
{
    public class Qulification
    {
        [Key]
        public int Qu_Id { get; set; }
        public string TR_Qu { get; set; }
        public string KZ_Qu { get; set; }
        public string RU_Qu { get; set; }
        public string EN_Qu { get; set; }
        [ForeignKey("ApplicationUser")]//very important
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

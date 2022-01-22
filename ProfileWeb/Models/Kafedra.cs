using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfileWeb.Models
{
    public class Kafedra
    {
        [Key]
        public int ID { get; set; }
        public string KOD { get; set; }
        public string TIP { get; set; }
        public string TR_AD { get; set; }
        public string KZ_AD { get; set; }
        public string RU_AD { get; set; }
        public string EN_AD { get; set; }
        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}

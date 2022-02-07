using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileWeb.Models
{
    public class Achievement
    {
        public Achievement()
        {

        }
        [Key]
        public int Topic_Id { get; set; }
        public string TR_Topic { get; set; }
        public string KZ_Topic { get; set; }
        public string RU_Topic { get; set; }
        public string EN_Topic { get; set; }
        [ForeignKey("ApplicationUser")]//very important
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

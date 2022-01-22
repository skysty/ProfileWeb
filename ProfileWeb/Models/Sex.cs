using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfileWeb.Models
{
    public class Sex
    {
        [Key]
        public int Sex_ID { get; set; }
        public string Sex_KZ { get; set;}
        public string Sex_EN { get; set; }
        public string Sex_RU { get; set; }
        public string Sex_TR { get; set; }
        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}

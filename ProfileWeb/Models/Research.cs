using Microsoft.AspNetCore.Http;
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
        [Display(Name = "Document")]
        [NotMapped]
        public IFormFile Document { get; set; }

        [ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; private set; } //very important

    }
}

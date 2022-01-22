using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileWeb.Models
{
    public class Research
    {
        public int Res_Id { get; set; }
        public string KZ_Title { get; set; }
        public string FileUrl { get; set; }
        [Required(ErrorMessage = "Please choose the File")]
        [Display(Name = "Document")]
        [NotMapped]
        public IFormFile Document { get; set; }
        [ForeignKey("ApplicationUser")]//very important
        public int Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; private set; } //very important
    }
}

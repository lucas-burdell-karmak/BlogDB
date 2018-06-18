using System.ComponentModel.DataAnnotations;

namespace The_Intern_MVC.Models
{
    public class SearchCriteria
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string SearchString { get; set; }

        public SearchCriteria() => SearchString = "";
    }
}

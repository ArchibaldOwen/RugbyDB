
namespace clubDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class club
    {
        public int id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s\-]*$", ErrorMessage = "Name must begin with a capital letter")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Year Established is required")]
        public int? Established { get; set; }

        [Display(Name = "Club Logo")]
        public string Logo { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Display(Name = "Domestic Titles")]
        public string Trophies { get; set; }

        [Display(Name = "Homepage")]
        public string ClubHomepage { get; set; }

        [Display(Name = "Video Highlight")]
        public string Video { get; set; }

        [Display(Name = "Current Coach")]
        [Required(ErrorMessage = "Current Coach is required")]
        public string CurrentCoach { get; set; }

        public string Stadium { get; set; }

        
        public int? Capacity { get; set; }

        public string Map { get; set; }

        [Display(Name = "Famous Players")]
        public string NotablePlayers { get; set; }

        public string Rivalries { get; set; }

        public int? Cheer { get; set; }
    }
}

using AboutYourHolidays.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AboutYourHolidays.ViewModels.PostModels
{
    public class PostIndexViewModel
    {
        [Display(Name = "Tytuł:")]
        [MaxLength(72)]
        public string Tilte { get; set; }

        [Display(Name = "Opis:")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Kraj:")]
        [MaxLength(50)]
        public string Country { get; set; }
        [Display(Name = "Miasto:")]
        [MaxLength(50)]
        public string City { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
    }
}
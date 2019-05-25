using AboutYourHolidays.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AboutYourHolidays.ViewModels.PostViewModels
{
    public class PostAddViewModel
    {
        [Display(Name = "Tytuł:")]
        [MaxLength(72)]
        public string Title { get; set; }

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
        public string FullName { get; set; }

        public static Post ToDB(PostAddViewModel model)
        {
            return new Post()
            {
                Tilte=model.Title,
                Description=model.Description,
                Country=model.Country,
                City=model.City,
                ImageUrl=model.ImageUrl,
                UserId=model.UserId,
            };
        }

        public static PostAddViewModel FromDB(Post model, HttpPostedFileBase file)
        {
            return new PostAddViewModel()
            {
                Title = model.Tilte,
                Description = model.Description,
                Country = model.Country,
                City = model.City,
                ImageUrl = model.ImageUrl,
                UserId = model.UserId,
            };
        }


    }
}
using AboutYourHolidays.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AboutYourHolidays.ViewModels.PostViewModels
{
    public class PostDetailsModel
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

        public string FullName { get; set; }

        public List<Comment> Comments { get; set; }

        public static explicit operator PostDetailsModel(Post dbPost)
        {
            if (dbPost == null) { return (PostDetailsModel)null; }
            var ps = new PostDetailsModel
            {
                Tilte = dbPost.Tilte,
                Description = dbPost.Description,
                Country = dbPost.Country,
                City = dbPost.City,
                ImageUrl = dbPost.ImageUrl,
                UserId = dbPost.UserId,
                FullName = dbPost.User.FullName
            };

            return ps;
        }
    }
}
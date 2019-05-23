using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AboutYourHolidays.Models
{
    public class Post : BaseEntity
    {
        public Post()
        {
            this.Comment = new HashSet<Comment>();
            this.Ratings = new HashSet<Rating>();
        }

        [Display(Name = "Title:")]
        [MaxLength(72)]
        public string Tilte { get; set; }

        [Display(Name = "Description:")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Address:")]
        [MaxLength(100)]
        public string Address { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AboutYourHolidays.Models
{
    public class Post 
    {
        public Post()
        {
            this.Comment = new HashSet<Comment>();
            this.Ratings = new HashSet<Rating>();
        }
        [System.ComponentModel.DataAnnotations.KeyAttribute()]
        public int Id { get; set; }
        public string Tilte { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ImageUrl { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

    }
}
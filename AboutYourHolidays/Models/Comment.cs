using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AboutYourHolidays.Models
{
    public class Comment : BaseEntity
    {
        public Comment()
        {
        }

        [Display(Name = "Text:")]
        [MaxLength(500)]
        public string Text { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
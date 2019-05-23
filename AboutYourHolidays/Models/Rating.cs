using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AboutYourHolidays.Models
{
    public class Rating : BaseEntity
    {
        public Rating()
        {
        }
        public int RateValue1 { get; set; }
        public int RateValue2 { get; set; }
        public int RateValue3 { get; set; }
        public int RateValue4 { get; set; }
        public int RateValue5 { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
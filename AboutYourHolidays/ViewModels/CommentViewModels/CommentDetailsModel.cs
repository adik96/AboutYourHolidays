using AboutYourHolidays.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AboutYourHolidays.ViewModels.PostViewModels
{
    public class CommentDetailsModel
    {
        [Display(Name = "Opis:")]
        [MaxLength(500)]
        public string Text { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public static explicit operator CommentDetailsModel(Comment dbComm)
        {
            if (dbComm == null) { return (CommentDetailsModel)null; }
            var cm = new CommentDetailsModel
            {
                Text = dbComm.Text,
                UserId = dbComm.UserId,
                User = dbComm.User
            };

            return cm;
        }

        
    }
}
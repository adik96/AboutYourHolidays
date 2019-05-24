using System;
using AboutYourHolidays.Models;
using System.Data.Entity;

namespace AboutYourHolidays.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        private ApplicationDbContext _context;
        public override DbSet<Comment> DataCollection
        {
            get { return _context.Comment; }
        }

        public CommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



    }
}
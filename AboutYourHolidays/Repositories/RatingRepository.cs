using System;
using AboutYourHolidays.Models;
using System.Data.Entity;

namespace AboutYourHolidays.Repositories
{
    public class RatingRepository : BaseRepository<Rating>
    {
        private ApplicationDbContext _context;
        public override DbSet<Rating> DataCollection
        {
            get { return _context.Rating; }
        }
        public RatingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



    }
}
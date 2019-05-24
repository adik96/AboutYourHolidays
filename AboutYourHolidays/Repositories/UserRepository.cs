using System;
using AboutYourHolidays.Models;
using System.Data.Entity;

namespace AboutYourHolidays.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private ApplicationDbContext _context;
        public override DbSet<User> DataCollection
        {
            get { return _context.User; }
        }
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



    }
}
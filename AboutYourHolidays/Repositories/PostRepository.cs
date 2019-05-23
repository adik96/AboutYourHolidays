using System;
using AboutYourHolidays.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AboutYourHolidays.Repositories
{
    public class PostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Post post)
        {
            return true;
        }
    }
}
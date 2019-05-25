﻿using System;
using AboutYourHolidays.Models;
using System.Data.Entity;

namespace AboutYourHolidays.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {
        private ApplicationDbContext _context;
        public override DbSet<Post> DataCollection
        {
            get { return _context.Post; }
        }
        public PostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
        
        
    }
}
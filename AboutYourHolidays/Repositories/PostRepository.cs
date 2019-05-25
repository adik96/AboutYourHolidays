using System;
using AboutYourHolidays.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

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
        public bool Add(Post post)
        {
            _context.Post.Add(post);
            return _context.SaveChanges() > 0;
        }
        public Post Get(int Id)
        {
            return _context.Post
                .SingleOrDefault(p=>p.Id == Id);

        }


    }
}
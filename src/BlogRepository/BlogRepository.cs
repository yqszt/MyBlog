using BlogModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogRepository
{
    public class BlogRepository
    {
        private BlogContext dbcontext;

        public BlogRepository(BlogContext context)
        {
            dbcontext = context;
        }

        public List<Post> GetAll()
        {
            return dbcontext.Posts.ToList();
        }

        public Post GetById(int id)
        {
            return dbcontext.Posts.Find(id);
        }
    }
}

using BlogModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlogRepository.MySQL
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

        public List<Post> GetTop5()
        {
            return dbcontext.Posts.OrderByDescending(p => p.ClickCount).Take(5).ToList();
        }

        public Post GetById(int id)
        {
            return dbcontext.Posts.Find(id);
        }

        public void Update(Post post)
        {
            dbcontext.Entry(post).State = EntityState.Modified;
            dbcontext.SaveChanges();
        }

        public void Insert(Post post)
        {
            dbcontext.Posts.Add(post);
            dbcontext.SaveChanges();
        }

        public void Delete(Post post)
        {
            dbcontext.Posts.Remove(post);
            dbcontext.SaveChanges();
        }
    }
}

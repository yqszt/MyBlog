using BlogModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBusinessLogic
{
    public class BlogManager
    {
        private BlogRepository.MySQL.BlogRepository repository;

        public BlogManager(BlogRepository.MySQL.BlogRepository repository)
        {
            this.repository = repository;
        }

        public List<Post> GetAllPosts()
        {
            return repository.GetAll();
        }

        public Post GetPostById(int id)
        {
            return repository.GetById(id);
        }

        public List<Post> GetTop5()
        {
            return repository.GetTop5();
        }

        public void UpdatePost(Post post)
        {
            repository.Update(post);
        }

        public void Insert(Post post)
        {
            repository.Insert(post);
        }

        public void Delete(Post post)
        {
            repository.Delete(post);
        }
    }
}

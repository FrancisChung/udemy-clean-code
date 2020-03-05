using System.Linq;

namespace Project.UserControls
{
    public class PostRepository
    {
        private readonly PostDbContext _dbContext;

        public PostRepository()
        {
            _dbContext = new PostDbContext();
        }

        public Post GetPost(int postId)
        {
            Post entity = _dbContext.Posts.SingleOrDefault(p => p.Id == postId);
            return entity;
        }

        public void SavePost(Post post)
        {
            // Save to the database and continue to the next page
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
        }
    }
}
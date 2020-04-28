using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PostService
    {
        private readonly Guid _userId;

        public PostService(Guid userId)
        {
            _userId = userId;
        }


        public bool CreatePost(PostCreate model)
        {
            var entity =
                new Post()
                {
                    Author = _userId,
                    Title = model.Title,
                    Text = model.Content,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Posts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PostListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Posts
                    .Where(e => e.Author == _userId)
                    .Select(
                        e =>
                            new PostListItem
                            {
                                PostId = e.Id,
                                Title = e.Title,
                                CreatedUtc = e.CreatedUtc
                            }
                        );
                return query.ToArray();
            }
        }
    }
}

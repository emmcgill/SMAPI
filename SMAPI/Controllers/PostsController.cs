using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SMAPI.Models;
using Data;

namespace SMAPI.Controllers
{
    public class SocialController : ApiController
    {
        private ApplicationDbContext db;
        public SocialController()
        {
            db = new ApplicationDbContext();
            db.Configuration.LazyLoadingEnabled = false;
        }
        // GET: api/Posts
        [HttpGet]
        public IEnumerable<Post> Posts()
        {
            return db.Posts.ToList();
        }

        // GET: api/PostComments
        [HttpGet]
        public IEnumerable<Comment> PostComments(int id)
        {
            return db.Posts.Include(x => x.Comments).FirstOrDefault(x => x.Id == id).Comments.Select(c =>
                  new Comment
                  {
                      Id = c.Id,
                      Text = c.Text,
                      AuthorId = c.AuthorId,
                      CommentPostId = c.CommentPostId,
                  });
        }

        // GET: api/CommentReplies
        [HttpGet]
        public IEnumerable<Reply> CommentReplies(int id)
        {
            return db.Comments.Include(x => x.Replies).FirstOrDefault(x => x.Id == id).Replies.Select(c =>
                  new Reply
                  {
                      Id = c.Id,
                      Text = c.Text,
                      AuthorId = c.AuthorId,
                      CommentId = c.CommentId,
                  });
        }

        // POST: api/Posts
        [HttpPost]
        [ResponseType(typeof(Post))]
        public async Task<IHttpActionResult> Post([FromBody]Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Posts.Add(post);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/PostComment/
        [HttpPost]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostComment([FromBody]Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(comment);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/PostComment/
        [HttpPost]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostCommentReply([FromBody]Reply reply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Replies.Add(reply);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
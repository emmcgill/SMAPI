using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class APIComment
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        [ForeignKey("CommentPost")]
        public int CommentPostId { get; set; }
        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public Post CommentPost { get; set; }

    }
    public class Comment : APIComment
    {
        public virtual DbSet<Reply> Replies { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        [ForeignKey("Post")]
        public int Id { get; set; }
        public virtual Post Post { get; set; }
        [ForeignKey("Author")]
        public Guid UserId { get; set; }
        public virtual Author Author { get; set; }
        
    }
}

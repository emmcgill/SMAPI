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
        public int Id { get; set; }
        [ForeignKey("LikedPost")]
        public int PostId { get; set; }

        [ForeignKey("Liker")]
        public string LikerId { get; set; }
        public Post LikedPost { get; set; }
        public ApplicationUser Liker { get; set; }
    }
}

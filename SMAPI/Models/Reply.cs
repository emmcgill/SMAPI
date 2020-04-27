using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMAPI.Models
{
{
    public class Reply : APIComment
    {
        [ForeignKey("ReplyComment")]
        public int CommentId { get; set; }
        public Comment ReplyComment { get; set; }
    }
}
}
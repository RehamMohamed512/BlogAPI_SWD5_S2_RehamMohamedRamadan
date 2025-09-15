using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models
{
    public class Comment
    {

        [Key]
       public int Id { get; set; }

        [Required(ErrorMessage ="Content is required")]
        [StringLength(200, ErrorMessage ="Content can't be longer than 200 characters")]
        public string Content { get; set; }

       public DateTime? CreatedAt { get; set; }

        //Relations

        //Post relation
        [ForeignKey("MyPost")]
        public int PostId { get; set; }
        public Post MyPost { get; set; }

        // user relation
        [ForeignKey("MyUser")]
        public int UserId { get; set; }
        public User MyUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models
{
    public class Post
    {
        [Key] //pk
        public int Id { get; set; }

        [Required(ErrorMessage ="Title is Required")]
        [MaxLength(100, ErrorMessage ="Title cannot exceed 100 characters")]
        [MinLength(5, ErrorMessage ="Title must be at least 5 characters long")]

        public string Title { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [MinLength(20, ErrorMessage = "Content must be at least 20 characters long")]
        [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters")]

        public string Content { get; set; }


        public DateTime? CreatedAt { get; set; } = DateTime.Now; //accept null value

        //Relation

        //user id foreign key
        [ForeignKey ("MyUser")]
        public int UserId { get; set; }
        public User MyUser { get; set; }

        //categoryid
        [ForeignKey("MyCategory")]
        public int CategoryId { get; set; }
        public Category MyCategory { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();



    }
}

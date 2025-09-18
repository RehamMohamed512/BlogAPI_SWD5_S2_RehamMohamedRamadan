using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage ="UserName is required")]
        [MaxLength(50, ErrorMessage ="UserName max length is 50 characters")]
        [MinLength(3, ErrorMessage ="UserName min length is 3 characters")]

        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(100, ErrorMessage = "Email max length is 100 characters")]
        [MinLength(5, ErrorMessage = "Email min length is 5 characters")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        //[RegularExpression("")]
        [DataType(DataType.Password)]
        public string Password { get; set; }//hashed password

        //Relation

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

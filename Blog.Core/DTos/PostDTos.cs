using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.DTos
{
    public class PostDTos
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters long")]

        public string Title { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [MinLength(20, ErrorMessage = "Content must be at least 20 characters long")]
        [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters")]

        public string Content { get; set; }


        

        //Relation

        //user id foreign key
        [ForeignKey("MyUser")]
        public int UserId { get; set; }
 

        //categoryid
        [ForeignKey("MyCategory")]
        public int CategoryId { get; set; }



    }
}

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
    public class CommentDTos
    {


            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "Content is required")]
            [StringLength(200, ErrorMessage = "Content can't be longer than 200 characters")]
            public string Content { get; set; }



            //Relations

            //Post relation

            public int PostId { get; set; }


            // user relation
     
            public int UserId { get; set; }

        }
    }


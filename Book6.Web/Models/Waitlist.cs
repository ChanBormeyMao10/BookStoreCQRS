using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book6.Web.Models
{
    public class Waitlist
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public BookViewModel Book { get; set; }
        public UserViewModel User { get; set; }
    }
}
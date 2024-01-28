using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book6.Web.Models
{
    public class UserViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required] //Id is required cause it is primary key

        public string Name { get; set; }
    }
}
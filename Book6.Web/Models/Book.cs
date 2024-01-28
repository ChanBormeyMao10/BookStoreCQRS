using System;
using System.ComponentModel.DataAnnotations;


namespace Book6.Web.Models
{
    public class BookViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required] //Id is required cause it is primary key

        public string Title { get; set; }

        //check for if the book is reserved
        public bool IsReserved { get; set; }
        public bool IsInWaitlist { get; set; }
    }
}
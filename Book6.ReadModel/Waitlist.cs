using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModel
{
    public class Waitlist
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId
        { get; set; }

        public Waitlist()
        {
            //User = user;
            //Book = book;
        }
    }
}

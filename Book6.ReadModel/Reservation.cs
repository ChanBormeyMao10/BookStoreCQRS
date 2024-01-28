using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModel
{
    public class Reservation
    {
        public System.Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }
        public Reservation()
        {
            //Book = book;
            //User = user;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModel
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string author { get; set; }
        public bool IsReserved { get; set; }
        public bool IsInWaitlist { get; set; }

        public Book()
        {
            //Id = id;
            //Title = title;
            //IsReserved = false;
            //IsInWaitlist = false;
        }
    }
}

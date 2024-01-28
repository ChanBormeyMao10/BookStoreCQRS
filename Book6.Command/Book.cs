using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6
{
    public class NewBook : IMessage
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string author { get; set; }
        public bool IsReserved { get; set; }
        public bool IsInWaitlist { get; set; }
    }
    public class UpdateBook : IMessage
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
    public class DeleteBook : IMessage
    {
        public Guid Id { get; set; }
    }
    public class UpdateBookReservationStatus : IMessage
    {
        public Guid Id { get; set; }
    }
    public class UpdateBookWaitlistStatus : IMessage
    {
        public Guid Id { get; set; }
    }
}

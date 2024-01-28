using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6
{
    public class NewWaitlist : IMessage
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId
        { get; set; }
    }
    public class RemoveWaitlist : IMessage
    {
        public System.Guid Id { get; set; }
    }
}

using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.Domain
{
    public class Waitlist : AggregateBase
    {
        //public readonly List<Book6.Common.Waitlist> _allWaitlist;

        public Waitlist()
        {
            //_allWaitlist = new List<Book6.Common.Waitlist>();
        }
        public Waitlist(NewWaitlist msg) : this()
        {
            RaiseEvent(msg);
        }
        public void RemoveWaitlist(RemoveWaitlist msg)
        {
            if (msg.Id != Id)
            {
                throw new DomainAggregateException(this, "Waitlist doesn't exist");
            }
            RaiseEvent(msg);
        }
        //__________________________________________________________________________________________

        private void Apply(NewWaitlist e)
        {
            Id = e.Id;

        }
        private void Apply(RemoveWaitlist e)
        {
            Id = e.Id;
        }

    }
}

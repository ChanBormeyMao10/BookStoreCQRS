using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModel.Handler
{
    public class WaitlistHandler : IHandleEvent<NewWaitlist>, IHandleEvent<RemoveWaitlist>
    {
        private readonly IReadModelSession session;

        public WaitlistHandler(IReadModelSession session)
        {
            this.session = session;
        }
        public void Handle(NewWaitlist e)
        {
            var wait = new Waitlist
            {
                Id = e.Id,
                UserId = e.UserId,
                BookId = e.BookId,
            };
            session.Store(wait);
        }
        public void Handle(RemoveWaitlist e)
        {
            var wait = session.Load<Waitlist>(e.Id);
            session.Delete(wait);
        }
    }
}

using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModel.Handler
{
    public class ReservationHandler : IHandleEvent<NewReservation>, IHandleEvent<ReturnReservation>
    {
        private readonly IReadModelSession session;

        public ReservationHandler(IReadModelSession session)
        {
            this.session = session;
        }
        public void Handle(NewReservation e)
        {
            var R = new Reservation
            {
                Id = e.Id,
                BookId = e.BookId,
                UserId = e.UserId,
                EmployeeId = e.EmployeeId,
            };
            session.Store(R);
        }
        public void Handle(ReturnReservation e)
        {
            var R = session.Load<Reservation>(e.Id);
            session.Delete(R);
        }
    }
}

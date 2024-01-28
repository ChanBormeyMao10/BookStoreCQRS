using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.Domain
{
    public class Reservation : AggregateBase
    {


        public Reservation()
        {

        }
        public Reservation(NewReservation msg) : this()
        {
            RaiseEvent(msg);
        }
        public void ReturnReservation(ReturnReservation msg)
        {
            if (msg.Id != Id)
            {
                throw new DomainAggregateException(this, "ID doesn't exist");
            }
            RaiseEvent(msg);
        }
        //____________________________________________________________________________________________
        private void Apply(NewReservation e)
        {
            Id = e.Id;
        }
        private void Apply(ReturnReservation e)
        {
            Id = e.Id;
        }
    }
}

using Book6.Domain;
using CRG.ES;
using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.CommandHandler
{
    public class ReservationHandler : IHandleCommand<NewReservation>, IHandleCommand<ReturnReservation>
    {
        private readonly IRepository repository;
        public ReservationHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public CommandResult Handle(NewReservation c)
        {
            var r = new Domain.Reservation(c);
            return new CommandResult(repository.Save(r));
        }
        public CommandResult Handle(ReturnReservation c)
        {
            var r = repository.GetById<Reservation>(c.Id);
            r.ReturnReservation(c);
            return new CommandResult(repository.Save(r));
        }
    }
}

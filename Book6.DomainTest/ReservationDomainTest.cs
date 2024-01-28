using Book6.CommandHandler;
using Book6.Domain;
using MM.ES;
using MM.ES.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.DomainTest
{

    [TestFixture]
    public class ReservationDomainTest : AggregateTestFixture<Reservation>
    {
        [TearDown]
        public void Teardown()
        { }

        private ReservationHandler commandHandler;
        private Guid id;
        private List<IMessage> givenReservations;
        protected override Dictionary<Guid, IEnumerable<IMessage>> Given()
        {
            id = Guid.NewGuid();
            givenReservations = new List<IMessage>
                {
                    new NewReservation
                    {
                        Id= id,
                        BookId = Guid.NewGuid(),
                        UserId = Guid.NewGuid(),
                    }
                };

            return new Dictionary<Guid, IEnumerable<IMessage>> { { id, givenReservations } };
        }

        protected override void SetupCommandHandler(IRepository repository)
        {
            commandHandler = new ReservationHandler(repository);
        }

        [Test]
        public void NewReservation()
        {
            var command = new NewReservation()
            {
                Id = Guid.NewGuid(),
                BookId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            };
            Then.Add(command.Id, new IMessage[] { command });
            ProcessCommand(commandHandler, command);
        }

        [Test]
        public void ReturnReservation()
        {
            var command = new ReturnReservation()
            {
                Id = id,
            };
            Then.Add(command.Id, new IMessage[] { command });
            ProcessCommand(commandHandler, command);
        }
        [Test]
        public void ReturnReservationNotSameId()
        {
            var command = new ReturnReservation()
            {
                Id = Guid.NewGuid(),
            };
            Then.Add(command.Id, new IMessage[] { command });
            ProcessExceptionCommand(commandHandler, command);
        }

    }

}

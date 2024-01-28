using MM.ES.Testing;
using MM.ES;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book6.Domain;
using Book6.CommandHandler;

namespace Book6.DomainTest
{
    [TestFixture]
    public class WaitlistDomainTest : AggregateTestFixture<Waitlist>
    {
        [TearDown]
        public void Teardown()
        { }

        private WaitlistHandler commandHandler;
        private Guid id;
        private List<IMessage> givenWaitlists;
        protected override Dictionary<Guid, IEnumerable<IMessage>> Given()
        {
            id = Guid.NewGuid();
            givenWaitlists = new List<IMessage>
                {
                    new NewWaitlist
                    {
                        Id = id,
                        BookId = Guid.NewGuid(),
                        UserId = Guid.NewGuid(),
                    }
                };

            return new Dictionary<Guid, IEnumerable<IMessage>> { { id, givenWaitlists } };
        }

        protected override void SetupCommandHandler(IRepository repository)
        {
            commandHandler = new WaitlistHandler(repository);
        }

        [Test]
        public void NewWaitlist()
        {
            var command = new NewWaitlist()
            {
                Id = Guid.NewGuid(),
                BookId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            };
            Then.Add(command.Id, new IMessage[] { command });
            ProcessCommand(commandHandler, command);
        }

        [Test]
        public void RemoveWaitlist()
        {
            var command = new RemoveWaitlist()
            {
                Id = id,
            };
            Then.Add(command.Id, new IMessage[] { command });
            ProcessCommand(commandHandler, command);
        }

    }

}

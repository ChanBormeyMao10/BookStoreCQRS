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
    public class UserDomainTest : AggregateTestFixture<User>
    {
        [TearDown]
        public void Teardown()
        { }

        private UserHandler commandHandler;
        private Guid id;
        private List<IMessage> givenUsers;
        protected override Dictionary<Guid, IEnumerable<IMessage>> Given()
        {
            id = Guid.NewGuid();
            givenUsers = new List<IMessage>
                {
                    new NewUser
                    {
                        Id= id,
                        Name= "TestName"
                    }
                };

            return new Dictionary<Guid, IEnumerable<IMessage>> { { id, givenUsers } };
        }
        protected override void SetupCommandHandler(IRepository repository)
        {
            commandHandler = new UserHandler(repository);
        }

        [Test]
        public void NewUser()
        {
            var command = new NewUser()
            {
                Id = Guid.NewGuid(),
                Name = "TestName"
            };
            Then.Add(command.Id, new IMessage[] { command });
            ProcessCommand(commandHandler, command);
        }
        [Test]
        public void UpdateUser()
        {
            var command = new UpdateUser()
            {
                Id = id,
                Name = "Test Name Updated"
            };
            Then.Add(command.Id, new IMessage[] { command });
            ProcessCommand(commandHandler, command);
        }
        [Test]
        public void UpdateUserSameUserName()
        {
            var command = new UpdateUser()
            {
                Id = id,
                Name = "TestName"
            };
            Then.Add(command.Id, new IMessage[] { command });
            ProcessExceptionCommand(commandHandler, command);
        }
        [Test]
        public void DeleteUser()
        {
            var command = new DeleteUser()
            {
                Id = id,
            };
            Then.Add(command.Id, new IMessage[] { command });
            ProcessCommand(commandHandler, command);
        }


    }
}

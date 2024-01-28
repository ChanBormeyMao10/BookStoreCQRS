using Book6.ReadModel;
using Book6.ReadModel.Handler;
using MM.ES;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModelTest
{
    [TestFixture]
    public class UserHandlerTest
    {
        private IReadModelSession session;
        private UserHandler handler;
        private Guid UserId;
        private User User;

        [TearDown]
        public void Teardown()
        { }
        [SetUp]
        public void Setup()
        {
            session = Global.ReadModelSession;
            handler = new UserHandler(session);
            UserId = Guid.NewGuid();
            User = new User
            {
                Id = UserId,
            };
            session.Store(User);
        }
        [Test]
        public void NewUser()
        {
            //Arrange
            var e = new NewUser
            {
                Id = Guid.NewGuid(),
               Name= "TestName",
            };
            //Action
            handler.Handle(e);
            var exp = session.Load<User>(e.Id);

            //Assert
            Assert.AreEqual(e.Id, exp.Id);
            Assert.AreEqual(e.Name, exp.Name);        
        }
        [Test]
        public void UpdateUser()
        {
            //Arrange
            session.Store(User);

            var e = new UpdateUser()
            {
                Id = UserId,
                Name = "Updated Name"
            };

            //Action
            handler.Handle(e);
            var exp = session.Load<User>(UserId);

            //Assert
            Assert.NotNull(exp);
            Assert.AreEqual(e.Name, exp.Name);
        }
        [Test]
        public void DeleteUser()
        {
            //Arrange
            session.Store(User);

            var e = new DeleteUser()
            {
                Id = UserId,
            };

            //Action
            handler.Handle(e);
            var exp = session.Load<User>(UserId);

            //Assert
            Assert.IsNull(exp);

        }
        
    }
}

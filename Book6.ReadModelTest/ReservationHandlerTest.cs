using Book6.ReadModel;
using Book6.ReadModel.Handler;
using MM.ES;
using NUnit.Framework;
using System;

namespace Book6.ReadModelTest
{
    public class ReservationHandlerTest
    {
        private IReadModelSession session;
        private ReservationHandler handler;
        private Guid rId;
        private Reservation reserve;

        [TearDown]
        public void Teardown()
        { }
        [SetUp]
        public void Setup()
        {
            session = Global.ReadModelSession;
            handler = new ReservationHandler(session);
            rId = Guid.NewGuid();
            reserve = new Reservation
            {
                Id = rId,
                BookId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            };
            session.Store(reserve);
        }
        [Test]
        public void NewReservation()
        {
            //Arrange
            var e = new NewReservation
            {
                Id = Guid.NewGuid(),
                BookId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            };
            //Action
            handler.Handle(e);
            var exp = session.Load<Reservation>(e.Id);

            //Assert
            Assert.AreEqual(e.Id, exp.Id);
            Assert.AreEqual(e.BookId, exp.BookId);
            Assert.AreEqual(e.UserId, exp.UserId);

        }
        [Test]
        public void ReturnReservation()
        {
            //Arrange
            session.Store(reserve);


            var e = new ReturnReservation()
            {
                Id = rId,
            };

            //Action
            handler.Handle(e);
            var exp = session.Load<Reservation>(e.Id);

            //Assert
            Assert.IsNull(exp);
        }
    }
}

using Book6.ReadModel;
using Book6.ReadModel.Handler;
using MM.ES;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModelTest
{
    [TestFixture]
    public class BookHandlerTests
    {
        private IReadModelSession session;
        private BookHandler handler;
        private Guid BookId;
        private Book book;

        [TearDown]
        public void Teardown()
        { }
        [SetUp]
        public void Setup()
        {
            session = Global.ReadModelSession;
            handler = new BookHandler(session);
            BookId = Guid.NewGuid();
            book = new Book
            {
                Id = BookId,
            };
            session.Store(book);
        }
        [Test]
        public void NewBook()
        {
            //Arrange
            var e = new NewBook
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                IsInWaitlist = false,
                IsReserved = false,
            };
            //Action
            handler.Handle(e);
            var exp = session.Load<Book>(e.Id);

            //Assert
            Assert.AreEqual(e.Id, exp.Id);
            Assert.AreEqual(e.Title, exp.Title);
            Assert.AreEqual(e.IsInWaitlist, exp.IsInWaitlist);
            Assert.AreEqual(e.IsReserved, exp.IsReserved);
        }
        [Test]
        public void UpdateBook()
        {
            //Arrange
            session.Store(book);

            var e = new UpdateBook()
            {
                Id = BookId,
                Title = "Updated Title"
            };

            //Action
            handler.Handle(e);
            var exp = session.Load<Book>(BookId);

            //Assert
            Assert.NotNull(exp);
            Assert.AreEqual(e.Title, exp.Title);
        }
        [Test]
        public void DeleteBook()
        {
            //Arrange
            session.Store(book);

            var e = new DeleteBook()
            {
                Id = BookId,
            };

            //Action
            handler.Handle(e);
            var exp = session.Load<Book>(BookId);

            //Assert
            Assert.IsNull(exp);

        }
        [Test]
        public void UpdateBookWaitlistStatus()
        {
            //Arrange
            session.Store(book);

            var e = new UpdateBookWaitlistStatus()
            {
                Id = BookId,
            };

            //Action
            handler.Handle(e);
            var exp = session.Load<Book>(BookId);

            //Assert
            Assert.NotNull(exp);
            Assert.IsTrue(exp.IsInWaitlist);
        }
        [Test]
        public void UpdateBookReservationStatus()
        {
            //Arrange
            session.Store(book);

            var e = new UpdateBookReservationStatus()
            {
                Id = BookId,
            };

            //Action
            handler.Handle(e);
            var exp = session.Load<Book>(BookId);

            //Assert
            Assert.NotNull(exp);
            Assert.IsTrue(exp.IsReserved);
        }
    }
}

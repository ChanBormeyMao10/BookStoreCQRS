using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Book6.ReadModel;
using CRG.ES.CommandProcessor;
using MM.FileStore;
using NLog;
using Raven.Client;

namespace Book6.Web.Views
{
    public class ReservationController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ICommandProcessingUnit cpu;

        private readonly IDocumentSession session;

        public ReservationController(IDocumentSession session, ICommandProcessingUnit cpu)
        {
            this.session = session;
            this.cpu = cpu;

        }
        public ActionResult Index()
        {
            var reservations = session.Query<Reservation>().ToList();
            Dictionary<Guid, string> book = new Dictionary<Guid, string>();
            Dictionary<Guid, string> user = new Dictionary<Guid, string>();
            Dictionary<Guid, string> employee = new Dictionary<Guid, string>();

            foreach (var reservation in reservations)
            {

                book.Add(reservation.BookId, session.Load<Book>(reservation.BookId).Title);
                user.Add(reservation.UserId, session.Load<User>(reservation.UserId).Name);
                employee.Add(reservation.EmployeeId, session.Load<Employee>(reservation.EmployeeId).Name);
            }
            ViewBag.bookReservation = book;
            ViewBag.userReservation = user;
            ViewBag.EmployeeReservation = employee;
            return View(reservations);
        }
        public ActionResult Create(Guid? BookId, Guid? UserId, Guid? EmployeeId)
        {
            cpu.Process(new NewReservation
            {
                Id = Guid.NewGuid(),
                BookId = (Guid)BookId,
                UserId = (Guid)UserId,
                EmployeeId = (Guid)EmployeeId
            });
            return RedirectToAction("UpdateReserve", "Book", new { Id = BookId });
        }
        public ActionResult Return(ReturnReservation m)
        {
            var reservation = session.Load<Reservation>(m.Id);
            var BookId = session.Load<Book>(reservation.BookId);
            cpu.Process(new ReturnReservation
            {
                Id = m.Id,
            });
            return RedirectToAction("UpdateReserve", "Book", new { Id = BookId });
        }
    }
}

using Book6.ReadModel;
using CRG.ES.CommandProcessor;
using MM.FileStore;
using NLog;
using Raven.Client;
using Raven.Client.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book6.Web.Controllers
{
    public class UserController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ICommandProcessingUnit cpu;


        private readonly IDocumentSession session;

        public UserController(IDocumentSession session, ICommandProcessingUnit cpu)
        {
            this.session = session;
            this.cpu = cpu;
        }
        public ActionResult Index()
        {
            var users = session.Query<ReadModel.User>().Take(1024).ToList();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(NewUser m)
        {
            cpu.Process(new NewUser
            {
                Id = Guid.NewGuid(),
                Name = m.Name,
            });
            return RedirectToAction("Index");
        }

        public ActionResult Update(Guid? Id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(UpdateUser m)
        {

            //var book = session.Load<Book>(m.Id);
            cpu.Process(new UpdateUser
            {
                Id = m.Id,
                Name = m.Name,

            });
            return RedirectToAction("Index");
        }

        public ActionResult Delete(DeleteUser m)
        {
            cpu.Process(new DeleteUser
            {
                Id = m.Id,
            });
            return RedirectToAction("Index");
        }

    }
}
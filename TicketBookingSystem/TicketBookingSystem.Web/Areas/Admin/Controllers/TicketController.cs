using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Web.Areas.Admin.Models;
using TicketBookingSystem.Web.Models;

namespace TicketBookingSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> _loger;
        public TicketController(ILogger<TicketController> loger)
        {
            _loger = loger;
        }
        public IActionResult Index()
        { 
            var model = new TicketListModel();
            model.LoadModelData();
            return View(model);
        }

        
        public JsonResult GetTickets()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new TicketListModel();
            var data = model.GetTickets(dataTablesModel);
            return Json(data);

        }
        

        public IActionResult Create()
        {
            var model = new CreateTicketModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateTicketModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateTicket();
                }
                catch (Exception ex)
                {
                    ModelState.TryAddModelError("", "Failed to create ticket");
                    _loger.LogError(ex, "Create Ticket Failed");
                }
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            var model = new EditTicketModel();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditTicketModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = new TicketListModel();
            model.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

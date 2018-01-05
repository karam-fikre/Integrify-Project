using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MBotRangerCore.Controllers
{
    public class TempController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string SubmitSubscription(string Name, string Address)
        {
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Address))
                //TODO: Save the data in database
                return "Thank you " + Name + ". Record Saved.";
            else
                return "Please complete the form.";

        }     
    }
}
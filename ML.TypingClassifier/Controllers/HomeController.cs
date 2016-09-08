﻿using ML.TypingClassifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ML.TypingClassifier.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult Results()
		{
			return View();
		}

        [HttpPost]
        
        public ActionResult KeySink(Timeline data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}
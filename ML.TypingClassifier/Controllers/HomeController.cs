using ML.TypingClassifier.Models;
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
            var sentinel = new Sentinel
            {
                Author = "Charles Dickens",
                Book = "A Tale of Two Cities",
                Text = "It was the best of times, it was the worst of times..."
            };
            return View(sentinel);
		}

        public ActionResult Woolf()
        {
            var text = new Sentinel
            {
                Author = "Virginia Woolf",
                Book = "The Waves",
                Text = 
                    "The sun had not yet risen. The sea was indistinguishable from the sky, except that the sea was slightly creased as if a cloth " +
                    "had wrinkles in it. Gradually as the sky whitened a dark line lay on the horizon diving the sea from the sky and the grey cloth became barred " +
                    "with thick strokes moving, one after another, beneath the surface, following each other, pursuing each other, perpetually. As they neared " + 
                    "the shore each bar rose, heaped itself, broke and swept a thin veil of white water across the sand. The wave paused, and then drew out again, sighing " + 
                    "like a sleeper whose breath comes and goes unconsciously."
            };
            return View("Index", text);
        }

        public ActionResult Proust()
        {
            var text = new Sentinel
            {
                Author = "Marcel Proust",
                Book = "In Search of Lost Time",
                Text =
                    "For a long time I would go to bed early. Sometimes, the candle barely out, my eyes closed so quickly that I did not have " +
                    "time to tell myself: \"I'm falling asleep.\" And half an hour later the thought that it was time to look for sleep would awaken " +
                    "me; I would make as if to put away the book which I imagined was still in my hands, and to blow out the light; I had gone on " +
                    "thinking, while I was asleep, about what I had just been reading, but these thoughts had taken a rather peculiar turn; it seemed " +
                    "to me that I myself was the immediate subect of my book: a church, a quartet, the rivalry between Francois I and Charles V. This " +
                    "impression would persist for some moments after I awoke; it did not offend my reason, but lay like scales up on my eyes and prevented " +
                    "them from registering the fact that the candle was no longer burning."
            };

            return View("Index", text);
        }

        public ActionResult Nietzsche()
        {
            var text = new Sentinel
            {
                Author = "Friedrich Nietzsche",
                Book = "Thus Spoke Zarathustra",
                Text =
                    "When Zarathustra was thirty years old he left his home and the lake of his home and went into the mountains. Here he enjoyed his spirit " +
                    "and his solitude, and for ten years did not tire of it. But at last a change came over his heart, and one morning he rose with the dawn, " +
                    "stepped before the sun, and spoke to it thus: \"You great star, what would your happiness be had you not those for whom you shine? \"For " +
                    "ten years you have climbed to my cave: you would have tired of your light and of the journey had it not been for me and my eagle and my serpent."
            };

            return View("Index", text);
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
	}
}
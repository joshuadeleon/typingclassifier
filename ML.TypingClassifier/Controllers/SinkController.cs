using ML.TypingClassifier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ML.TypingClassifier.Controllers
{
    [RoutePrefix("sink")]
    public class SinkController : ApiController
    {

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Route("")]
        public IHttpActionResult Post(Timeline data)
        {
            return Ok(data);
        }
    }
}

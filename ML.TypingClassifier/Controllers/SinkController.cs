using ML.TypingClassifier.Models;
using System;
using System.Collections.Concurrent;
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
        private static readonly string ConnString =
            "Server=tcp:zg8hk2j3i5.database.windows.net,1433;Database=typingcAFGz4D1Xe;User ID=classifier@zg8hk2j3i5;Password=M3talt0ad;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";

        private readonly DataAccess _dataAccess;

        public SinkController()
        {
            _dataAccess = new DataAccess(ConnString);
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_dataAccess.All());
        }

        [Route("{email}")]
        public IHttpActionResult GetByEmail(string email)
        {
            var sample = _dataAccess.Single(email);
            if (sample == null)
                return NotFound();
            return Ok(sample);
        }

        [Route("")]
        public IHttpActionResult Post(Sample data)
        {
            _dataAccess.Add(data);
            return Ok();
        }
    }
}

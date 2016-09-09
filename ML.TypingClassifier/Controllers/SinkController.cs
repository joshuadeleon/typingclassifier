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
        private static readonly ConcurrentDictionary<string, double[]> _map =
            new ConcurrentDictionary<string, double[]>();

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Route("")]
        public IHttpActionResult Post(Timeline data)
        {
            double[] features = FeatureExtractor.Default(data);
            
            return Ok(data);
        }
    }
}

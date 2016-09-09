using ML.TypingClassifier.ML;
using ML.TypingClassifier.Models;
using System.Web.Http;

namespace ML.TypingClassifier.Controllers
{
    [RoutePrefix("sink")]
    public class SinkController : ApiController
    {
        private readonly DataAccess _dataAccess;

        public SinkController()
        {
            _dataAccess = new DataAccess(Constants.ConnectionString);
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
            return Ok(data);
        }

        [Route("kmeans/{clusters}")]
        public IHttpActionResult Get(int clusters)
        {
            var classification = Engine.Instance.RunKMeans(clusters);
            return Ok(classification);
        }

        [Route("kmeans")]
        public IHttpActionResult Post(int clusters)
        {
            var classification = Engine.Instance.RunKMeans(clusters);
            return Ok(classification);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CHR_pathfindingAPI.Controllers
{
    [Route("api/[controller]/{entry}")]
    [ApiController]
    public class GraphController : ControllerBase
    {
        private Graph graph = new Graph();
        [HttpGet(Name = "GetPath")]
        public IEnumerable<string> Get(string entry)
        {
            List<string> result;
            try
            {
                result = graph.GetPath("USA",entry);
            }
            catch (ArgumentException e)
            {

                return new List<string> { $" {e.Message} SUCH DESTIONATION DOES NOT EXIST" };
            }
            return result;
        }
    }
}

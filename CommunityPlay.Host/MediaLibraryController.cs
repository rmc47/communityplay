using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CommunityPlay.Host
{
    public class MediaLibraryController : ApiController
    {
        [HttpGet]
        public List<Media> List()
        {
            Console.WriteLine("List media");
            return MediaLibrary.AllMedia();
        }

        [HttpGet]
        public IEnumerable<Media> Search(string term)
        {
            Console.WriteLine("Searching: " + term);
            var results = MediaLibrary.AllMedia().FindAll(m => m.Name.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);
            return results;
        }
    }
}

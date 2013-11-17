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
        public List<Media> ListCarts()
        {
            Console.WriteLine("List media");
            return MediaLibrary.Instance.AllCarts;
        }

        [HttpGet]
        public IEnumerable<Media> Search(string term)
        {
            Console.WriteLine("Searching: " + term);
            if (string.IsNullOrEmpty(term))
                return new Media[0];

            return MediaLibrary.Instance.Search(term);
        }
    }
}

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
            return MediaLibrary.AllMedia();
        }
    }
}

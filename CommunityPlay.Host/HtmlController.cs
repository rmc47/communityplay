using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CommunityPlay.Host
{
    public class HtmlController : ApiController
    {
        public HttpResponseMessage Get(string path)
        {
            return new HttpResponseMessage() { Content = new ByteArrayContent(File.ReadAllBytes("html/" + path)) };
        }
    }
}

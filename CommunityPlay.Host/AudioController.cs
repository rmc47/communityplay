using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CommunityPlay.Host
{
    public class AudioController : ApiController
    {
        [HttpGet]
        public void Play(string id)
        {
            Guid g;
            if (!Guid.TryParse(id, out g))
                return;

            var media = MediaLibrary.Instance.GetByID(g);
            if (media == null)
                return;

            AudioManager.Instance.Start(media.Path);
        }

        [HttpGet]
        public void StopAll()
        {
            AudioPlayer.StopAll();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CommunityPlay.Host
{
    public class NowPlayingController : ApiController
    {
        [HttpGet]
        public List<NowPlayingItem> List()
        {
            return AudioManager.Instance.ItemsPlaying;
        }

        [HttpGet]
        public void Stop(string id)
        {
            AudioPlayer player = AudioManager.Instance.GetPlayer(id);
            if (player == null)
                return;

            player.Stop();
        }

        [HttpGet]
        public void Fade(string id)
        {
            AudioPlayer player = AudioManager.Instance.GetPlayer(id);
            if (player == null)
                return;

            player.FadeOut();
        }
    }
}

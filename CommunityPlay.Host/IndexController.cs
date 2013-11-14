using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CommunityPlay.Host
{
    public class IndexController : ApiController
    {
        static AudioPlayer s_Player;

        public string Get(string method)
        {
            //Program.PlayStuff();
            switch (method)
            {
                case "play":
                    s_Player = new AudioPlayer(@"d:\dropbox\105ww\Jingles and Jungles\C105 Weekend Waffle Sung A.mp3");
                    s_Player.Start();
                    break;
                case "stop":
                    if (s_Player != null)
                        s_Player.Stop();
                    break;
            }
            return "Hello worldzz";
        }
    }
}

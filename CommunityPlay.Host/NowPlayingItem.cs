using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlay.Host
{
    public class NowPlayingItem
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string TimeRemaining { get; set; }
        public string StopUri { get; set; }
        public string FadeUri { get; set; }
    }
}

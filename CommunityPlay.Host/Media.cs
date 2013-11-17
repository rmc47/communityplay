using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlay.Host
{
    public class Media
    {
        public Guid ID { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }

        public string PlayUri
        {
            get
            {
                return "/api/audio/play/" + ID;
            }
        }
    }
}

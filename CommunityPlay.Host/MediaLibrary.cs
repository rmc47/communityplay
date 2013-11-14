using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlay.Host
{
    class MediaLibrary
    {
        private static List<Media> s_AllMedia;

        public static List<Media> AllMedia()
        {
            if (s_AllMedia != null)
                return s_AllMedia;

            List<Media> media = new List<Media>();
            int nextID = 1;
            foreach (string path in Directory.GetFiles(@"D:\dropbox\cambridge 105 daytime team", "*.mp3", SearchOption.AllDirectories))
            {
                media.Add(new Media { ID = nextID++, Name = Path.GetFileNameWithoutExtension(path), Path = path });
                if (nextID > 1000)
                    break;
            }
            return s_AllMedia = media;
        }
    }
}

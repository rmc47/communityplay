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
        public static MediaLibrary Instance = new MediaLibrary ();

        private Dictionary<Guid, Media> m_AllMedia = new Dictionary<Guid, Media>();
        private List<Media> m_AllCarts;

        private MediaLibrary()
        {
            AddDirectory(GetMedia(@"\\carbon.syxis.co.uk\music\"));

            var allCarts = GetMedia(@"D:\dropbox\Cambridge 105 Daytime Team");
            m_AllCarts = allCarts;
            AddDirectory(allCarts);
        }

        private void AddDirectory(List<Media> media)
        {
            foreach (Media m in media)
                m_AllMedia[m.ID] = m;
        }

        private List<Media> GetMedia(string directory)
        {
            List<Media> media = new List<Media> ();
            foreach (string path in Directory.GetFiles(directory, "*.mp3", SearchOption.AllDirectories))
            {
                var m = new Media { ID = Guid.NewGuid(), Name = Path.GetFileNameWithoutExtension(path), Path = path};
                media.Add(m);
            }
            return media;
        }

        public List<Media> AllCarts
        {
            get { return m_AllCarts; }
        }

        public Media GetByID(Guid id)
        {
            Media m;
            m_AllMedia.TryGetValue(id, out m);
            return m;
        }

        public IEnumerable<Media> Search(string term)
        {
            return m_AllMedia.Values.Where(m => m.Name.ToLowerInvariant().Contains(term.ToLowerInvariant()));
        }
    }
}

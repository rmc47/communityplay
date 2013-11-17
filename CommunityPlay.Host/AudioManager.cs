using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlay.Host
{
    internal class AudioManager
    {
        private static readonly AudioManager s_Instance = new AudioManager();
        public static AudioManager Instance { get { return s_Instance; } } // Oh this should totally be DI'd..

        private List<AudioPlayer> m_AudioPlayers = new List<AudioPlayer>();

        public void Start(string path)
        {
            AudioPlayer player = new AudioPlayer(path);

            player.Completed += PlayerCompleted;
            player.Name = Path.GetFileNameWithoutExtension(path);
            lock (m_AudioPlayers)
            {
                m_AudioPlayers.Add(player);
            }
            player.Start();
        }

        private void PlayerCompleted(object sender, EventArgs e)
        {
            AudioPlayer player = sender as AudioPlayer;
            if (player == null)
                throw new InvalidOperationException("Unexpected sender type for PlayerCompleted: " + sender);

            lock (m_AudioPlayers)
            {
                m_AudioPlayers.Remove(player);
            }
        }

        public List<NowPlayingItem> ItemsPlaying
        {
            get
            {
                List<NowPlayingItem> items = new List<NowPlayingItem>();
                lock (m_AudioPlayers)
                {
                    foreach (AudioPlayer player in m_AudioPlayers)
                    {
                        items.Add(new NowPlayingItem
                        {
                            Name = player.Name,
                            ID = player.ID,
                            TimeRemaining = player.TimeRemaining.ToString("mm\\:ss"),
                            StopUri = "/api/nowplaying/stop/" + player.ID,
                            FadeUri = "/api/nowplaying/fade/" + player.ID
                        });
                    }
                }
                return items;
            }
        }

        public AudioPlayer GetPlayer(string ID)
        {
            return m_AudioPlayers.Find(p => p.ID.Equals(ID, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

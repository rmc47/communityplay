using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommunityPlay.Host
{
    internal class AudioPlayer
    {
        private static bool s_Shutdown = false;
        private static CountdownEvent s_ShutdownSemaphore = new CountdownEvent(1); // Have to start at one otherwise it pre-signals it!

        public static void Shutdown()
        {
            s_ShutdownSemaphore.Signal();
            s_Shutdown = true;
            s_ShutdownSemaphore.Wait();
        }

        public static void StopAll()
        {
            Shutdown();
            s_Shutdown = false;
            s_ShutdownSemaphore = new CountdownEvent(1);
        }

        IWavePlayer m_WaveOutDevice;
        WaveChannel32 m_VolumeStream;
        string m_Filename;

        public AudioPlayer(string filename)
        {
            m_Filename = filename;
        }

        void PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (m_WaveOutDevice != null)
            {
                m_WaveOutDevice.Stop();
            }
            if (m_VolumeStream != null)
            {
                // this one really closes the file and ACM conversion
                m_VolumeStream.Close();
                m_VolumeStream = null;
                // this one does the metering stream
                //mainOutputStream.Close();
                //mainOutputStream = null;
            }
            if (m_WaveOutDevice != null)
            {
                m_WaveOutDevice.Dispose();
                m_WaveOutDevice = null;
            }
            s_ShutdownSemaphore.Signal();
        }

        public void Start()
        {
            s_ShutdownSemaphore.AddCount();
            m_WaveOutDevice = new WaveOut();
            m_VolumeStream = CreateInputStream(m_Filename);
            m_WaveOutDevice.Init(m_VolumeStream);
            m_WaveOutDevice.PlaybackStopped += PlaybackStopped;
            m_WaveOutDevice.Play();
            ThreadPool.QueueUserWorkItem(CheckPlaybackStatus);
        }

        private void CheckPlaybackStatus(object state)
        {
            if (m_VolumeStream == null)
                return;
            if (m_WaveOutDevice.PlaybackState == PlaybackState.Stopped)
                return;

            if (m_VolumeStream.Position >= m_VolumeStream.Length || s_Shutdown)
            {
                m_WaveOutDevice.Stop();
            }
            else
            {
                Thread.Sleep(100);
                ThreadPool.QueueUserWorkItem(CheckPlaybackStatus);
            }
        }

        public void Stop()
        {
            m_WaveOutDevice.Stop();
        }

        public void FadeOut()
        {
        }

        private static WaveChannel32 CreateInputStream(string fileName)
        {
            WaveChannel32 inputStream;
            if (fileName.EndsWith(".mp3"))
            {
                Mp3FileReader mp3Reader = new Mp3FileReader(fileName);
                inputStream = new WaveChannel32(mp3Reader);
            }
            else
            {
                throw new InvalidOperationException("Unsupported extension");
            }
            return inputStream;
        }
    }
}

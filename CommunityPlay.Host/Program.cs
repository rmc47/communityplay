using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace CommunityPlay.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://+:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.ReadLine();
                AudioPlayer.Shutdown();
            }
        }
    }
}

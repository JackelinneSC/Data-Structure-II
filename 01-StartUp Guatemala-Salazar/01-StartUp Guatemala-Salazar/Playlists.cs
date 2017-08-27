using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_StartUp_Guatemala_Salazar
{
    class Playlists
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<SongsProperties> playlist { get; set; }
        public Dictionary<string, SongsProperties> searching { get; set; }
        public List<string> routes { get; set; }
        
        public Playlists(string name, string description)
        {
            this.name = name;
            this.description = description;
            playlist = new List<SongsProperties>();
            searching = new Dictionary<string, SongsProperties>();
            routes = new List<string>();

        }
        public Playlists(string name)
        {
            playlist = new List<SongsProperties>();
            searching = new Dictionary<string, SongsProperties>();
            routes = new List<string>();
            this.name = name;
            description = "";


        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_StartUp_Guatemala_Salazar
{
    class SongsProperties
    {
        //Atributes
        public string nameSong { get; set; }
        public string duration { get; set; }
        public string filePath { get; set; }
        public string artist { get; set; }
        //Constructor
        public SongsProperties(string nameSong, string artist, string duration, string filePath)
        {
            this.nameSong = nameSong;
            this.duration = duration;
            this.filePath = filePath;
            this.artist = artist;
        }
      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_StartUp_Guatemala_Salazar
{
    class SongsProperties: IComparable
    {
        public string nameSong { get; set; }
        public string album { get; set; }
        public string artist { get; set; }
        public string duration { get; set; }
        public string filePath { get; set; }

        public SongsProperties(string namesong, string album, string artist, string duration, string filepath)
        {
            nameSong = namesong;
            this.album = album;
            this.artist = artist;
            this.duration = duration;
            filePath = filepath;
        }
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            SongsProperties song = obj as SongsProperties;
            if (song == null)
            {
                return 1;
            }
            return string.Compare(this.nameSong, song.nameSong);
           

        }
    }
}

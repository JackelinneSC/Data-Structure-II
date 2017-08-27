using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TagLib;

namespace _01_StartUp_Guatemala_Salazar
{
    public partial class Form1 : Form
    {
    
        /// <summary>
        /// Atributes
        /// </summary>
        private string[] routes;
        List<Playlists> lists = new List<Playlists>();
        Playlists yourLibrary;

        public Form1()
        {
            InitializeComponent();
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        /// <summary>
        /// Start program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openSong = new OpenFileDialog();
            openSong.FileName = "Search a new song";
            openSong.Filter = "file mp3|*.mp3";
            openSong.Multiselect = true;
            //openSong.FilterIndex = 1;
            if (openSong.ShowDialog() == DialogResult.OK)
            {
                axWindowsMediaPlayer1.URL = openSong.FileName;
                routes = openSong.FileNames;
                //Creat your library
                if (comboBox1.Items.Count == 0 || comboBox1.Items.Count ==1)
                {
                    
                    if (comboBox1.Items.Count == 0)
                    {
                        comboBox1.Items.Add("Your library");
                        yourLibrary = new Playlists("Your library", "Your songs");
                        foreach (var routes in routes)
                        {
                            addToList(routes, yourLibrary.name, yourLibrary);
                        }

                        lists.Add(yourLibrary);
                        printSogns(yourLibrary.playlist);
                    }
                    else
                    {
                        foreach (var routes in routes)
                        {
                            addToList(routes, yourLibrary.name, yourLibrary);
                        }

                    }
                    
                }
                else
                {
                    createPlaylist(comboBox1.Text);                    
                }
                         
                
            }

        }
        private void addToList(string fileRoute, string nameList, Playlists actualList)
        {
            SongsProperties newSong;
            string filename = "", duration = "", songTitle = "", album = "", artist = "";
          
            try
            {
                TagLib.File Mp3file = TagLib.File.Create(fileRoute);
                filename = Mp3file.Name;
                duration = Mp3file.Properties.Duration.ToString();
                songTitle = Mp3file.Tag.Title;
                album = Mp3file.Tag.Album;
                artist = Mp3file.Tag.FirstArtist;
                newSong = new SongsProperties(songTitle, album, artist, duration, filename);
                actualList.playlist.Add(newSong);
                actualList.searching.Add(songTitle.ToLower(), newSong);
                actualList.routes.Add(fileRoute);
               
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// Creat playlists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatPlayList newPlaylist = new CreatPlayList();
            newPlaylist.ShowDialog();
            Playlists newlist = new Playlists(newPlaylist.namelist, newPlaylist.description);
            lists.Add(newlist);
            comboBox1.Items.Add(newPlaylist.namelist);

        }
        private void createPlaylist(string nameList)
        {
            foreach (var lists in lists)
            {
                if (lists.name == nameList )
                {
                    foreach (var routes in routes)
                    {
                        addToList(routes, lists.name, lists);
                        printSogns(lists.playlist);
                    }
                }
                else
                {

                    Playlists newPlaylist = new Playlists(comboBox1.Text);
                    foreach (var routes in routes)
                    {
                        addToList(routes, newPlaylist.name, newPlaylist);
                        printSogns(newPlaylist.playlist);
                    }
                }
            }
        }
 

        private void saveDefaultSongs(List<string> songs)
        {
           
         //   StreamWriter YourLibrary = new StreamWriter("c:\\YourLibrary.txt");
                       
           // YourLibrary.WriteLine(songs);

            //YourLibrary.Close();
        }
        private void openDefaultSongs()
        {

        }
       
      
       /// <summary>
       /// Close the program
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      
        /// <summary>
        ///Button to search music
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string songTitle = textBox1.Text;
            if (songTitle == "")
            {
                MessageBox.Show("Asegúrate de escribir el nombre de la canción y de Agregar canciones a tu librería para poder buscarlas.");
            }
            else
                searchSong(songTitle);
        }
        private void searchSong(string key)
        {
            try
            {
                Playlists obj;
                if (comboBox1.SelectedIndex.Equals(-1) || comboBox1.SelectedIndex.Equals(0))
                    obj = new Playlists("Your library");
                else
                    obj = new Playlists(comboBox1.Text);
                SongsProperties element;
                foreach (var lists in lists)
                {
                    element = lists.searching[key.ToLower()];
                    listBox1.Visible = true;
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    listBox1.Items.Add(element.artist + " - " + element.nameSong + ".mp3");
                    listBox1.Items.Add("Duration: " + element.duration);
                    axWindowsMediaPlayer1.URL = element.filePath;
                    printSogns(lists.playlist);
                    if (element != null)
                    {
                        return;
                    }

                }
                
            }
            catch (Exception )
            {
                MessageBox.Show("Ups, try againg! Make sure the song exist on your library");
            }

        }
              
        /// <summary>
        /// Print songs of each playlist
        /// </summary>
        /// <param name="play"></param>
        private void printSogns(List<SongsProperties> play)
        {
            Playlist_lb.Items.Clear();
            foreach (var lists in play)
            {
                Playlist_lb.Items.Add(lists.nameSong + " - "+ lists.duration);
            }
        }
        
        /// <summary>
        /// Button to sort playlists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            string option = "0";
            if (radioButton1.Checked != true)
            {
                if (radioButton2.Checked != true)
                    MessageBox.Show("Select an option to sort your playlist: ascending or descending.");
                else
                    option = "2";
            }
            else
                option = "1";
            if (radioButton3.Checked != true)
            {
                if (radioButton4.Checked != true)
                    MessageBox.Show("Select the way you want to sort your playlist: by name or by duration.");
                else
                {
                    option += "4";
                    sortPlaylist(option);
                }
                   
            }
            else
            {
                option += "3";
                sortPlaylist(option);
            }
                
        
        }
        private void sortPlaylist(string option)
        {
            Playlists obj;
            if (comboBox1.SelectedIndex.Equals(-1) || comboBox1.SelectedIndex.Equals(0))
                obj = new Playlists("Your library");
            else
                obj = new Playlists(comboBox1.Text);

            foreach (var lists in lists)
            {
                if (lists.name == obj.name)
                {
                    lists.playlist.Sort((a, b) =>
                    {
                        int comparacion1 = compare(option, a, b);
                        if (comparacion1 == 0)
                            return compare(option, a, b);
                        else
                            return comparacion1;
                    });

                }
                printSogns(lists.playlist);
                
            }
        }
        private int compare(string option, SongsProperties a, SongsProperties b)
        {
            switch (option)
            {
                case "13"://parametros para ascendente por nombre
                    return string.Compare(a.nameSong, b.nameSong);
                case "23"://parametros para descendente por nombre
                    return string.Compare(b.nameSong, a.nameSong);
                case "14": //parámetros ascendentes por duración
                    return string.Compare(a.duration, b.duration);
                case "24"://parámetros descendentes por duración
                    return string.Compare(b.duration, a.duration);
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Supuestamente debe desplegar las canciones de cada playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Playlists obj = new Playlists(comboBox1.Text, "");
            foreach (var lists in lists)
            {
                if (lists.name == obj.name)
                {
                    printSogns(lists.playlist);
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    break;
                }
            }
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.previous();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.next();
        }

    }
}

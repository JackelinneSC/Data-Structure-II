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
using System.Runtime.InteropServices;

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
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

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
            if (openSong.ShowDialog() == DialogResult.OK)
            {
                axWindowsMediaPlayer1.URL = openSong.FileName;
                routes = openSong.FileNames;
                //Creat your library, the main playlist
                if (comboBox1.Items.Count == 0 || comboBox1.Items.Count ==1)
                {
                    
                    if (comboBox1.Items.Count == 0)
                    {

                        comboBox1.Items.Add("Your library");
                        comboBox1.SelectedIndex = 0;
                        yourLibrary = new Playlists("Your library", "Your songs");
                        lists.Add(yourLibrary);
                        CreatePlaylist(yourLibrary.name);
                        
                    }
                    else
                        CreatePlaylist(yourLibrary.name);
                }
                else
                    CreatePlaylist(comboBox1.Text);
            }

        }
        /// <summary>
        /// Method to add songs to a playlist
        /// </summary>
        /// <param name="fileRoute">Song's path </param>
        /// <param name="actualList">Actual playlist</param>
        private void AddToList(string fileRoute, Playlists actualList)
        {
            SongsProperties newSong;
            string filename = "", duration = "", songTitle = "", artist = "";
          
            try
            {
                TagLib.File Mp3file = TagLib.File.Create(fileRoute);
                filename = Mp3file.Name;
                duration = Mp3file.Properties.Duration.ToString();
                songTitle = Mp3file.Tag.Title;
                artist = Mp3file.Tag.FirstAlbumArtist; 
                newSong = new SongsProperties(songTitle, artist, duration, filename);
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
        /// Creat playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatPlayList newPlaylist = new CreatPlayList(); //New form
            newPlaylist.ShowDialog();
            Playlists newlist = new Playlists(newPlaylist.namelist, newPlaylist.description);
            lists.Add(newlist);
            comboBox1.Items.Add(newPlaylist.namelist);

        }
        /// <summary>
        /// Method to create playlist
        /// </summary>
        /// <param name="nameList">Playlist name</param>
        private void CreatePlaylist(string nameList)
        {
            //Look for the playlist name and add the songs. Then print the songs.
            foreach (var lists in lists)
            {
                if (lists.name == nameList )
                {
                    foreach (var routes in routes)
                    {
                        AddToList(routes, lists);
                        PrintPlaylist(lists.playlist);
                    }
                    break;
                }
               
            }
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
                MessageBox.Show("Write the song tittle", "Oh no!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            else
                SearchSong(songTitle);
        }
        /// <summary>
        /// Method to search a song by its key
        /// </summary>
        /// <param name="key"></param>
        private void SearchSong(string key)
        {
            try
            {
                Playlists obj;
                if (comboBox1.SelectedIndex.Equals(-1) || comboBox1.SelectedIndex.Equals(0)) //it means that is in the main playlist "your library"
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
                    PrintPlaylist(lists.playlist);
                    if (element != null)
                        return;
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
        private void PrintPlaylist(List<SongsProperties> play)
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
                    SortPlaylist(option);
                }
                   
            }
            else
            {
                option += "3";
                SortPlaylist(option);
            }
                
        
        }
        /// <summary>
        /// Method to sort using lambda
        /// </summary>
        /// <param name="option"></param>
        private void SortPlaylist(string option)
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
                    lists.playlist.Sort((song1, song2) =>
                    {
                        int comparenumber = Compare(option, song1, song2);
                        if (comparenumber == 0)
                            return Compare(option, song1, song2);
                        else
                            return comparenumber;
                    });
                    
                }
                PrintPlaylist(lists.playlist);
                
            }
        }
        /// <summary>
        /// Compare values to the sort method
        /// </summary>
        /// <param name="option">ID for each type of sort</param>
        /// <param name="a">A song</param>
        /// <param name="b">Other song</param>
        /// <returns></returns>
        private int Compare(string option, SongsProperties a, SongsProperties b)
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
                    PrintPlaylist(lists.playlist);
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    break;
                }
            }
           
        }
        /// <summary>
        /// The user can select any song from the playlist but if this list is sort before it will be a error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Playlist_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var lists in lists)
            {
                if (lists.name == comboBox1.Text)
                {
                    try
                    {
                        List<SongsProperties> a = lists.playlist;
                        foreach (SongsProperties b in a)
                        {
                            if (b.nameSong == a.ElementAt(Playlist_lb.SelectedIndex).nameSong)
                            {
                                axWindowsMediaPlayer1.URL = b.filePath;
                                break;
                            }
                            
                        }
                        
                    }
                    catch (Exception)
                    {
                       //There is an exception when is the first time using the program
                    }
                    
                }
                    
            }
         

        }

    }
}

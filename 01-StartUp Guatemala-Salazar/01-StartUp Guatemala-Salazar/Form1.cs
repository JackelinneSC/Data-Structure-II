using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _01_StartUp_Guatemala_Salazar
{
    public partial class Form1 : Form
    {
       
        private string[] songs;
        private string[] roots;
        private List<string> playList = new List<string>();
        public Form1()
        {
            InitializeComponent();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

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
                // DialogResult result = MessageBox.Show("Would you like to add this song to your library?", "Your library", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                songs = openSong.SafeFileNames;
                roots = openSong.FileNames;
                foreach (var songs in songs)
                {
                    Playlist_lb.Items.Add(songs);
                    playList.Add(songs);
                }
            }

        }
        private void defaultSongs()
        {
            System.IO.StreamWriter YourLibrary = new System.IO.StreamWriter("c:\\test.txt");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void Playlist_lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = roots[Playlist_lb.SelectedIndex];
        }
        private void creatPlayList()
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

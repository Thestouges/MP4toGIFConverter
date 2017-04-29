using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using NReco.VideoConverter;

namespace MP4toGIF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ImageDir;
        string SaveName;

        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            ConvertButton.IsEnabled = false;
            DirTextBox.IsReadOnly = true;
            SaveTextBox.IsReadOnly = true;
            SaveDirButton.IsEnabled = false;
        }

        private void DirButton_Click(object sender, RoutedEventArgs e)
        {
            var Dir = new Microsoft.Win32.OpenFileDialog();
            Dir.Filter = "MP4 files (*.mp4) | *.mp4";
            try
            {
                var close = Dir.ShowDialog();
                switch (close)
                {
                    case true:
                        DirTextBox.Text = Dir.FileName;
                        ImageDir = System.IO.Path.GetDirectoryName(Dir.FileName);
                        SaveName = System.IO.Path.GetFileName(Dir.FileName);
                        if (SaveTextBox.Text == "")
                        {
                            SaveTextBox.Text = ImageDir;
                        }
                        SaveDirButton.IsEnabled = true;
                        break;
                    case false:
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
        }

        private void DirTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                MP4Media.Source = new Uri(DirTextBox.Text);
                ConvertButton.IsEnabled = true;
            }
            catch
            {

            }
        }

        private void SaveDirButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            try
            {
                var Dir = dialog.ShowDialog();

                if(Dir == System.Windows.Forms.DialogResult.OK)
                {
                    SaveTextBox.Text = dialog.SelectedPath;
                }
            }
            catch
            {

            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ffmpeg = new FFMpegConverter();
                var settings = new ConvertSettings();
                settings.VideoFrameRate = Convert.ToInt32(FPSselector.Text);
                ffmpeg.ConvertMedia(DirTextBox.Text, null, SaveTextBox.Text + "\\" + SaveName + ".gif", null, settings);
            }
            catch
            {

            }
        }
    }
}

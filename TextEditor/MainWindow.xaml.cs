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
using Microsoft.Win32;
using System.IO;

namespace TextEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SaveFileDialog fil = new SaveFileDialog();
        public string filename;
        public  string n;
        public string theme = File.ReadAllText("Config/theme.txt");
        public MainWindow()
        {
            InitializeComponent();
            RememberFile();
        }

        private void Content_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        private void Main_Closed(object sender, EventArgs e)
        {
            File.WriteAllText("Config/theme.txt", theme);
            File.WriteAllText("Config/text.txt", filename);
            try
            {
                string path = File.ReadAllText(filename);
                if (Content.Text != path)
                {
                    var result = MessageBox.Show("Вы хотите соханить файл?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        SaveAs();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch
            {

            }
        }
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            Upload();
        }

        public void  SaveAs()
        {
            Main.Title = "Light text";
            fil.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            fil.Title = "Save as";
            if (fil.ShowDialog() == false)
            {
                return;
            }
            filename = fil.FileName;
            File.WriteAllText(filename, Content.Text);
            Main.Title += "(" + fil.FileName + ")";
        }
        public void Upload()
        {
            Main.Title = "Light text";
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (f.ShowDialog() == false)
                return;
            filename = f.FileName;
            Main.Title += "(" + filename + ")";
            string result = File.ReadAllText(filename);
            Content.Text = result;
        }

        private void SaveOne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (filename == null)
                {
                    SaveAs();
                }
                File.WriteAllText(filename, Content.Text);
            }
            catch
            {

            }
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        private void Content_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
            {
                try
                {
                    if (filename == null)
                    {
                        SaveAs();
                    }
                    File.WriteAllText(filename, Content.Text);
                }
                catch
                {

                }
            }
        }
        private void Content_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.LeftCtrl && Keyboard.Modifiers==ModifierKeys.Shift)
            {
                SaveAs();
            }
        }
        public void RememberFile()
        {
            try
            {
                Main.Title = "Light text";
                string a = File.ReadAllText("Config/text.txt");
                Content.Text = File.ReadAllText(a);
                Main.Title += "(" + a + ")";
            }
            catch
            {

            }
        }
        private void WhiteTheme_Click(object sender, RoutedEventArgs e)
        {
            theme = "White";
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            filename = File.ReadAllText("Config/text.txt");
            n = File.ReadAllText("Config/theme.txt");
            //if (n== "White")
            //{
            //    Content.Background = Brushes.White;
            //    Content.Foreground = Brushes.Black;
            //}
            switch (n)
            {
                case "White":
                    Content.Background = Brushes.White;
                    Content.Foreground = Brushes.Black;
                    break;
                case "Black":
                    Content.Background = Brushes.Black;
                    Content.Foreground = Brushes.White;
                    break;
                case "Gray":
                    Content.Background = Brushes.Gray;
                    Content.Foreground = Brushes.White;
                    break;
                case "Default":
                    break;
            }
        }

        private void BlackTheme_Click(object sender, RoutedEventArgs e)
        {
            theme = "Black";
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void GrayTheme_Click(object sender, RoutedEventArgs e)
        {
            theme = "Gray";
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void DefaultTheme_Click(object sender, RoutedEventArgs e)
        {
            theme = "Default";
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}

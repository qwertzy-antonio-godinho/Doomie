using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Doomie
{
    public partial class MainWindow : Window
    {
        ViewModel VM = new ViewModel();
        Dictionary<string, string> Parameters = new Dictionary<string, string>();
        string Settings_File = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Doomie.cfg");
        string Folder_Playlists_New = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Playlists");
        string Folder_Playlists_Import = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Playlists");
        string Folder_Playlists_Open = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Playlists");
        string Folder_Playlists_Save_As = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Playlists");
        string Folder_SourcePort_Open = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sourceports");
        string Folder_IWad_Open = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Wads", "IWads");
        string Folder_PWads_Import = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Wads", "PWads");
        string Folder_PWads_Open = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Wads", "PWads");

        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM;
            if (File.Exists(Settings_File))
            {
                Helper_Settings_Read();
            }
            else
            {
                Helper_Settings_Write();
            }
            Helper_UI();
        }

        private void Button_Playlist_New_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog Playlist_New_File = new SaveFileDialog
            {
                Title = "New playlist...",
                DefaultExt = ".dpf",
                InitialDirectory = Folder_Playlists_New,
                Filter = "Doom Playlist (*.dpf)|*.dpf"
            };
            if (Playlist_New_File.ShowDialog() == true)
            {
                Folder_Playlists_New = Path.GetDirectoryName(Playlist_New_File.FileName);
                Playlists Playlist = new Playlists(true, false, Path.GetFileNameWithoutExtension(Playlist_New_File.FileName), 0, Path.GetDirectoryName(Playlist_New_File.FileName), "", false,"", "");
                var Duplicate = VM.Playlist.SingleOrDefault(x => (x.Playlist_Name.ToLower() == Path.GetFileNameWithoutExtension(Playlist_New_File.FileName).ToLower() & x.Playlist_Location.ToLower() == Path.GetDirectoryName(Playlist_New_File.FileName).ToLower()));
                if (Duplicate != null)
                {
                    int Position = VM.Playlist.IndexOf(Duplicate);
                    VM.Playlist.Remove(Duplicate);
                    VM.Playlist.Insert(Position, Playlist);
                }
                else
                {
                    VM.Playlist.Add(Playlist);
                }
                Helper_UI();
            }
        }

        private void Button_Playlist_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Playlist_Open_File = new OpenFileDialog
            {
                InitialDirectory = Folder_Playlists_Open,
                Filter = "Doom Playlist (*.dpf)|*.dpf"
            };
            if (Playlist_Open_File.ShowDialog() == true)
            {
                Folder_Playlists_Open = Path.GetDirectoryName(Playlist_Open_File.FileName);
                List<string> Files = new List<string>
                {
                    Path.GetFullPath(Playlist_Open_File.FileName)
                };
                Helper_Playlists(Files);
            }
        }

        private void Button_Playlist_Import_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog Playlist_Import_Folder = new System.Windows.Forms.FolderBrowserDialog
            {
                SelectedPath = Folder_Playlists_Import
            };
            Playlist_Import_Folder.Description = "Select a folder to import Playlists:";
            Playlist_Import_Folder.ShowNewFolderButton = false; System.Windows.Forms.DialogResult Playlist_Import_Browser = Playlist_Import_Folder.ShowDialog();

            if (Playlist_Import_Browser == System.Windows.Forms.DialogResult.OK)
            {
                Folder_Playlists_Import = Playlist_Import_Folder.SelectedPath;
                List<string> Playlist_Import_Files = Directory.GetFiles(Playlist_Import_Folder.SelectedPath, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(file => new string[] { ".dpf" }.Contains(Path.GetExtension(file).ToLower())).ToList();
                Helper_Playlists(Playlist_Import_Files);
            }
        }

        private void Button_Playlist_Save_Click(object sender, RoutedEventArgs e)
        {
            foreach (Playlists Playlist_Selected in ListView_Playlists.SelectedItems)
            {
                Playlist_Selected.Playlist_Changed = false;
                string File = Path.Combine(Playlist_Selected.Playlist_Location, Playlist_Selected.Playlist_Name) + ".dpf".ToString();
                Helper_Save(Playlist_Selected, File);
            }
        }

        private void Button_Playlist_Save_As_Click(object sender, RoutedEventArgs e)
        {
            var Selected_Playlists = ListView_Playlists.SelectedItems.Cast<object>().ToList();
            foreach (Playlists Playlist_Selected in Selected_Playlists)
            {
                SaveFileDialog Playlist_Save_As_File = new SaveFileDialog
                {
                    Title = "Save playlist " + Playlist_Selected.Playlist_Name + " as...",
                    DefaultExt = ".dpf",
                    InitialDirectory = Folder_Playlists_Save_As,
                    Filter = "Doom Playlist (*.dpf)|*.dpf"
                };
                if (Playlist_Save_As_File.ShowDialog() == true)
                {
                    Playlists Playlist = new Playlists(false, false, Path.GetFileNameWithoutExtension(Playlist_Save_As_File.FileName),
                        Playlist_Selected.Playlist_Files,
                        Path.GetDirectoryName(Playlist_Save_As_File.FileName),
                        Playlist_Selected.Playlist_SourcePort,
                        Playlist_Selected.Playlist_SourcePort_HasParameters,
                        Playlist_Selected.Playlist_SourcePort_Parameters,
                        Playlist_Selected.Playlist_IWad,
                        Playlist_Selected.Wadlist);
                    var Duplicate = VM.Playlist.SingleOrDefault(x => 
                        (x.Playlist_Name.ToLower() == Path.GetFileNameWithoutExtension(Playlist_Save_As_File.FileName).ToLower() 
                        & x.Playlist_Location.ToLower() == Path.GetDirectoryName(Playlist_Save_As_File.FileName).ToLower()));
                    if (Duplicate != null)
                    {
                        int Position = VM.Playlist.IndexOf(Duplicate);
                        VM.Playlist.Remove(Duplicate);
                        VM.Playlist.Insert(Position, Playlist);
                    }
                    else
                    {
                        VM.Playlist.Add(Playlist);
                    }
                    Folder_Playlists_Save_As = Path.GetDirectoryName(Playlist_Save_As_File.FileName);
                    Helper_Save(Playlist_Selected, Playlist_Save_As_File.FileName);
                }
            }
        }

        private void Button_Playlist_Clear_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Playlists> Playlists_Save = new ObservableCollection<Playlists>(VM.Playlist.Where(x => x.Playlist_Changed == true));
            if (Playlists_Save.Count > 0)
            {
                QuitWindow Quit = new QuitWindow(Playlists_Save)
                {
                    Owner = this
                };
                Quit.ShowDialog();
                if (Quit.DialogResult == true)
                {
                    foreach (Playlists Playlist_Selected in Playlists_Save)
                    {
                        if (Playlist_Selected.Playlist_Save == true)
                        {
                            string File = Path.Combine(Playlist_Selected.Playlist_Location, Playlist_Selected.Playlist_Name) + ".dpf".ToString();
                            Helper_Save(Playlist_Selected, File);
                        }
                    }
                    VM.Playlist.Clear();
                }
                else if (Quit.DialogResult == false)
                {
                }
                else if (Quit.DialogResult == null)
                {
                }
            }
            else
            {
                VM.Playlist.Clear();
            }
            Helper_UI();
        }

        private void Button_About_Click(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;
            foreach (Window Win in Application.Current.Windows)
            {
                if (Win is AboutWindow)
                {
                    isWindowOpen = true;
                    Win.Activate();
                }
            }
            if (!isWindowOpen)
            {
                AboutWindow About = new AboutWindow
                {
                    Owner = this
                };
                About.ShowDialog();
            }
        }

        private void Button_SourcePort_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog SourcePort_Open_File = new OpenFileDialog
            {
                InitialDirectory = Folder_SourcePort_Open,
                Filter = "Executable file (*.exe)|*.exe"
            };
            if (SourcePort_Open_File.ShowDialog() == true)
            {
                Folder_SourcePort_Open = Path.GetDirectoryName(SourcePort_Open_File.FileName);
                ((Playlists)ListView_Playlists.SelectedItem).Playlist_SourcePort = SourcePort_Open_File.FileName;
                ((Playlists)ListView_Playlists.SelectedItem).Playlist_SourcePort_Description = SourcePort_Open_File.FileName;
                Helper_UI();
            }
        }

        private void Button_Iwad_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Iwad_Open_File = new OpenFileDialog
            {
                InitialDirectory = Folder_IWad_Open,
                Filter = "Doom (*.wad,*.pk3,*.pk7)|*.wad;*.pk3;*.pk7"
            };
            if (Iwad_Open_File.ShowDialog() == true)
            {
                Folder_IWad_Open = Path.GetDirectoryName(Iwad_Open_File.FileName);
                ((Playlists)ListView_Playlists.SelectedItem).Playlist_IWad = Iwad_Open_File.FileName;
                ((Playlists)ListView_Playlists.SelectedItem).Playlist_IWad_Description = Iwad_Open_File.FileName;
                Helper_UI();
            }
        }

        private void Button_Pwad_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Pwad_Open_File = new OpenFileDialog
            {
                InitialDirectory = Folder_PWads_Open,
                Filter = "Doom (*.wad,*.pk3,*.pk7,*.deh,*.bex)|*.wad;*.pk3;*.pk7;*.deh;*.bex"
            };
            if (Pwad_Open_File.ShowDialog() == true)
            {
                Folder_PWads_Open = Path.GetDirectoryName(Pwad_Open_File.FileName);
                List<string> Files = new List<string>
                {
                    Path.GetFullPath(Pwad_Open_File.FileName)
                };
                Helper_Pwads(Files);
            }
        }

        private void Button_Pwad_Import_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog Pwad_Import_Folder = new System.Windows.Forms.FolderBrowserDialog
            {
                SelectedPath = Folder_PWads_Import
            };
            Pwad_Import_Folder.Description = "Select a folder to import PWADs:";
            Pwad_Import_Folder.ShowNewFolderButton = false;

            System.Windows.Forms.DialogResult Pwad_Import_Browser = Pwad_Import_Folder.ShowDialog();

            if (Pwad_Import_Browser == System.Windows.Forms.DialogResult.OK)
            {
                Folder_PWads_Import = Pwad_Import_Folder.SelectedPath;
                List<string> Pwad_Import_Files = Directory.GetFiles(Pwad_Import_Folder.SelectedPath, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(file => new string[] { ".wad", ".pk3", ".pk7", ".deh", ".bex" }.Contains(Path.GetExtension(file).ToLower())).ToList();
                Helper_Pwads(Pwad_Import_Files);
            }
        }

        private void Button_Pwad_Clear_Click(object sender, RoutedEventArgs e)
        {
            if (ListView_Playlists.SelectedItems.Count == 1)
            {
                ((Playlists)ListView_Playlists.SelectedItem).Wadlist.Clear();
                ((Playlists)ListView_Playlists.SelectedItem).Playlist_Files = ((Playlists)ListView_Playlists.SelectedItem).Wadlist.Count;
            }
        }

        private void Button_Play_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = Label_SourcePort.ToolTip.ToString();
            string Args = " -iwad " + AddQuotesIfRequired(@Label_IWad.ToolTip.ToString());
            if (ListView_Pwads.Items.Count > 0)
            {
                foreach (Wads Wad in ListView_Pwads.Items)
                {
                    if (Wad.Wad_Load == true)
                    {
                        if (Path.GetExtension(Path.Combine(Wad.Wad_Location, Wad.Wad_File)).ToLower() == ".wad"
                         | Path.GetExtension(Path.Combine(Wad.Wad_Location, Wad.Wad_File)).ToLower() == ".pk3"
                         | Path.GetExtension(Path.Combine(Wad.Wad_Location, Wad.Wad_File)).ToLower() == ".pk7")
                        {
                            if (Wad.Wad_Merge == false)
                            { 
                                Args = Args + " -file " + AddQuotesIfRequired(@Path.Combine(Wad.Wad_Location, Wad.Wad_File).ToString());
                            }
                            else
                            {
                                Args = Args + " -merge " + AddQuotesIfRequired(@Path.Combine(Wad.Wad_Location, Wad.Wad_File).ToString());
                            }
                        }
                        else if (Path.GetExtension(Path.Combine(Wad.Wad_Location, Wad.Wad_File)).ToLower() == ".deh")
                        {
                            Args = Args + " -deh " + AddQuotesIfRequired(@Path.Combine(Wad.Wad_Location, Wad.Wad_File).ToString());
                        }
                        else if (Path.GetExtension(Path.Combine(Wad.Wad_Location, Wad.Wad_File)).ToLower() == ".bex")
                        {
                            Args = Args + " -bex " + AddQuotesIfRequired(@Path.Combine(Wad.Wad_Location, Wad.Wad_File).ToString());
                        }
                    }
                }
                //Console.WriteLine(Args);
            }
            if (((Playlists)ListView_Playlists.SelectedItem).Playlist_SourcePort_Parameters.ToString() != "")
            {
                Args = Args + " " + ((Playlists)ListView_Playlists.SelectedItem).Playlist_SourcePort_Parameters.ToString();
            }
            process.StartInfo.Arguments = Args;
            process.Start();
        }

        private void ItemContextMenu_Playlists_Remove_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Playlists> Playlists_Save_Selected = new ObservableCollection<Playlists>();
            foreach (Playlists Playlist in ListView_Playlists.SelectedItems)
            {
                Playlists_Save_Selected.Add(Playlist);
            }
            ObservableCollection<Playlists> Playlists_Save = new ObservableCollection<Playlists>(Playlists_Save_Selected.Where(x => x.Playlist_Changed == true));
            if (Playlists_Save.Count > 0)
            {
                QuitWindow Quit = new QuitWindow(Playlists_Save)
                {
                    Owner = this
                };
                Quit.ShowDialog();
                if (Quit.DialogResult == true)
                {
                    foreach (Playlists Playlist_Selected in Playlists_Save)
                    {
                        if (Playlist_Selected.Playlist_Save == true)
                        {
                            string File = Path.Combine(Playlist_Selected.Playlist_Location, Playlist_Selected.Playlist_Name) + ".dpf".ToString();
                            Helper_Save(Playlist_Selected, File);
                        }
                    }
                    var Selected_Playlists = ListView_Playlists.SelectedItems.Cast<object>().ToList();
                    foreach (Playlists Playlist in Selected_Playlists)
                    {
                        VM.Playlist.Remove(Playlist);
                    }
                }
                else if (Quit.DialogResult == false)
                {
                }
                else if (Quit.DialogResult == null)
                {
                }
            }
            else
            {
                var Selected_Playlists = ListView_Playlists.SelectedItems.Cast<object>().ToList();
                foreach (Playlists Playlist in Selected_Playlists)
                {
                    VM.Playlist.Remove(Playlist);
                }
            }
        }

        private void ItemContextMenu_Pwads_Toogle_Load(object sender, RoutedEventArgs e)
        {
            var Selected_Pwads = ListView_Pwads.SelectedItems;
            foreach (Wads Pwad in Selected_Pwads)
            {
                if (Pwad.Wad_Load.Equals(true))
                {
                    Pwad.Wad_Load = false;
                }
                else
                {
                    Pwad.Wad_Load = true;
                }
            }
        }

        private void ItemContextMenu_Pwads_Remove_Click(object sender, RoutedEventArgs e)
        {
            var Selected_Pwads = ListView_Pwads.SelectedItems.Cast<object>().ToList();
            foreach (Wads Pwad in Selected_Pwads)
            {
                ((Playlists)ListView_Playlists.SelectedItem).Wadlist.Remove(Pwad);
                ((Playlists)ListView_Playlists.SelectedItem).Playlist_Files = ((Playlists)ListView_Playlists.SelectedItem).Wadlist.Count;
            }
        }

        private void ListView_Playlists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Helper_UI();
        }

        private void ListView_Pwads_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Helper_UI();
        }

        private void CheckBox_Load_Toogle(object sender, RoutedEventArgs e)
        {
            ((Playlists)ListView_Playlists.SelectedItem).Playlist_Changed = true;
        }

        private void CheckBox_Merge_Toogle(object sender, RoutedEventArgs e)
        {
            ((Playlists)ListView_Playlists.SelectedItem).Playlist_Changed = true;
        }

        private void ListView_Pwads_Drop(object sender, DragEventArgs e)
        {
            if (((Playlists)ListView_Playlists.SelectedItem).Playlist_Files > 1)
            { 
                ((Playlists)ListView_Playlists.SelectedItem).Playlist_Changed = true;
            }
        }

        private void ProgressBar_Load_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBlock_Load.Visibility = (ProgressBar_Load.Value == 0 | ProgressBar_Load.Value == 100) ? Visibility.Collapsed : Visibility.Visible;
            ProgressBar_Load.Visibility = (ProgressBar_Load.Value == 0 | ProgressBar_Load.Value == 100) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            int Playlist_Save = 0;
            foreach (Playlists Playlist in VM.Playlist)
            {
                if (Playlist.Playlist_Changed == true)
                {
                    Playlist_Save++;
                }
            }
            if (Playlist_Save > 0)
            {
                ObservableCollection<Playlists> Playlists_Save = new ObservableCollection<Playlists>(VM.Playlist.Where(x => x.Playlist_Changed == true));
                QuitWindow Quit = new QuitWindow(Playlists_Save)
                {
                    Owner = this
                };
                Quit.ShowDialog();
                if (Quit.DialogResult == true)
                {
                    foreach (Playlists Playlist_Selected in Playlists_Save)
                    {
                        if (Playlist_Selected.Playlist_Save == true)
                        {
                            string File = Path.Combine(Playlist_Selected.Playlist_Location, Playlist_Selected.Playlist_Name) + ".dpf".ToString();
                            Helper_Save(Playlist_Selected, File);
                        }
                    }
                }
                else if (Quit.DialogResult == false)
                {
                    e.Cancel = true;
                }
                else if (Quit.DialogResult == null)
                {
                }
            }
            Helper_Settings_Write();
        }

        private async void Helper_Playlists(List<string> Files)
        {
            foreach (var File in Files)
            {
                FileInfo File_Info = new FileInfo(File);
                if (File_Info.Extension.ToLower().Equals(".dpf"))
                {
                    PlaylistIO Dpf = new PlaylistIO(File_Info.FullName);
                    string Playlist_Name = Path.GetFileNameWithoutExtension(File_Info.Name);
                    int Playlist_Files = Int32.Parse(Dpf.Read("Counter", "PWAD"));
                    string Playlist_Location = File_Info.DirectoryName;
                    string Playlist_Sourceport = Dpf.Read("Path", "SourcePort");
                    bool Playlist_SourcePort_HasParameters = Dpf.KeyExists("Parameters", "SourcePort");
                    string Playlist_SourcePort_Parameters = "";
                    if (Playlist_SourcePort_HasParameters == true)
                    {
                        Playlist_SourcePort_Parameters = Dpf.Read("Parameters", "SourcePort");
                    }
                    string Playlist_Iwad = Dpf.Read("Path", "IWAD");
                    ObservableCollection<Wads> Playlist_Wads = new ObservableCollection<Wads>();
                    var Progress = new Progress<int>(Value => ProgressBar_Load.Value = Value);
                    TextBlock_Load.Text = "Loading playlist " + Playlist_Name + "...";
                    await Task.Run(() =>
                    {
                        for (int i = 1; i <= Playlist_Files; ++i)
                        {
                            int Result = (int)Math.Round((double)i / Playlist_Files * 100);
                            string Wad = Dpf.Read("Path" + i.ToString(), "PWAD");
                            string Wad_File_Path = Wad;
                            bool Wad_Load = true;
                            bool Wad_Merge = false;
                            if (Wad.Contains("|"))
                            {
                                List<string> Wad_Holder = Wad.Split('|').ToList();
                                Wad_Merge = Boolean.Parse(Wad_Holder[2]);
                                Wad_Load = Boolean.Parse(Wad_Holder[1]);
                                Wad_File_Path = Wad_Holder[0];
                            }
                            FileInfo Wad_File_Info = new FileInfo(Wad_File_Path);
                            string Wad_File = Path.GetFileName(Wad_File_Info.Name);
                            string Wad_Location = Wad_File_Info.DirectoryName;
                            Wads Pwad = new Wads(true, Wad_Load, Wad_File, Wad_Merge, Wad_Location);
                            Playlist_Wads.Add(Pwad);
                            ((IProgress<int>)Progress).Report(Result);
                        }
                    });
                    var Duplicate = VM.Playlist.SingleOrDefault(x => (x.Playlist_Name.ToLower() == Playlist_Name.ToLower() & x.Playlist_Location.ToLower() == Playlist_Location.ToLower()));
                    if (Duplicate == null)
                    {
                        Playlists Playlist = new Playlists(false, false, Playlist_Name, Playlist_Files, Playlist_Location, Playlist_Sourceport, Playlist_SourcePort_HasParameters, Playlist_SourcePort_Parameters, Playlist_Iwad, Playlist_Wads);
                        VM.Playlist.Add(Playlist);
                    }
                }
            }
            Helper_UI();
        }

        private void Helper_Pwads(List<string> Files)
        {
            foreach (var File in Files)
            {
                var Duplicate = ((Playlists)ListView_Playlists.SelectedItem).Wadlist.SingleOrDefault(x => (x.Wad_File.ToLower() == Path.GetFileName(File).ToLower() & x.Wad_Location.ToLower() == Path.GetDirectoryName(File).ToLower()));
                if (Duplicate == null)
                {
                    Wads Pwad = new Wads(true, true, Path.GetFileName(File), false, Path.GetDirectoryName(File));
                    ((Playlists)ListView_Playlists.SelectedItem).Wadlist.Add(Pwad);
                }
            }
            ((Playlists)ListView_Playlists.SelectedItem).Playlist_Files = ((Playlists)ListView_Playlists.SelectedItem).Wadlist.Count;
            Helper_UI();
        }

        private void Helper_Save(Playlists Playlist, string File)
        {
            PlaylistIO Dpf = new PlaylistIO(File);
            Dpf.DeleteSection("SourcePort");
            Dpf.DeleteSection("IWAD");
            Dpf.DeleteSection("PWAD");
            Dpf.Write("Path", Playlist.Playlist_SourcePort.ToString(), "SourcePort");
            if (Playlist.Playlist_SourcePort_HasParameters == true)
            {
                Dpf.Write("Parameters", Playlist.Playlist_SourcePort_Parameters.ToString(), "SourcePort");
            }
            Dpf.Write("Path", Playlist.Playlist_IWad.ToString(), "IWAD");
            Dpf.Write("Counter", Playlist.Playlist_Files.ToString(), "PWAD");
            for (int i = 0; i < Playlist.Wadlist.Count; i++)
            {
                Dpf.Write("Path" + (i + 1).ToString(), Path.Combine(
                    Playlist.Wadlist[i].Wad_Location,
                    Playlist.Wadlist[i].Wad_File) + "|" +
                    Playlist.Wadlist[i].Wad_Load.ToString() + "|" +
                    Playlist.Wadlist[i].Wad_Merge.ToString()
                    , "PWAD");
            }
            Helper_UI();
        }

        private void Helper_Settings_Read()
        {
            PlaylistIO Settings = new PlaylistIO(Settings_File);
            if (Boolean.Parse(Settings.Read("Maximized", "Window Settings")))
            {
                Width = Double.Parse(Settings.Read("Width", "Window Settings"));
                Height = Double.Parse(Settings.Read("Height", "Window Settings"));
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Width = Double.Parse(Settings.Read("Width", "Window Settings"));
                Height = Double.Parse(Settings.Read("Height", "Window Settings"));
            }
            if (Settings.KeyExists("X_Position", "Window Settings"))
            {
                Left = Double.Parse(Settings.Read("X_Position", "Window Settings"));
            }
            if (Settings.KeyExists("Y_Position", "Window Settings"))
            {
                Top = Double.Parse(Settings.Read("Y_Position", "Window Settings"));
            }
            if (Settings.KeyExists("Splitter", "Window Settings"))
            {
                Grid_Left.ColumnDefinitions[0].Width = new GridLength(Double.Parse(Settings.Read("Splitter", "Window Settings")), GridUnitType.Pixel);
            }
            Folder_Playlists_New = Settings.Read("Playlists_New", "Paths");
            Folder_Playlists_Import = Settings.Read("Playlists_Import", "Paths");
            Folder_Playlists_Open = Settings.Read("Playlists_Open", "Paths");
            Folder_Playlists_Save_As = Settings.Read("Playlists_Save_As", "Paths");
            Folder_SourcePort_Open = Settings.Read("SourcePort_Open", "Paths");
            Folder_IWad_Open = Settings.Read("IWad_Open", "Paths");
            Folder_PWads_Import = Settings.Read("PWads_Import", "Paths");
            Folder_PWads_Open = Settings.Read("PWads_Open", "Paths");
            if (Settings.KeyExists("Save", "Session"))
            {
                if (Boolean.Parse(Settings.Read("Save", "Session")))
                {
                    ToogleButton_Save_Session.IsChecked = true;
                }
                else
                {
                    ToogleButton_Save_Session.IsChecked = false;
                }
            }
            if (Settings.KeyExists("Counter", "Playlists"))
            { 
                int Playlist_Files = Int32.Parse(Settings.Read("Counter", "Playlists"));
                List<string> Files = new List<string>();
                for (int i = 1; i <= Playlist_Files; ++i)
                {
                    if (File.Exists(Settings.Read("Playlist" + i.ToString(), "Playlists")))
                    { 
                        Files.Add(Settings.Read("Playlist" + i.ToString(), "Playlists"));
                    }
                }
                Helper_Playlists(Files);
            }
        }

        private void Helper_Settings_Write()
        {
            PlaylistIO Settings = new PlaylistIO(Settings_File);
            switch (WindowState)
            {
                case (WindowState.Maximized):
                    {
                        Settings.Write("Maximized", "True", "Window Settings");
                        break;
                    }
                case (WindowState.Minimized):
                    {
                        Settings.Write("Maximized", "False", "Window Settings");
                        Settings.Write("Width", Width.ToString(), "Window Settings");
                        Settings.Write("Height", Height.ToString(), "Window Settings");
                        break;
                    }
                case (WindowState.Normal):
                    {
                        Settings.Write("Maximized", "False", "Window Settings");
                        Settings.Write("Width", Width.ToString(), "Window Settings");
                        Settings.Write("Height", Height.ToString(), "Window Settings");
                        break;
                    }
            }
            Settings.Write("X_Position", Left.ToString(), "Window Settings");
            Settings.Write("Y_Position", Top.ToString(), "Window Settings");
            Settings.Write("Splitter", Grid_Left.ColumnDefinitions[0].ActualWidth.ToString(), "Window Settings");
            Settings.Write("Playlists_New", Folder_Playlists_New.ToString(), "Paths");
            Settings.Write("Playlists_Import", Folder_Playlists_Import.ToString(), "Paths");
            Settings.Write("Playlists_Open", Folder_Playlists_Open.ToString(), "Paths");
            Settings.Write("Playlists_Save_As", Folder_Playlists_Save_As.ToString(), "Paths");
            Settings.Write("SourcePort_Open", Folder_SourcePort_Open.ToString(), "Paths");
            Settings.Write("IWad_Open", Folder_IWad_Open.ToString(), "Paths");
            Settings.Write("PWads_Import", Folder_PWads_Import.ToString(), "Paths");
            Settings.Write("PWads_Open", Folder_PWads_Open.ToString(), "Paths");
            Settings.Write("Save", ToogleButton_Save_Session.IsChecked.ToString(), "Session");
            if (ToogleButton_Save_Session.IsChecked.Value)
            {
                Settings.DeleteSection("Playlists");
                Settings.Write("Counter", VM.Playlist.Count().ToString(), "Playlists");
                for (int i = 0; i < ListView_Playlists.Items.Count; i++)
                {
                    Settings.Write("Playlist" + (i + 1).ToString(), Path.Combine(
                        ((Playlists)ListView_Playlists.Items.GetItemAt(i)).Playlist_Location,
                        ((Playlists)ListView_Playlists.Items.GetItemAt(i)).Playlist_Name + ".dpf")
                        , "Playlists");
                }
            }
        }

        private void Helper_UI()
        {
            if (ListView_Playlists.View is GridView GV_Playlists)
            {
                foreach (GridViewColumn GVC_Playlists in GV_Playlists.Columns)
                {
                    GVC_Playlists.Width = GVC_Playlists.ActualWidth;
                    GVC_Playlists.Width = Double.NaN;
                }
            }
            if (ListView_Pwads.View is GridView GV_Pwads)
            {
                foreach (GridViewColumn GVC_Pwads in GV_Pwads.Columns)
                {
                    GVC_Pwads.Width = GVC_Pwads.ActualWidth;
                    GVC_Pwads.Width = Double.NaN;
                }
            }
            if (ListView_Playlists.Items.Count > 0)
            {
                Button_Playlist_Clear.IsEnabled = true;
            }
            else
            {
                Button_Playlist_Clear.IsEnabled = false;
            }
            if (TextBlock_SourcePort.Text.ToString() == "Not found"
                | TextBlock_Iwad.Text.ToString() == "Not found"
                | String.IsNullOrEmpty(TextBlock_SourcePort.Text.ToString())
                | String.IsNullOrEmpty(TextBlock_Iwad.Text.ToString()) 
                | ListView_Playlists.SelectedItems.Count > 1)
            {
                Button_Play.IsEnabled = false;
            }
            else
            {
                Button_Play.IsEnabled = true;
            }
            if (String.IsNullOrEmpty(TextBlock_SourcePort.Text.ToString()) | ListView_Playlists.SelectedItems.Count > 1)
            {
                CheckBox_Parameters.IsEnabled = false;
            }
            else
            {
                CheckBox_Parameters.IsEnabled = true;
            }
            if (ListView_Playlists.SelectedItems.Count < 1)
            {
                Button_Iwad_Open.IsEnabled = false;
                Button_Playlist_Save.IsEnabled = false;
                Button_Playlist_Save_As.IsEnabled = false;
                Button_Pwad_Clear.IsEnabled = false;
                Button_Pwad_Import.IsEnabled = false;
                Button_Pwad_Open.IsEnabled = false;
                Button_SourcePort_Open.IsEnabled = false;
                ListView_Pwads.IsEnabled = false;
            }
            else if (ListView_Playlists.SelectedItems.Count == 1)
            {
                Button_Iwad_Open.IsEnabled = true;
                Button_Playlist_Save.IsEnabled = true;
                Button_Playlist_Save_As.IsEnabled = true;
                Button_Pwad_Clear.IsEnabled = true;
                Button_Pwad_Import.IsEnabled = true;
                Button_Pwad_Open.IsEnabled = true;
                Button_SourcePort_Open.IsEnabled = true;
                ListView_Pwads.IsEnabled = true;
            }
            else 
            {
                Button_Iwad_Open.IsEnabled = false;
                Button_Playlist_Save.IsEnabled = true;
                Button_Playlist_Save_As.IsEnabled = true;
                Button_Pwad_Clear.IsEnabled = false;
                Button_Pwad_Import.IsEnabled = false;
                Button_Pwad_Open.IsEnabled = false;
                Button_SourcePort_Open.IsEnabled = false;
                ListView_Pwads.IsEnabled = false;
            }
            if (ListView_Pwads.Items.Count < 1 | ListView_Playlists.SelectedItems.Count > 1)
            {
                Button_Pwad_Clear.IsEnabled = false;
            }
            else
            {
                Button_Pwad_Clear.IsEnabled = true;
            }
        }

        private string AddQuotesIfRequired(string Path_String)
        {
            return !string.IsNullOrEmpty(Path_String) ?
                Path_String.Contains(" ") ? "\"" + Path_String + "\"" : Path_String
                : string.Empty;
        }

    }
}

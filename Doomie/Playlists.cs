﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace Doomie
{
    class Playlists : INotifyPropertyChanged
    {
        public Uri Playlist_Icon
        {
            get
            {
                Uri Icon = new Uri(@Path.Combine(Playlist_Location, Playlist_Name).ToString() + ".png", UriKind.RelativeOrAbsolute);
                if (!File.Exists(Icon.LocalPath))
                {
                    Icon = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Doomie.ico"));
                }
                return Icon;
            }
        }

        public bool Playlist_Changed
        {
            get { return _Playlist_Changed; }
            set
            {
                if (_Playlist_Changed != value)
                {
                    _Playlist_Changed = value;
                    OnPropertyChanged("Playlist_Changed");
                }
            }
        }

        public string Playlist_Name
        {
            get { return _Playlist_Name; }
            set
            {
                if (_Playlist_Name != value)
                {
                    _Playlist_Name = value;
                    OnPropertyChanged("Playlist_Name");
                }
            }
        }

        public int Playlist_Files
        {
            get { return _Playlist_Files; }
            set
            {
                if (_Playlist_Files != value)
                {
                    Playlist_Changed = true;
                    _Playlist_Files = value;
                    OnPropertyChanged("Playlist_Files");
                }
            }
        }

        public string Playlist_Location
        {
            get { return _Playlist_Location; }
            set
            {
                if (_Playlist_Location != value)
                {
                    _Playlist_Location = value;
                    OnPropertyChanged("Playlist_Location");
                }
            }
        }

        public string Playlist_SourcePort
        {
            get { return _Playlist_SourcePort; }
            set
            {
                if (_Playlist_SourcePort != value)
                {
                    Playlist_Changed = true;
                    _Playlist_SourcePort = value;
                    OnPropertyChanged("Playlist_SourcePort");
                }
            }
        }

        public string Playlist_SourcePort_Description
        {
            get
            {
                if (string.IsNullOrEmpty(_Playlist_SourcePort))
                {
                    _Playlist_SourcePort_Description = "";
                }
                else
                { 
                    if (File.Exists(_Playlist_SourcePort) != true)
                    {
                        _Playlist_SourcePort_Description = "Not found";
                    }
                    else
                    {
                        FileVersionInfo SourcePort_File_Info = FileVersionInfo.GetVersionInfo(_Playlist_SourcePort);
                        string SourcePort_Name = SourcePort_File_Info.FileDescription;
                        if (SourcePort_Name != null)
                        {
                            _Playlist_SourcePort_Description = SourcePort_Name + " Version " + SourcePort_File_Info.ProductMajorPart + "." + SourcePort_File_Info.ProductMinorPart + " [" + SourcePort_File_Info.FileVersion + "]";
                        }
                        else
                        {
                            _Playlist_SourcePort_Description = Path.GetFullPath(SourcePort_File_Info.FileName);
                        }
                    }
                }
                return _Playlist_SourcePort_Description;
            }
            set
            {
                if (_Playlist_SourcePort_Description != value)
                {
                    _Playlist_SourcePort_Description = value;
                    OnPropertyChanged("Playlist_SourcePort_Description");
                }
            }
        }

        public bool Playlist_SourcePort_HasParameters
        {
            get
            {
                if (_Playlist_SourcePort_HasParameters == false)
                {
                    _Playlist_SourcePort_HasParameters = false;
                }
                return _Playlist_SourcePort_HasParameters;
            }
            set
            {
                if (_Playlist_SourcePort_HasParameters != value)
                {
                    _Playlist_SourcePort_HasParameters = value;
                    OnPropertyChanged("Playlist_SourcePort_HasParameters");
                }
            }
        }

        public string Playlist_SourcePort_Parameters
        {
            get { return _Playlist_SourcePort_Parameters; }
            set
            {
                if (_Playlist_SourcePort_Parameters != value)
                {
                    Playlist_Changed = true;
                    _Playlist_SourcePort_Parameters = value;
                    OnPropertyChanged("Playlist_SourcePort_Parameters");
                }
            }
        }

        public string Playlist_IWad
        {
            get { return _Playlist_IWad; }
            set
            {
                if (_Playlist_IWad != value)
                {
                    Playlist_Changed = true;
                    _Playlist_IWad = value;
                    OnPropertyChanged("Playlist_IWad");
                }
            }
        }

        public string Playlist_IWad_Description
        {
            get
            {
                if (string.IsNullOrEmpty(_Playlist_IWad))
                {
                    _Playlist_IWad_Description = "";
                }
                else
                { 
                    if (File.Exists(_Playlist_IWad) != true)
                    {
                        _Playlist_IWad_Description = "Not found";
                    }
                    else
                    { 
                        string IWad_Description = new Wads().Get_Wad_Description(_Playlist_IWad);
                        if (string.IsNullOrEmpty(IWad_Description))
                        {
                            _Playlist_IWad_Description = Playlist_IWad;
                        }
                        else
                        {
                            _Playlist_IWad_Description = IWad_Description;
                        }
                    }
                }
                return _Playlist_IWad_Description;
            }
            set
            {
                if (_Playlist_IWad_Description != value)
                {
                    _Playlist_IWad_Description = value;
                    OnPropertyChanged("Playlist_IWad_Description");
                }
            }
        }

        public ObservableCollection<Wads> Wadlist
        {
            get { return _Wadlist; }
            set
            {
                if (_Wadlist != value)
                {
                    Playlist_Changed = true;
                    _Wadlist = value;
                    OnPropertyChanged("Wadlist");
                }
            }
        }

        public bool Playlist_Save
        {
            get { return _Playlist_Save; }
            set
            {
                if (_Playlist_Save != value)
                {
                    _Playlist_Save = value;
                    OnPropertyChanged("Playlist_Save");
                }
            }
        }

        public Playlists(bool Playlist_Changed, bool Playlist_Save, string Playlist_Name, int Playlist_Files, string Playlist_Location, string Playlist_SourcePort, bool Playlist_SourcePort_HasParameters, string Playlist_SourcePort_Parameters, string Playlist_IWad, ObservableCollection<Wads> Wadlist = null, string Playlist_SourcePort_Description = null, string Playlist_IWad_Description = null)
        {
            _Playlist_Changed = Playlist_Changed;
            _Playlist_Save = Playlist_Save;
            _Playlist_Name = Playlist_Name;
            _Playlist_Files = Playlist_Files;
            _Playlist_Location = Playlist_Location;
            _Playlist_SourcePort = Playlist_SourcePort;
            _Playlist_SourcePort_HasParameters = Playlist_SourcePort_HasParameters;
            _Playlist_SourcePort_Parameters = Playlist_SourcePort_Parameters;
            _Playlist_SourcePort_Description = Playlist_SourcePort_Description;
            _Playlist_IWad = Playlist_IWad;
            _Playlist_IWad_Description = Playlist_IWad_Description;
            if(Wadlist != null)
            {
                _Wadlist = Wadlist;
            }
            else
            { 
                _Wadlist = new ObservableCollection<Wads>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _Playlist_Changed;
        private bool _Playlist_Save;
        private string _Playlist_Name;
        private int _Playlist_Files;
        private string _Playlist_Location;
        private string _Playlist_SourcePort;
        private string _Playlist_SourcePort_Description;
        private bool _Playlist_SourcePort_HasParameters;
        private string _Playlist_SourcePort_Parameters;
        private string _Playlist_IWad;
        private string _Playlist_IWad_Description;
        private ObservableCollection<Wads> _Wadlist;

        void OnPropertyChanged(string Property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }
    }
}

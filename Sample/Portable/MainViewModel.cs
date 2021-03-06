﻿using Rox;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace RoxSample
{
    public class MainViewModel
        : INotifyPropertyChanged
    {
        private readonly VideoView VideoView;
        public MainViewModel(VideoView videoView)
        {
            VideoView = videoView;
        }

        private bool _AutoPlay = false;
        public bool AutoPlay
        {
            get
            {
                return _AutoPlay;
            }
            set
            {
                _AutoPlay = value;

                OnPropertyChanged(nameof(AutoPlay));
            }
        }

        private bool _LoopPlay = false;
        public bool LoopPlay
        {
            get
            {
                return _LoopPlay;
            }
            set
            {
                _LoopPlay = value;

                OnPropertyChanged(nameof(LoopPlay));
            }
        }

        private bool _ShowController = false;
        public bool ShowController
        {
            get
            {
                return _ShowController;
            }
            set
            {
                _ShowController = value;

                OnPropertyChanged(nameof(ShowController));
            }
        }

        private bool _Muted = false;
        public bool Muted
        {
            get
            {
                return _Muted;
            }
            set
            {
                _Muted = value;

                OnPropertyChanged(nameof(Muted));
            }
        }

        private double _Volume = 1;
        public double Volume
        {
            get
            {
                return _Volume;
            }
            set
            {
                _Volume = value;

                OnPropertyChanged(nameof(Volume));
                OnPropertyChanged(nameof(SliderVolume));
            }
        }

        public double SliderVolume
        {
            get
            {
                return _Volume * 100;
            }
            set
            {
                try
                {
                    _Volume = value / 100;
                }
                catch
                {
                    _Volume = 0;
                }

                OnPropertyChanged(nameof(Volume));
                OnPropertyChanged(nameof(SliderVolume));
            }
        }

        private string _LabelVideoStatus;
        public string LabelVideoStatus
        {
            get
            {
                return _LabelVideoStatus;
            }
        }

        public ICommand PropertyChangedCommand
        {
            get
            {
                return new Command<string>((propertyName) =>
                {
                    switch (propertyName)
                    {
                        case nameof(VideoView.VideoState):
                            {
                                _LabelVideoStatus = VideoView.VideoState.ToString();

                                OnPropertyChanged(nameof(LabelVideoStatus));
                                break;
                            }
                    }
                });
            }
        }

        private string _Source = "http://www.sample-videos.com/video/mp4/720/big_buck_bunny_720p_1mb.mp4";
        public string EntrySource
        {
            get
            {
                return _Source;
            }
            set
            {
                _Source = value;

                OnPropertyChanged(nameof(EntrySource));
                OnPropertyChanged(nameof(VideoSource));
            }
        }

        public ImageSource VideoSource
        {
            get
            {
                ImageSource videoSource = null;
                try
                {
                    ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
                    videoSource = (ImageSource)imageSourceConverter.ConvertFromInvariantString(_Source);
                }
                catch
                {
                }
                return videoSource;
            }
        }

        public ICommand PlayCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await VideoView.Start();
                });
            }
        }

        public ICommand PauseCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await VideoView.Pause();
                });
            }
        }

        public ICommand StopCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await VideoView.Stop();
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs eventArgs = new PropertyChangedEventArgs(propertyName);

            PropertyChanged?.Invoke(this, eventArgs);
        }
    }
}
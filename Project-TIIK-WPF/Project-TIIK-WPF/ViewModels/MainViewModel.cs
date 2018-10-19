﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_TIIK_WPF.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private Page LetterFrequencyPage;
        private Page AboutPage;

        private Page _currentPage;
        public Page CurrentPage {
            get => _currentPage;
            set {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private double _frameOpacity;
        public double FrameOpacity { get => _frameOpacity; set => _frameOpacity = value; }

        public MainViewModel()
        {
            LetterFrequencyPage = new Pages.LetterFrequencyPage();
            AboutPage = new Pages.AboutPage();

            FrameOpacity = 1;
            CurrentPage = LetterFrequencyPage;
        }

        public ICommand LetterFrequency_Click
        {
            get {
                return new RelayCommand(
                    () =>
                    {
                        SlowOpacity(LetterFrequencyPage);
                    });
            }
        }

        public ICommand About_Click
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        SlowOpacity(AboutPage);
                    });
            }
        }

        private async void SlowOpacity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(10);
                }
                CurrentPage = page;
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    FrameOpacity = i;
                    Thread.Sleep(10);
                }
            });
        }
    }
}
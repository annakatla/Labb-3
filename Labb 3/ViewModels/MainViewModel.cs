﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb_3.Managers;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb_3.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly NavigationManager _navigationManager;

        public ObservableObject CurrentViewModel => _navigationManager.CurrentViewModel;

        public MainViewModel(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _navigationManager.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

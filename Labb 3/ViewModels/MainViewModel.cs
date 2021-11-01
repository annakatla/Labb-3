using System;
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
        private readonly QuizManager _quizManager;

        //Koppla huvudfönstret till NMs vymodell. i Huvudfönstrets XAML-fil sätts de vyer som används vid respektive vymodell.
        public ObservableObject CurrentViewModel => _navigationManager.CurrentViewModel;

        //Konstruktor som kopplar vår NM till 
        public MainViewModel(NavigationManager navigationManager, QuizManager quizManager)
        {
            _navigationManager = navigationManager;
            _navigationManager.CurrentViewModelChanged += OnCurrentViewModelChanged;

            _quizManager = quizManager;
            //Vad behöver jag göra mer här?

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

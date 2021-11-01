using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using Labb_3.Managers;
using Labb_3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb_3.ViewModels
{
    public class StartViewModel : ObservableObject
    {
        private readonly NavigationManager _navigationManager;
        private readonly QuizManager _quizManager;
        private readonly FileManager _fileManager;
        public RelayCommand EditQuizCommand { get; }
        public AsyncRelayCommand PlayQuizCommand { get; }
        public RelayCommand CreateQuizCommand { get; }



        public StartViewModel(NavigationManager navigationManager, QuizManager quizManager, FileManager fileManager)
        {
            _navigationManager = navigationManager;
            _quizManager = quizManager;
            _fileManager = fileManager;

            EditQuizCommand = new RelayCommand(GoToEdit);
            PlayQuizCommand = new AsyncRelayCommand(GoToPlay);
            CreateQuizCommand = new RelayCommand(GoToCreate);

        }
        private async Task GoToPlay()
        {
            await Task.Run(() => { _quizManager.AllQuizzes = new ObservableCollection<Quiz>(_fileManager.LoadQuizAsync().Result); });
            _navigationManager.CurrentViewModel = new PlayQuizViewModel(_navigationManager, _quizManager, _fileManager);
        }

        private void GoToCreate()
        {
            _navigationManager.CurrentViewModel = new CreateQuizViewModel(_navigationManager, _quizManager, _fileManager);
        }

        private void GoToEdit()
        {
            _navigationManager.CurrentViewModel = new EditQuizViewModel(_navigationManager, _quizManager, _fileManager);
        }


    }
}

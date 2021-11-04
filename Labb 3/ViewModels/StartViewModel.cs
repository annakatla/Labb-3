using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        public AsyncRelayCommand EditQuizCommand { get; }
        public AsyncRelayCommand PlayQuizCommand { get; }
        public AsyncRelayCommand CreateQuizCommand { get; }

        public StartViewModel(NavigationManager navigationManager, QuizManager quizManager)
        {
            _navigationManager = navigationManager;
            _quizManager = quizManager;

            EditQuizCommand = new AsyncRelayCommand(GoToEditAsync);
            PlayQuizCommand = new AsyncRelayCommand(GoToPlayAsync);
            CreateQuizCommand = new AsyncRelayCommand(GoToCreateAsync);

        }

        private async Task GoToPlayAsync()
        {
            await Task.Run(() =>
            {
                _quizManager.AllQuizzes = new ObservableCollection<Quiz>(_quizManager.LoadQuizAsync().Result);
            });
            _navigationManager.CurrentViewModel = new PlayQuizViewModel(_navigationManager, _quizManager);
        }

        private async Task GoToCreateAsync()
        {
            await Task.Run(() => { _quizManager.AllQuizzes = new ObservableCollection<Quiz>(_quizManager.LoadQuizAsync().Result); });
            _navigationManager.CurrentViewModel = new CreateQuizViewModel(_navigationManager, _quizManager);
        }

        private async Task GoToEditAsync()
        {
            await Task.Run(() => { _quizManager.AllQuizzes = new ObservableCollection<Quiz>(_quizManager.LoadQuizAsync().Result); });
            _navigationManager.CurrentViewModel = new EditQuizViewModel(_navigationManager, _quizManager);
        }

    }
}

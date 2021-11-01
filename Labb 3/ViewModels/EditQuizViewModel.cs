using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb_3.Managers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb_3.ViewModels
{
    //När man editerar ett quiz så skall man få en överblick av samtliga frågor och välja en av dessa för att kunna ändra dess statement och/eller svarsalternativ.
    public class EditQuizViewModel : ObservableObject
    {
        private readonly NavigationManager _navigationManager;
        private readonly QuizManager _quizManager;
        private readonly FileManager _fileManager;
        public RelayCommand GoToStartCommand { get; }

        public EditQuizViewModel(NavigationManager navigationManager, QuizManager quizManager, FileManager fileManager)
        {
            _navigationManager = navigationManager;
            _quizManager = quizManager;
            _fileManager = fileManager;

            GoToStartCommand = new RelayCommand(GoToStart);
        }

        private void GoToStart()
        {
            _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager, _fileManager);
        }

    }
}

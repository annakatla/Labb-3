using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb_3.Managers;
using Labb_3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb_3.ViewModels
{
    //När man editerar ett quiz så skall man få en överblick av samtliga frågor och välja en av dessa för att kunna ändra dess statement och/eller svarsalternativ.
    public class EditQuizViewModel : ObservableObject
    {
        private readonly NavigationManager _navigationManager;
        private readonly QuizManager _quizManager;

        public ObservableCollection<Quiz> AvailableQuizzes => _quizManager.AllQuizzes;

        private Quiz _currentQuiz;
        public Quiz CurrentQuiz
        {
            get { return _currentQuiz; }
            set
            {
                SetProperty(ref _currentQuiz, value);
                OnPropertyChanged(nameof(Answers));
            }
        }

        private Question _currentQuestion;
        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                SetProperty(ref _currentQuestion, value);
            }
        }

        private string _answer1;
        public string Answer1
        {
            get { return _answer1; }
            set
            {
                _answer1 = CurrentQuestion.Answers[0];
                SetProperty(ref _answer1, value);
            }
        }

        private string _answer2;
        public string Answer2
        {
            get { return _answer2; }
            set
            {
                _answer2 = CurrentQuestion.Answers[1];
                SetProperty(ref _answer2, value);
            }
        }

        private string _answer3;
        public string Answer3
        {
            get { return _answer3; }
            set
            {
                _answer3 = CurrentQuestion.Answers[2];
                SetProperty(ref _answer3, value);
            }
        }

        private List<string> _answers;
        public List<string> Answers
        {
            get { return _answers; }
            set { SetProperty(ref _answers, value); }
        }


        public RelayCommand GoToStartCommand { get; }
        public RelayCommand SaveQuestionCommand { get; }
        public RelayCommand AddQuestionCommand { get; }
        public AsyncRelayCommand SaveQuizCommand { get; }

        public EditQuizViewModel(NavigationManager navigationManager, QuizManager quizManager)
        {
            _navigationManager = navigationManager;
            _quizManager = quizManager;
            CurrentQuiz = AvailableQuizzes[0];
            CurrentQuestion = CurrentQuiz.Questions.ToList()[0];
            Answers = CurrentQuestion.Answers.ToList();
            GoToStartCommand = new RelayCommand(GoToStart);
            SaveQuestionCommand = new RelayCommand(UpdateQuestion);
            AddQuestionCommand = new RelayCommand(AddQuestion);
            PropertyChanged += OnViewModelPropertyChanged;
        }


        private void AddQuestion()
        {
            throw new NotImplementedException();
        }

        private void UpdateQuestion()
        {
            throw new NotImplementedException();
        }

        private void GoToStart()
        {
            _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager);
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}

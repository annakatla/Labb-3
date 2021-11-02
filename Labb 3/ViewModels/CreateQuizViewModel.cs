using Labb_3.Managers;
using Labb_3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Labb_3.ViewModels
{
    public class CreateQuizViewModel : ObservableObject
    {
        #region Managers
        
        private readonly QuizManager _quizManager;
        private readonly NavigationManager _navigationManager;

        #endregion

        private Quiz _creatingQuiz;
        public Quiz CreatingQuiz
        {
            get { return _creatingQuiz; }
            set {SetProperty(ref _creatingQuiz, value); }
        }


        private string _quizTitle;
        public string QuizTitle
        {
            get { return _quizTitle; }
            set
            {
                SetProperty(ref _quizTitle, value);
                OnPropertyChanged(nameof(CreatingQuiz.Title));
            }
        }

        #region AnswerStuff

        private string _answer1;
        public string Answer1
        {
            get { return _answer1; }
            set
            {
                SetProperty(ref _answer1, value);
                Answers[0] = _answer1;
                OnPropertyChanged(nameof(CorrectAnswer));
            }
        }

        private string _answer2;
        public string Answer2
        {
            get { return _answer2; }
            set
            {
                SetProperty(ref _answer2, value);
                Answers[1] = _answer2;
                OnPropertyChanged(nameof(CorrectAnswer));
            }
        }

        private string _answer3;
        public string Answer3
        {
            get { return _answer3; }
            set { SetProperty(ref _answer3, value); 
                Answers[2] = _answer3;
                OnPropertyChanged(nameof(CorrectAnswer));
            }
        }

        private int _correctAnswer;
        public int CorrectAnswer
        {
            get { return _correctAnswer; }
            set { SetProperty(ref _correctAnswer, value); }
        }

        private readonly ObservableCollection<string> _answers = new() {"", "", ""};

        public ObservableCollection<string> Answers
        {
            get { return _answers; }
        }


        #endregion

        #region QuestionStuff

        private string _questionStatement;
        public string QuestionStatement
        {
            get { return _questionStatement; }
            set { SetProperty(ref _questionStatement, value); }
        }
        
        private readonly ObservableCollection<string> _questions;
        public IEnumerable<string> Questions => _questions;

        #endregion

        #region Commands
        public RelayCommand GoToStartCommand { get; }
        public AsyncRelayCommand SaveQuizCommand { get; }
        public RelayCommand AddQuestionCommand { get; }
        public RelayCommand CreateQuizCommand { get; }

        #endregion

        public CreateQuizViewModel(NavigationManager navigationManager, QuizManager quizManager)
        {
            _navigationManager = navigationManager;
            _quizManager = quizManager;
            CorrectAnswer = 0;
            _questions = new ObservableCollection<string>();
            GoToStartCommand = new RelayCommand(GoToStart);
            SaveQuizCommand = new AsyncRelayCommand(SaveToFileAsync, CanSaveToFile);
            AddQuestionCommand = new RelayCommand(AddQuestion, CanAddQuestion);
            CreateQuizCommand = new RelayCommand(CreateQuiz, CanCreateQuiz);
            PropertyChanged += OnViewModelPropertyChanged;
        }

        #region Methods

        private bool CanCreateQuiz()
        {
            if (!string.IsNullOrWhiteSpace(_quizTitle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreateQuiz()
        {
            _creatingQuiz = new Quiz(_quizTitle);
            _quizManager.AllQuizzes.Add(_creatingQuiz);
        }


        private bool CanAddQuestion()
        {
            if (!string.IsNullOrWhiteSpace(_quizTitle) && 
                !string.IsNullOrWhiteSpace(_questionStatement) && 
                !string.IsNullOrWhiteSpace(_answer1) && 
                !string.IsNullOrWhiteSpace(_answer2) && 
                !string.IsNullOrWhiteSpace(_answer3))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AddQuestion()
        {
            _creatingQuiz.Questions.Add(new Question(_questionStatement, _correctAnswer, _answer1, _answer2, _answer3));
            _questions.Add(_questionStatement);

            QuestionStatement = null;
            CorrectAnswer = 0;
            Answer1 = "";
            Answer2 = "";
            Answer3 = "";
            _answers[0] = "";
            _answers[1] = "";
            _answers[2] = "";
        }

        private bool CanSaveToFile()
        {
            if (_creatingQuiz != null && _creatingQuiz.Questions != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task SaveToFileAsync()
        {
            await _quizManager.SaveToFileAsync();
        }

        private void GoToStart()
        {
            _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveQuizCommand.NotifyCanExecuteChanged();
            AddQuestionCommand.NotifyCanExecuteChanged();
            CreateQuizCommand.NotifyCanExecuteChanged();
        }

        #endregion

    }
}

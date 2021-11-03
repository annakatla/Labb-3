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
using System.Windows;

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
            set
            {
                SetProperty(ref _creatingQuiz, value);
            }
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
            set 
            { 
                SetProperty(ref _answer3, value); 
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


        private string _questionStatement;
        public string QuestionStatement
        {
            get { return _questionStatement; }
            set
            {
                SetProperty(ref _questionStatement, value);
            }
        }

        public ObservableCollection<string> ListOfQuestions
        {
            get { return CreatingQuiz != null? new ObservableCollection<string>(CreatingQuiz.Questions.Select(q => q.Statement)): new ObservableCollection<string>(); }
        }

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
            GoToStartCommand = new RelayCommand(GoToStart);
            SaveQuizCommand = new AsyncRelayCommand(SaveToFileAsync, CanSaveToFile);
            AddQuestionCommand = new RelayCommand(AddQuestion, CanAddQuestion);
            CreateQuizCommand = new RelayCommand(CreateQuiz, CanCreateQuiz);

            PropertyChanged += OnViewModelPropertyChanged;
        }

        #region Methods

        private bool CanCreateQuiz()
        {
            bool canCreate = false;
            foreach (var quiz in _quizManager.AllQuizzes)
            {
                if (!string.IsNullOrWhiteSpace(_quizTitle) && _quizTitle.ToLower() != quiz.Title.ToLower())
                {
                    canCreate = true;
                }
                else
                {
                    canCreate = false;
                    break;
                }
            }
            return canCreate;
        }
        private void CreateQuiz()
        {
            CreatingQuiz = new Quiz(_quizTitle);
            _quizManager.AllQuizzes.Add(CreatingQuiz);
        }

        private bool CanAddQuestion()
        {
            if (CreatingQuiz != null && 
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
            CreatingQuiz.AddQuestion(_questionStatement, _correctAnswer, _answer1, _answer2, _answer3);
            OnPropertyChanged(nameof(ListOfQuestions));

            QuestionStatement = "";
            CorrectAnswer = 0;
            Answer1 = "";
            Answer2 = "";
            Answer3 = "";
            Answers[0] = "";
            Answers[1] = "";
            Answers[2] = "";
        }

        private bool CanSaveToFile()
        {
            if (CreatingQuiz != null && CreatingQuiz.Questions.Count != 0)
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
            MessageBox.Show("Your quiz was successfully saved.");
            GoToStart();
        }

        private void GoToStart()
        {
            CreatingQuiz = null;
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

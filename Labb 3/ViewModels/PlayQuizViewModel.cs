using Labb_3.Managers;
using Labb_3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Labb_3.ViewModels
{
    public class PlayQuizViewModel : ObservableObject
    {
        #region Managers

        private readonly NavigationManager _navigationManager;
        private readonly QuizManager _quizManager;

        #endregion

        public ObservableCollection<Quiz> AvailableQuizzes => _quizManager.AllQuizzes;

        private Quiz _currentQuiz;
        public Quiz CurrentQuiz
        {
            get { return _currentQuiz; }
            set
            {
                SetProperty(ref _currentQuiz, value);
                OnPropertyChanged(nameof(NumberOfAnsweredQuestions));
                OnPropertyChanged(nameof(NumberOfCorrectAnswers));
                OnPropertyChanged(nameof(CurrentQuestion));
            }
        }

        private Question _currentQuestion;
        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set { SetProperty(ref _currentQuestion, value); }
        }

        private int _chosenAnswer;

        #region Integers for stats

        
        /// <summary>
        /// Integer för att räkna upp i vyn vid varje besvarad fråga.
        /// </summary>
        private int _numberOfAnsweredQuestions;
        public int NumberOfAnsweredQuestions
        {
            get { return _numberOfAnsweredQuestions; }
            set { SetProperty(ref _numberOfAnsweredQuestions, value); }
        }

        /// <summary>
        /// Integer för att räkna upp i vyn vid varje rätt svar.
        /// </summary>
        private int _numberOfCorrectAnswers;
        public int NumberOfCorrectAnswers
        {
            get { return _numberOfCorrectAnswers; }
            set { SetProperty(ref _numberOfCorrectAnswers, value); }
        }

        #endregion

        #region Commands

        public RelayCommand StartGameCommand { get; }
        public RelayCommand GoToStartCommand { get; }
        public RelayCommand ChooseAnswer1Command { get; } 
        public RelayCommand ChooseAnswer2Command { get; } 
        public RelayCommand ChooseAnswer3Command { get; }

        #endregion

        public PlayQuizViewModel(NavigationManager navigationManager, QuizManager quizManager)
        {
            _navigationManager = navigationManager;
            _quizManager = quizManager;

            CurrentQuiz = AvailableQuizzes[0];
            GoToStartCommand = new RelayCommand(GoToStart);
            ChooseAnswer1Command = new RelayCommand(ChooseAnswer1);
            ChooseAnswer2Command = new RelayCommand(ChooseAnswer2);
            ChooseAnswer3Command = new RelayCommand(ChooseAnswer3);
            StartGameCommand = new RelayCommand(StartGame);
        }

        private void StartGame()
        {
            CurrentQuestion = CurrentQuiz.GetRandomQuestion();
            NumberOfCorrectAnswers = 0;
            NumberOfAnsweredQuestions = 0;
        }
        
        #region AnswerMethods
        private void ChooseAnswer1()
        {
            _chosenAnswer = 0;
            ControlAnswer();
        }


        private void ChooseAnswer2()
        {
            _chosenAnswer = 1;
            ControlAnswer();
        }
        private void ChooseAnswer3()
        {
            _chosenAnswer = 2;
            ControlAnswer();
        }
        private void ControlAnswer()
        {
            NumberOfAnsweredQuestions++;
            if (_chosenAnswer == CurrentQuestion.CorrectAnswer)
            {
                NumberOfCorrectAnswers++;
                MessageBox.Show("Correct answer!");
            }
            else
            {
                MessageBox.Show("Wrong answer. :( ");
            }

            if (NumberOfAnsweredQuestions == CurrentQuiz.Questions.Count)
            {
                MessageBox.Show($"Thank you for playing! You got {NumberOfCorrectAnswers} of {CurrentQuiz.Questions.Count}.");
                GoToStart();
            }
            else
            {
                CurrentQuestion = CurrentQuiz.GetRandomQuestion();
            }
        }

        #endregion

        private void GoToStart()
        {
            _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager);
        }
    }
}

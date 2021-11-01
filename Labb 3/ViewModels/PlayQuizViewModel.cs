using Labb_3.Managers;
using Labb_3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Labb_3.ViewModels
{
    public class PlayQuizViewModel : ObservableObject
    {
        #region Managers

        private readonly NavigationManager _navigationManager;
        private readonly QuizManager _quizManager;
        private readonly FileManager _fileManager;

        #endregion


        public ObservableCollection<Quiz> AvailableQuizzes => _quizManager.AllQuizzes;

        private Quiz _currentQuiz;
        public Quiz CurrentQuiz
        {
            get { return _currentQuiz; }
            set
            {
                SetProperty(ref _currentQuiz, value);
                OnPropertyChanged(nameof(AnsweredQuestions));
                OnPropertyChanged(nameof(CorrectAnswers));
            }
        }

        private Question _currentQuestion;
        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set { SetProperty(ref _currentQuestion, value); }
        }

        private int _chosenAnswer;

        /// <summary>
        /// Integer för att räkna upp i vyn vid varje besvarad fråga.
        /// </summary>
        private int _answeredQuestions;
        public int AnsweredQuestions
        {
            get { return _answeredQuestions; }
            set { SetProperty(ref _answeredQuestions, value); }
        }

        /// <summary>
        /// Integer för att räkna upp i vyn vid varje rätt svar.
        /// </summary>
        private int _correctAnswers;
        public int CorrectAnswers
        {
            get { return _correctAnswers; }
            set { SetProperty(ref _correctAnswers, value); }
        }


        #region Commands

        public RelayCommand StartGameCommand { get; }
        public RelayCommand GoToStartCommand { get; }
        public RelayCommand ChooseAnswer1Command { get; } 
        public RelayCommand ChooseAnswer2Command { get; } 
        public RelayCommand ChooseAnswer3Command { get; }

        #endregion


        public PlayQuizViewModel(NavigationManager navigationManager, QuizManager quizManager, FileManager fileManager)
        {
            _navigationManager = navigationManager;
            _quizManager = quizManager;
            _fileManager = fileManager;

            CurrentQuiz = AvailableQuizzes[0];

            CorrectAnswers = 0;
            AnsweredQuestions = 0;

            GoToStartCommand = new RelayCommand(GoToStart);
            ChooseAnswer1Command = new RelayCommand(ChooseAnswer1);
            ChooseAnswer2Command = new RelayCommand(ChooseAnswer2);
            ChooseAnswer3Command = new RelayCommand(ChooseAnswer3);
            StartGameCommand = new RelayCommand(StartGame);

            PropertyChanged += OnViewModelPropertyChanged;
        }

        private void ChooseAnswer1()
        {
            AnsweredQuestions++;
            _chosenAnswer = 0;
            if (_chosenAnswer == CurrentQuestion.CorrectAnswer)
            {
                CorrectAnswers++;
                MessageBox.Show("Correct answer!");
            }
            else
            {
                MessageBox.Show("Wrong answer. :( ");
            }
            CurrentQuestion = CurrentQuiz.GetRandomQuestion();
        }
        private void ChooseAnswer2()
        {
            AnsweredQuestions++;
            _chosenAnswer = 1;
            if (_chosenAnswer == CurrentQuestion.CorrectAnswer)
            {
                CorrectAnswers++;
                MessageBox.Show("Correct answer!");
            }
            else
            {
                MessageBox.Show("Wrong answer. :( ");
            }
            CurrentQuestion = CurrentQuiz.GetRandomQuestion();
        }
        private void ChooseAnswer3()
        {
            AnsweredQuestions++;
            _chosenAnswer = 2;
            if (_chosenAnswer == CurrentQuestion.CorrectAnswer)
            {
                CorrectAnswers++;
                MessageBox.Show("Correct answer!");
            }
            else
            {
                MessageBox.Show("Wrong answer. :( ");
            }
            CurrentQuestion = CurrentQuiz.GetRandomQuestion();
        }


        private void GoToStart()
        {
            _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager, _fileManager);
        }
        private void StartGame()
        {
            CurrentQuestion = CurrentQuiz.GetRandomQuestion();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StartGameCommand.NotifyCanExecuteChanged();
            ChooseAnswer1Command.NotifyCanExecuteChanged();
            ChooseAnswer2Command.NotifyCanExecuteChanged();
            ChooseAnswer3Command.NotifyCanExecuteChanged();
        }

    }
}

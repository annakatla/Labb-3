using Labb_3.Managers;
using Labb_3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Labb_3.ViewModels
{
    public class EditQuizViewModel : ObservableObject
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
                OnPropertyChanged(nameof(CurrentQuestion.Answers));
                OnPropertyChanged(nameof(AvailableQuizzes));
            }
        }

        #region QuestionStuff

        private Question _currentQuestion;
        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                SetProperty(ref _currentQuestion, value);
                OnPropertyChanged(nameof(Answer1));
                AnswersCurrentQ[0] = Answer1;
                OnPropertyChanged(nameof(Answer2));
                AnswersCurrentQ[1] = Answer2;
                OnPropertyChanged(nameof(Answer3));
                AnswersCurrentQ[2] = Answer3;
            }
        }

        private string _newQuestionStatement;
        public string NewQuestionStatement
        {
            get { return _newQuestionStatement; }
            set
            {
                SetProperty(ref _newQuestionStatement, value);
            }
        }

        #endregion

        #region AnswerStuff

        private string _answer1;
        public string Answer1
        {
            get => CurrentQuestion != null ? CurrentQuestion.Answers[0] : string.Empty;
            set
            {
                SetProperty(ref CurrentQuestion.Answers[0], value);
                SetProperty(ref _answer1, value);
                AnswersCurrentQ[0] = Answer1;
                OnPropertyChanged(nameof(CurrentQuestion.CorrectAnswer));
            }
        }

        private string _answer2;
        public string Answer2
        {
            get => CurrentQuestion != null ? CurrentQuestion.Answers[1] : string.Empty;
            set
            {
                SetProperty(ref CurrentQuestion.Answers[1], value);
                SetProperty(ref _answer2, value);
                AnswersCurrentQ[1] = Answer2;
                OnPropertyChanged(nameof(CurrentQuestion.CorrectAnswer));
            }
        }

        private string _answer3;
        public string Answer3
        {
            get => CurrentQuestion != null ? CurrentQuestion.Answers[2] : string.Empty;
            set
            {
                SetProperty(ref CurrentQuestion.Answers[2], value);
                SetProperty(ref _answer3, value);
                AnswersCurrentQ[2] = Answer3;
                OnPropertyChanged(nameof(CurrentQuestion.CorrectAnswer));
            }
        }

        private readonly ObservableCollection<string> _answersCurrentQ = new() {"", "", ""};

        public ObservableCollection<string> AnswersCurrentQ
        { 
            get { return _answersCurrentQ; }
        }

        private string _newAnswer1;
        public string NewAnswer1
        {
            get { return _newAnswer1; }
            set
            {
                SetProperty(ref _newAnswer1, value);
                NewAnswers[0] = _newAnswer1;
                OnPropertyChanged(nameof(CorrectNewAnswer));
            }
        }

        private string _newAnswer2;
        public string NewAnswer2
        {
            get { return _newAnswer2; }
            set
            {
                SetProperty(ref _newAnswer2, value);
                NewAnswers[1] = _newAnswer2;
                OnPropertyChanged(nameof(CorrectNewAnswer));
            }
        }

        private string _newAnswer3;
        public string NewAnswer3
        {
            get { return _newAnswer3; }
            set
            {
                SetProperty(ref _newAnswer3, value);
                NewAnswers[2] = _newAnswer3;
                OnPropertyChanged(nameof(CorrectNewAnswer));
            }
        }

        private readonly ObservableCollection<string> _newAnswers = new() { "", "", "" };
        public ObservableCollection<string> NewAnswers
        {
            get { return _newAnswers; }
        }

        private int _correctNewAnswer;
        public int CorrectNewAnswer
        {
            get { return _correctNewAnswer; }
            set { SetProperty(ref _correctNewAnswer, value); }
        }

        #endregion

        #region Commands

        public RelayCommand GoToStartCommand { get; }
        public RelayCommand AddQuestionCommand { get; }
        public AsyncRelayCommand DeleteQuestionCommand { get; }
        public AsyncRelayCommand SaveQuizCommand { get; }
        public AsyncRelayCommand SaveQuestionCommand { get; }

        #endregion

        public EditQuizViewModel(NavigationManager navigationManager, QuizManager quizManager)
        {
            _navigationManager = navigationManager;
            _quizManager = quizManager;
            CurrentQuiz = AvailableQuizzes[0];
            CurrentQuestion = CurrentQuiz.Questions.ToList()[0];
            CorrectNewAnswer = 0;
            GoToStartCommand = new RelayCommand(GoToStart);
            AddQuestionCommand = new RelayCommand(AddQuestion, CanAddQuestion);
            SaveQuestionCommand = new AsyncRelayCommand(SaveQuestionAsync, CanSaveQuestion);
            DeleteQuestionCommand = new AsyncRelayCommand(DeleteQuestionAsync);
            SaveQuizCommand = new AsyncRelayCommand(SaveToFileAsync, CanSaveToFile);
            PropertyChanged += OnViewModelPropertyChanged;
        }

        #region Methods

        private bool CanSaveQuestion()
        {
            if (CurrentQuiz != null &&
                !string.IsNullOrWhiteSpace(CurrentQuestion.Statement) &&
                !string.IsNullOrWhiteSpace(Answer1) &&
                !string.IsNullOrWhiteSpace(Answer2) &&
                !string.IsNullOrWhiteSpace(Answer3))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private async Task SaveQuestionAsync()
        {
            _quizManager.AllQuizzes = AvailableQuizzes;
            await _quizManager.SaveToFileAsync();
            MessageBox.Show("Your question was successfully updated.");
            _navigationManager.CurrentViewModel = new EditQuizViewModel(_navigationManager, _quizManager);
        }

        private async Task DeleteQuestionAsync()
        {
            int questionToRemoveInt = CurrentQuiz.Questions.ToList().IndexOf(CurrentQuestion);
            CurrentQuiz.RemoveQuestion(questionToRemoveInt);
            MessageBox.Show("Your question was removed.");
            _quizManager.AllQuizzes = AvailableQuizzes;
            await _quizManager.SaveToFileAsync();
            _navigationManager.CurrentViewModel = new EditQuizViewModel(_navigationManager, _quizManager);
        }

        private bool CanAddQuestion()
        {
            if (CurrentQuiz != null &&
                !string.IsNullOrWhiteSpace(_newQuestionStatement) &&
                !string.IsNullOrWhiteSpace(_newAnswer1) &&
                !string.IsNullOrWhiteSpace(_newAnswer2) &&
                !string.IsNullOrWhiteSpace(_newAnswer3))
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
            CurrentQuiz.AddQuestion(_newQuestionStatement, _correctNewAnswer, _newAnswer1, _newAnswer2, _newAnswer3);
            MessageBox.Show("Question successfully added!");

            NewQuestionStatement = "";
            CorrectNewAnswer = 0;
            NewAnswer1 = "";
            NewAnswer2 = "";
            NewAnswer3 = "";
            NewAnswers[0] = "";
            NewAnswers[1] = "";
            NewAnswers[2] = "";
        }

        private bool CanSaveToFile()
        {
            if (CurrentQuiz != null && CurrentQuiz.Questions.Count != 0)
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
            _quizManager.AllQuizzes = AvailableQuizzes;
            await _quizManager.SaveToFileAsync();
            MessageBox.Show("Your quiz was successfully updated.");
            GoToStart();
        }

        private void GoToStart()
        {
            _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveQuizCommand.NotifyCanExecuteChanged();
            SaveQuestionCommand.NotifyCanExecuteChanged();
            AddQuestionCommand.NotifyCanExecuteChanged();
            DeleteQuestionCommand.NotifyCanExecuteChanged();
        }

        #endregion


    }
}

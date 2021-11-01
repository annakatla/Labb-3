using Labb_3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Labb_3.Managers
{
    public class QuizManager
    {
        private readonly FileManager _fileManager;

        private Quiz _quiz;
        public Quiz Quiz
        {
            get { return _quiz; }
            set { _quiz = value; }
        }

        private ObservableCollection<Quiz> _allQuizzes;

        public ObservableCollection<Quiz> AllQuizzes
        {
            get { return _allQuizzes; }
            set { _allQuizzes = value; }
        }

        public QuizManager (FileManager fileManager)
        {
            _fileManager = fileManager;
            _allQuizzes = new ObservableCollection<Quiz>();
        }
    }
}

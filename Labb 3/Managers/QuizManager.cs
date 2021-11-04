using Labb_3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Labb_3.Managers
{
    public class QuizManager
    {

        private ObservableCollection<Quiz> _allQuizzes;
        public ObservableCollection<Quiz> AllQuizzes
        {
            get { return _allQuizzes; }
            set { _allQuizzes = value; }
        }

        private readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        private readonly string _fileName = "Quizeption.json";

        public QuizManager ()
        {
            _allQuizzes = new ObservableCollection<Quiz>();
        }

        public async Task SaveToFileAsync()
        {
            using FileStream createStream = File.Create(Path.Combine(_path, _fileName));
            JsonSerializerOptions options = new() { WriteIndented = true };
            await JsonSerializer.SerializeAsync(createStream, AllQuizzes, options);
            await createStream.DisposeAsync();
        }

        public async Task<List<Quiz>> LoadQuizAsync()
        {
            try
            {
                using FileStream loadStream = File.OpenRead(Path.Combine(_path, _fileName));
                var quizzes = await JsonSerializer.DeserializeAsync<List<Quiz>>(loadStream);
                return quizzes;
            }
            catch
            {
                var defaultQuiz = new Quiz("Jim Carrey Quiz");
                defaultQuiz.AddQuestion("Vilken är Jim Carreys debutfilm?", 2, "Ace Ventura", "Peggy Sue gifte sig",
                    "Copper Mountain");
                defaultQuiz.AddQuestion("I Vilken film ser vi Jim Carrey som komikern Andy Kaufman?", 0, "Man on the moon", "Kidding", "Liar Liar");
                defaultQuiz.AddQuestion("Vilket år är Jim Carrey född?", 1, "1958", "1962", "1965");
                var quizzes = new List<Quiz>();
                quizzes.Add(defaultQuiz);
                return quizzes;
            }
        }
    }
}

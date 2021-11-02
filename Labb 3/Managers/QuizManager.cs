using Labb_3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            using FileStream loadStream = File.OpenRead(Path.Combine(_path, _fileName));
            var quizzes = await JsonSerializer.DeserializeAsync<List<Quiz>>(loadStream);
            return quizzes;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Text.Json.Serialization;
using Labb_3.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Labb_3.Managers
{
    public class FileManager
    {
        private QuizManager _quizManager;

        private string _path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        private string _fileName = "Quizeption.json";


        public FileManager(QuizManager quizManager)
        {
            _quizManager = quizManager;
        }

        public async Task SaveToFileAsync()
        {
            using FileStream createStream = File.Create(Path.Combine(_path, _fileName));
            JsonSerializerOptions options = new() { WriteIndented = true };
            await JsonSerializer.SerializeAsync(createStream, _quizManager.AllQuizzes, options);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb_3.Models
{
    public class Question
    {
        public string Statement { get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswer { get; }

        public Question(string statement, int correctAnswer, params string[] answers)
        {
            Statement = statement;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }

    }
}

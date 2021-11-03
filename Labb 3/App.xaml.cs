 using System;
using System.Collections.Generic;
 using System.Collections.ObjectModel;
 using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Labb_3.Models;
using Labb_3.Managers;
using Labb_3.ViewModels;
using Labb_3.Views;

namespace Labb_3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationManager _navigationManager;
        private readonly QuizManager _quizManager;

        public App()
        {
            _navigationManager = new NavigationManager();
            _quizManager = new QuizManager();

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationManager)
            };

            MainWindow.Show();
        }
    }
}

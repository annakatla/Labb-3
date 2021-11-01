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
        //Implementera en NaviagionsManager och skapa ett objekt..
        private readonly NavigationManager _navigationManager;
        private readonly QuizManager _quizManager;
        private readonly FileManager _fileManager;

        public App()
        {
            _navigationManager = new NavigationManager();
            _quizManager = new QuizManager(_fileManager);
            _fileManager = new FileManager(_quizManager);
            
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Sätt den första vymodellen till en StartViewModel. 
            _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager, _fileManager);

            //Skapa ditt huvudfönster, för att sedan kunna byta vyer(Usercontrols) och vymodeller på det.
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationManager, _quizManager)
            };

            MainWindow.Show();
        }
    }
}

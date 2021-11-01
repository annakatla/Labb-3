using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb_3.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Labb_3.Managers
{
    public class NavigationManager
    {
        public event Action CurrentViewModelChanged;

        private ObservableObject _currentViewModel;
        public ObservableObject CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                //Metod för att kontrollera och utföra eventuella ändringar på vilken den aktuella ViewModellen är.
                OnCurrentViewModelChanged();
            }
        }

        /// <summary>
        /// Metod för att se ifall ViewModel har ändrats. Använd isåfall den Action som implementerats i klassen.
        /// </summary>
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}

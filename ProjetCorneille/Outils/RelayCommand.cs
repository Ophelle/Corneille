using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjetCorneille.Outils
{
    public class RelayCommand : ICommand
    {
        private Action<object> executeAction;

        /// <summary>
        /// Le statut d'exécution.
        /// </summary>
        private Func<bool> canExecute;

        /// <summary>
        /// Commande d'exécution.
        /// </summary>
        /// <param name="executeAction">Délégué qui encapsule la méthode exécutée.</param>
        public RelayCommand(Action<object> executeAction) : this(executeAction, null) { }

        /// <summary>
        /// Commande d'exécution.
        /// </summary>
        /// <param name="executeAction">Délégué qui encapsule la méthode exécutée.</param>
        /// <param name="canExecute">Le statut d'exécution.</param>
        public RelayCommand(Action<object> executeAction, Func<bool> canExecute)
        {
            if (executeAction == null) throw new ArgumentNullException("Aucune action fournie.");

            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Statut d'exécution.
        /// </summary>
        /// <param name="parameter">Le paramètre d'exécution.</param>
        /// <returns>Le statut d'exécution.</returns>
        public bool CanExecute(object parameter)
        {
            return (canExecute == null) ? true : canExecute.Invoke();
        }

        /// <summary>
        /// Méthode d'exécution.
        /// </summary>
        /// <param name="parameter">Paramètre d'exécution.</param>
        public void Execute(object parameter)
        {
            this.executeAction.Invoke(parameter);
        }

        /// <summary>
        /// Évènement de changement de statut d'exécution.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

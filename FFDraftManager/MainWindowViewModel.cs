using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Ink;
using FFDraftManager.Models;
using FFDraftManager.Services;

namespace FFDraftManager {
    /// <summary>
    /// The Main Window View Model
    /// </summary>
    public class MainWindowViewModel {

        #region Private Data Members

        private Window window;
        private bool draftInProgress;
        private bool draftInitialized;
        private bool isExpanded;
        private ICommand startNewDraftCommand;
        private ICommand undoLastDraftPickCommand;
        
        #endregion

        #region Properties

        private PlayersService Players {
            get { return PlayersService.Instance; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draft in progress].
        /// </summary>
        public bool DraftInProgress {
            get { return draftInProgress; }
            set {
                if (draftInProgress != value) {
                    draftInProgress = value;
                    RaisePropertyChanged("DraftInProgress");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [draft initialized].
        /// </summary>
        public bool DraftInitialized {
            get { return draftInitialized; }
            set {
                if (draftInitialized != value) {
                    draftInitialized = value;
                    RaisePropertyChanged("DraftInitialized");
                }
            }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    RaisePropertyChanged("IsExpanded");
                }
            }
        }

        #endregion

        #region Constructors

        public MainWindowViewModel(Window window) {
            this.window = window;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the start new draft command.
        /// </summary>
        public ICommand StartNewDraftCommand {
            get {
                return startNewDraftCommand ?? (startNewDraftCommand = new DelegateCommand(StartNewDraftCommandExecuted));
            }
        }

        private void StartNewDraftCommandExecuted(object sender) {
            Dialogs.NewDraftDialog newDraftDialog = new Dialogs.NewDraftDialog();
            newDraftDialog.ShowDialog();
            DraftInitialized = true;
            Players.SetPlayerList();
            Players.SetAvailablePlayers();
            PlayersService.Instance.SelectedPlayer = PlayersService.Instance.AvailablePlayers.FirstOrDefault();
            DraftInProgress = true;
        }

        public ICommand UndoLastDraftPickCommand {
            get {
                return undoLastDraftPickCommand ?? (undoLastDraftPickCommand = new DelegateCommand(UndoLastDraftPickCommandExecuted));
            }
        }

        private void UndoLastDraftPickCommandExecuted(object sender) {
            int currentTeamIndex = DraftStatusService.Instance.TeamOnClock.DraftOrder - 1;
            int previousTeamIndex;
            if (DraftStatusService.Instance.CurrentRound % 2 == 1) {
                if (currentTeamIndex == 0) {
                    previousTeamIndex = 0;
                    DraftStatusService.Instance.Rounds.RemoveAt(DraftStatusService.Instance.Rounds.Count() - 1);
                }
                else {
                    previousTeamIndex = currentTeamIndex - 1;
                }
            }
            else {
                if (currentTeamIndex == DraftSettingsService.Instance.NumberOfTeams - 1) {
                    previousTeamIndex = DraftSettingsService.Instance.NumberOfTeams - 1;
                    DraftStatusService.Instance.Rounds.RemoveAt(DraftStatusService.Instance.Rounds.Count() - 1);
                }
                else {
                    previousTeamIndex = currentTeamIndex + 1;
                }
            }

            var previousTeam = FantasyTeamService.Instance.Teams.ElementAt(previousTeamIndex);
            var previousPlayer = previousTeam.Players.ElementAt(previousTeam.Players.Count() - 1);
            previousTeam.Players.RemoveAt(previousTeam.Players.Count() - 1);
            AddPlayerToPlayerLists(previousPlayer);

            DraftStatusService.Instance.TeamOnClock = FantasyTeamService.Instance.Teams[previousTeamIndex];
            DraftStatusService.Instance.RaisePropertyChanged("CurrentOverallPick");
            DraftStatusService.Instance.RaisePropertyChanged("CurrentPick");
            DraftStatusService.Instance.RaisePropertyChanged("CurrentRound");
        }

        private void ExpandOrCollapseTabs(object sender)
        {
            IsExpanded = !isExpanded;
            RaisePropertyChanged("IsExpanded");
        }

        #endregion

        #region Methods

        private void AddPlayerToPlayerLists(Player player) {
            if (player != null) {
                switch (player.Position) {
                    case PositionType.QB:
                        Players.AvailableQbs.Insert(0, player);
                        break;
                    case PositionType.RB:
                        Players.AvailableRbs.Insert(0, player);
                        Players.AvailableWrRbs.Insert(0, player);
                        Players.AvailableWrRbTes.Insert(0, player);
                        break;
                    case PositionType.WR:
                        Players.AvailableWrs.Insert(0, player);
                        Players.AvailableWrRbs.Insert(0, player);
                        Players.AvailableWrRbTes.Insert(0, player);
                        break;
                    case PositionType.TE:
                        Players.AvailableTes.Insert(0, player);
                        Players.AvailableWrRbs.Insert(0, player);
                        Players.AvailableWrRbTes.Insert(0, player);
                        break;
                    case PositionType.PK:
                        Players.AvailablePks.Insert(0, player);
                        break;
                    case PositionType.DST:
                        Players.AvailableDefs.Insert(0, player);
                        break;
                    default:
                        break;
                }
                Players.AvailablePlayers.Insert(0, player);
            }
        }

        private void UpdateAvailablePlayers() {

        }

        #endregion
        
        #region PropertyChangedHelper

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged(string property) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                var e = new PropertyChangedEventArgs(property);
                handler(this, e);
            }
        }

        #endregion

    }

    #region DelegateCommand Implementation

    /// <summary>
    /// From Kent Boogart's blog http://kentb.blogspot.com/2009/05/mvvm-infrastructure-activeawarecommand.html
    /// </summary>
    public class DelegateCommand : Command
	{
		private readonly Predicate<object> _canExecute;
		private readonly Action<object> _execute;

		/// <summary>
		/// Constructs an instance of <c>DelegateCommand</c>.
		/// </summary>
		/// <remarks>
		/// This constructor creates the command without a delegate for determining whether the command can execute. Therefore, the
		/// command will always be eligible for execution.
		/// </remarks>
		/// <param name="execute">
		/// The delegate to invoke when the command is executed.
		/// </param>
		public DelegateCommand(Action<object> execute)
			: this(execute, null) {
		}

		/// <summary>
		/// Constructs an instance of <c>DelegateCommand</c>.
		/// </summary>
		/// <param name="execute">
		/// The delegate to invoke when the command is executed.
		/// </param>
		/// <param name="canExecute">
		/// The delegate to invoke to determine whether the command can execute.
		/// </param>
		public DelegateCommand(Action<object> execute, Predicate<object> canExecute) { 
			_execute = execute;
			_canExecute = canExecute;
		}

		/// <summary>
		/// Determines whether this command can execute.
		/// </summary>
		/// <remarks>
		/// If there is no delegate to determine whether the command can execute, this method will return <see langword="true"/>. If a delegate was provided, this
		/// method will invoke that delegate.
		/// </remarks>
		/// <param name="parameter">
		/// The command parameter.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the command can execute, otherwise <see langword="false"/>.
		/// </returns>
		public override bool CanExecute(object parameter) {
			if (_canExecute == null)
			{
				return true;
			}

			return _canExecute(parameter);
		}

		/// <summary>
		/// Executes this command.
		/// </summary>
		/// <remarks>
		/// This method invokes the provided delegate to execute the command.
		/// </remarks>
		/// <param name="parameter">
		/// The command parameter.
		/// </param>
		public override void Execute(object parameter) {
			_execute(parameter);
		}
	}

    /// <summary>
    /// From Kent Boogart's blog http://kentb.blogspot.com/2009/05/mvvm-infrastructure-activeawarecommand.html
    /// </summary>
	public abstract class Command : ICommand
	{
		private readonly Dispatcher _dispatcher;

		/// <summary>
		/// Constructs an instance of <c>CommandBase</c>.
		/// </summary>
		protected Command() {
			if (Application.Current != null)
			{
				_dispatcher = Application.Current.Dispatcher;
			}
			else
			{
				//this is useful for unit tests where there is no application running
				_dispatcher = Dispatcher.CurrentDispatcher;
			}

			Debug.Assert(_dispatcher != null);
		}

		/// <summary>
		/// Occurs whenever the state of the application changes such that the result of a call to <see cref="CanExecute"/> may return a different value.
		/// </summary>
		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		/// <summary>
		/// Determines whether this command can execute.
		/// </summary>
		/// <param name="parameter">
		/// The command parameter.
		/// </param>
		/// <returns>
		/// <see langword="true"/> if the command can execute, otherwise <see langword="false"/>.
		/// </returns>
		public abstract bool CanExecute(object parameter);

		/// <summary>
		/// Executes this command.
		/// </summary>
		/// <param name="parameter">
		/// The command parameter.
		/// </param>
		public abstract void Execute(object parameter);

		/// <summary>
		/// Raises the <see cref="CanExecuteChanged"/> event.
		/// </summary>
		protected virtual void OnCanExecuteChanged() {
			if (!_dispatcher.CheckAccess())
			{
				_dispatcher.Invoke((ThreadStart)OnCanExecuteChanged, DispatcherPriority.Normal);
			}
			else
			{
				CommandManager.InvalidateRequerySuggested();
			}
		}
	}

    #endregion
}


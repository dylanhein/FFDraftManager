using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using FFDraftManager.Models;
using FFDraftManager.Services;

namespace FFDraftManager.Tabs {
    public class AvailablePlayersViewModel : INotifyPropertyChanged {

        #region Private Data Members

        private Player selectedPlayer;
        private Player selectedQb;
        private Player selectedRb;
        private Player selectedWr;
        private Player selectedTe;
        private Player selectedPk;
        private Player selectedDef;
        private Player selectedWrRb;
        private Player selectedWrRbTe;

        private ICommand draftPlayerCommand;

        #endregion

        #region Properties

        private PlayersService Players {
            get { return PlayersService.Instance; }
        }
        
        /// <summary>
        /// Gets or sets the selected player.
        /// </summary>
        public Player SelectedPlayer {
            get { return selectedPlayer; }
            set {
                if (selectedPlayer != value) {
                    selectedPlayer = value;
                    RaisePropertyChanged("SelectedPlayer");
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected qb.
        /// </summary>
        public Player SelectedQb {
            get { return selectedQb; }
            set {
                if (selectedQb != value) {
                    selectedQb = value;
                    SelectedPlayer = value;
                    RaisePropertyChanged("SelectedQb");
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected rb.
        /// </summary>
        public Player SelectedRb {
            get { return selectedRb; }
            set {
                if (selectedRb != value) {
                    selectedRb = value;
                    SelectedPlayer = value;
                    RaisePropertyChanged("SelectedRb");
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected wr.
        /// </summary>
        public Player SelectedWr {
            get { return selectedWr; }
            set {
                if (selectedWr != value) {
                    selectedWr = value;
                    SelectedPlayer = value;
                    RaisePropertyChanged("SelectedWr");
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected te.
        /// </summary>
        public Player SelectedTe {
            get { return selectedTe; }
            set {
                if (selectedTe != value) {
                    selectedTe = value;
                    SelectedPlayer = value;
                    RaisePropertyChanged("SelectedTe");
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected pk.
        /// </summary>
        public Player SelectedPk {
            get { return selectedPk; }
            set {
                if (selectedPk != value) {
                    selectedPk = value;
                    SelectedPlayer = value;
                    RaisePropertyChanged("SelectedPk");
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected def.
        /// </summary>
        public Player SelectedDef {
            get { return selectedDef; }
            set {
                if (selectedDef != value) {
                    selectedDef = value;
                    SelectedPlayer = value;
                    RaisePropertyChanged("SelectedDef");
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected wr rb.
        /// </summary>
        public Player SelectedWrRb {
            get { return selectedWrRb; }
            set {
                if (selectedWrRb != value) {
                    selectedWrRb = value;
                    SelectedPlayer = value;
                    RaisePropertyChanged("SelectedWrRb");
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected wr rb te.
        /// </summary>
        public Player SelectedWrRbTe {
            get { return selectedWrRbTe; }
            set {
                if (selectedWrRbTe != value) {
                    selectedWrRbTe = value;
                    SelectedPlayer = value;
                    RaisePropertyChanged("SelectedWrRbTe");
                }
            }
        }

        #endregion

        #region Constructors

        public AvailablePlayersViewModel() {
            Initialize();
        }

        #endregion

        #region Commands

        public ICommand DraftPlayerCommand {
            get {
                return draftPlayerCommand ?? (draftPlayerCommand = new DelegateCommand(DraftPlayerCommandExecuted));
            }
        }

        private void DraftPlayerCommandExecuted(object sender) {
            if (SelectedPlayer != null) {
                AssignPick(SelectedPlayer);
                RemoveSelectedPlayerFromPositionList();
                UpdateTeamOnClock();
                SelectedPlayer = Players.AvailablePlayers.FirstOrDefault();
            }
        }

        #endregion

        #region Methods

        private void Initialize() {
            //SelectedPlayer = Players.AvailablePlayers.FirstOrDefault();
        }

        private void UpdateTeamOnClock() {
            int currentTeamIndex = FantasyTeamService.Instance.Teams.IndexOf(DraftStatusService.Instance.TeamOnClock);
            int nextTeamIndex = currentTeamIndex >= DraftSettingsService.Instance.NumberOfTeams - 1 ? 0 : currentTeamIndex + 1;
            DraftStatusService.Instance.TeamOnClock = FantasyTeamService.Instance.Teams[nextTeamIndex];
        }

        private void AssignPick(Player player) {
            if (player != null) {
                FantasyTeamService.Instance.AddPlayer(DraftStatusService.Instance.TeamOnClock, player);
            }
        }

        private void RemoveSelectedPlayerFromPositionList() {
            if (SelectedPlayer != null) {
                var player = selectedPlayer;
                switch (SelectedPlayer.Position) {
                    case PositionType.QB:
                        Players.AvailableQbs.Remove(Players.AvailableQbs.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        break;
                    case PositionType.RB:
                        Players.AvailableRbs.Remove(Players.AvailableRbs.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        Players.AvailableWrRbs.Remove(Players.AvailableWrRbs.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        Players.AvailableWrRbTes.Remove(Players.AvailableWrRbTes.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        break;
                    case PositionType.WR:
                        Players.AvailableWrs.Remove(Players.AvailableWrs.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        Players.AvailableWrRbs.Remove(Players.AvailableWrRbs.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        Players.AvailableWrRbTes.Remove(Players.AvailableWrRbTes.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        break;
                    case PositionType.TE:
                        Players.AvailableTes.Remove(Players.AvailableTes.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        Players.AvailableWrRbs.Remove(Players.AvailableWrRbs.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        Players.AvailableWrRbTes.Remove(Players.AvailableWrRbTes.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        break;
                    case PositionType.PK:
                        Players.AvailablePks.Remove(Players.AvailablePks.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        break;
                    case PositionType.DST:
                        Players.AvailableDefs.Remove(Players.AvailableDefs.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
                        break;
                    default:
                        break;
                }
                Players.AvailablePlayers.Remove(Players.AvailablePlayers.FirstOrDefault(rb => rb.Name == player.Name && rb.PositionRank == player.PositionRank));
            }
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
}

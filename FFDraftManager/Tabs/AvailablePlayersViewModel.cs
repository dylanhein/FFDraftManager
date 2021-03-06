﻿using System;
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
            get { return PlayersService.Instance.SelectedPlayer ?? (PlayersService.Instance.SelectedPlayer = PlayersService.Instance.AvailablePlayers.FirstOrDefault()); }
            set {
                if (PlayersService.Instance.SelectedPlayer != value) {
                    PlayersService.Instance.SelectedPlayer = value;
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

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize() {
            SelectedPlayer = Players.AvailablePlayers.FirstOrDefault();
        }

        /// <summary>
        /// Updates the team on clock.
        /// </summary>
        private void UpdateTeamOnClock() {
            int currentTeamIndex = FantasyTeamService.Instance.Teams.IndexOf(DraftStatusService.Instance.TeamOnClock);
            int nextTeamIndex = GetNextTeamIndex(currentTeamIndex);
            DraftStatusService.Instance.TeamOnClock = FantasyTeamService.Instance.Teams[nextTeamIndex];
            DraftStatusService.Instance.RaisePropertyChanged("CurrentOverallPick");
            DraftStatusService.Instance.RaisePropertyChanged("CurrentPick");
            DraftStatusService.Instance.RaisePropertyChanged("CurrentRound");
        }

        /// <summary>
        /// Gets the index of the next team.
        /// </summary>
        /// <param name="currentTeamIndex">Index of the current team.</param>
        /// <returns></returns>
        private int GetNextTeamIndex(int currentTeamIndex) {
            if (DraftStatusService.Instance.CurrentRound % 2 == 1) {
                if (currentTeamIndex == DraftSettingsService.Instance.NumberOfTeams - 1) {
                    DraftStatusService.Instance.AddRound();
                    return DraftSettingsService.Instance.NumberOfTeams - 1;
                }
                return currentTeamIndex + 1;
            }
            else {
                if (currentTeamIndex == 0) {
                    DraftStatusService.Instance.AddRound();
                    return 0;
                }
                return currentTeamIndex - 1;
            }
        }

        /// <summary>
        /// Assigns the pick.
        /// </summary>
        /// <param name="player">The player.</param>
        private void AssignPick(Player player) {
            if (player != null) {
                FantasyTeamService.Instance.AddPlayer(DraftStatusService.Instance.TeamOnClock, player);
                DraftStatusService.Instance.TeamOnClock.RaisePropertyChanged("QBCount");
                DraftStatusService.Instance.TeamOnClock.RaisePropertyChanged("RBCount");
                DraftStatusService.Instance.TeamOnClock.RaisePropertyChanged("WRCount");
                DraftStatusService.Instance.TeamOnClock.RaisePropertyChanged("TECount");
                DraftStatusService.Instance.TeamOnClock.RaisePropertyChanged("DSTCount");
                DraftStatusService.Instance.TeamOnClock.RaisePropertyChanged("PKCount");
                FantasyTeamService.Instance.RaisePropertyChanged(DraftStatusService.Instance.TeamOnClock.TeamTitle);
                DraftStatusService.Instance.TeamOnClock.RaisePropertyChanged("AdpPar");
                DraftStatusService.Instance.TeamOnClock.RaisePropertyChanged("AdpSum");
                while (DraftStatusService.Instance.AdpChartMax <= DraftStatusService.Instance.TeamOnClock.AdpSum) {
                    DraftStatusService.Instance.AdpChartMax += 100;
                }
            }
        }

        /// <summary>
        /// Removes the selected player from position list.
        /// </summary>
        private void RemoveSelectedPlayerFromPositionList() {
            if (SelectedPlayer != null) {
                var player = SelectedPlayer;
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

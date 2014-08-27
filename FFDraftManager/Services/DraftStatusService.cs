using FFDraftManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FFDraftManager.Services {
    public sealed class DraftStatusService : INotifyPropertyChanged {

        #region Private Data Members

        private static volatile DraftStatusService instance;
        private static object syncRoot = new Object();

        private int currentOverallPick;
        private int secondsOnClock;
        private FantasyTeam teamOnClock;
        private ObservableCollection<Round> rounds;

        #endregion

        #region Construction

        private DraftStatusService() { }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static DraftStatusService Instance {
            get {
                if (instance == null) {
                    lock (syncRoot) {
                        if (instance == null)
                            instance = new DraftStatusService();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current overall pick.
        /// </summary>
        public int CurrentOverallPick {
            get { return currentOverallPick; }
            set {
                if (currentOverallPick != value) {
                    currentOverallPick = value;
                    RaisePropertyChanged("CurrentOverallPick");
                }
            }
        }

        /// <summary>
        /// Gets the current round.
        /// </summary>
        public int CurrentRound {
            get { return Rounds.Count + 1; }
        }

        /// <summary>
        /// Gets or sets the current pick.
        /// </summary>
        public int CurrentPick {
            get { return TeamOnClock != null ? FantasyTeamService.Instance.Teams.IndexOf(TeamOnClock) + 1 : 0; }
        }

        /// <summary>
        /// Gets or sets the seconds on clock.
        /// </summary>
        public int SecondsOnClock {
            get { return secondsOnClock; }
            set {
                if (secondsOnClock != value) {
                    secondsOnClock = value;
                    RaisePropertyChanged("SecondsOnClock");
                }
            }
        }

        public FantasyTeam TeamOnClock {
            get { return teamOnClock; }
            set {
                if (teamOnClock != value) {
                    teamOnClock = value;
                    RaisePropertyChanged("TeamOnClock");
                }
            }
        }

        /// <summary>
        /// Gets or sets the rounds.
        /// </summary>
        public ObservableCollection<Round> Rounds {
            get { return rounds ?? (rounds = new ObservableCollection<Round>()); }
            set {
                if (rounds != value) {
                    rounds = value;
                    RaisePropertyChanged("Rounds");
                }
            }
        }

        #endregion

        #region Methods

        public void AddRound(Round round) {
            Rounds.Add(round);
            RaisePropertyChanged("Rounds");
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
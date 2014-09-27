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

        private bool draftInProgress;
        private int secondsOnClock;
        private int adpChartMax;
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
            get {
                return (CurrentRound - 1) * DraftSettingsService.Instance.NumberOfTeams + CurrentPick;
            }
        }

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
        /// Gets the current round.
        /// </summary>
        public int CurrentRound {
            get { return Rounds.Count; }
        }

        /// <summary>
        /// Gets or sets the current pick.
        /// </summary>
        public int CurrentPick {
            get {
                if (CurrentRound % 2 == 1) {
                    if (TeamOnClock.DraftOrder > 1) {
                        return TeamOnClock.DraftOrder;
                    }
                    return 1;
                }
                else {
                    if (TeamOnClock.DraftOrder < 12) {
                        return (DraftSettingsService.Instance.NumberOfTeams - TeamOnClock.DraftOrder + 1);
                    }
                    return 1;
                }; 
            }
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

        /// <summary>
        /// Gets or sets the adp chart max.
        /// </summary>
        public int AdpChartMax {
            get { return adpChartMax; }
            set {
                if (adpChartMax != value) {
                    adpChartMax = value;
                    RaisePropertyChanged("AdpChartMax");
                }
            }
        }

        public FantasyTeam TeamOnClock {
            get { return teamOnClock ?? ( teamOnClock = new FantasyTeam() ); }
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

        public void AddRound() {
            var round = new Round { RoundNumber = Rounds.Count + 1 }; 
            Rounds.Add(round);
            RaisePropertyChanged("Rounds");
            RaisePropertyChanged("CurrentRound");
            RaisePropertyChanged("CurrentPick");
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
using System;
using System.ComponentModel;

namespace FFDraftManager.Services {
    public sealed class DraftStatusService {

        #region Private Data Members

        private static volatile DraftStatusService instance;
        private static object syncRoot = new Object();

        private int currentOverallPick;
        private int currentRound;
        private int currentPick;
        private int secondsOnClock;

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
        /// Gets or sets the current round.
        /// </summary>
        public int CurrentRound {
            get { return currentRound; }
            set {
                if (currentRound != value) {
                    currentRound = value;
                    RaisePropertyChanged("CurrentRound");
                }
            }
        }

        /// <summary>
        /// Gets or sets the current pick.
        /// </summary>
        public int CurrentPick {
            get { return currentPick; }
            set {
                if (currentPick != value) {
                    currentPick = value;
                    RaisePropertyChanged("CurrentPick");
                }
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
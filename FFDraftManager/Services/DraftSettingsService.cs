using FFDraftManager.Constants;
using FFDraftManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FFDraftManager.Services {
    public sealed class DraftSettingsService : INotifyPropertyChanged {

        #region Private Data Members

        private int numberOfTeams;
        private int secondsPerPick;
        private int numberOfQbs;
        private int numberOfRbs;
        private int numberOfWrs;
        private int numberOfTes;
        private int numberOfPks;
        private int numberOfDefs;
        private int numberOfWrRbs;
        private int numberOfWrRbTes;
        private int numberOfQbWrRbTes;
        private int numberOfBenchPlayers;
        private bool picksTimed;
        private bool isPPR;
        private List<string> allPositions;
        
        private static volatile DraftSettingsService instance;
        private static object syncRoot = new Object();

        #endregion

        #region Construction

        private DraftSettingsService() { 
            numberOfTeams = DraftDefaults.NumberOfTeams;
            secondsPerPick = DraftDefaults.SecondsPerPick;
            numberOfQbs = DraftDefaults.NumberOfQbs;
            numberOfRbs = DraftDefaults.NumberOfRbs;
            numberOfWrs = DraftDefaults.NumberOfWrs;
            numberOfTes = DraftDefaults.NumberOfTes;
            numberOfPks = DraftDefaults.NumberOfPks;
            numberOfDefs = DraftDefaults.NumberOfDefs;
            numberOfWrRbs = DraftDefaults.NumberOfWrRbs;
            numberOfWrRbTes = DraftDefaults.NumberOfWrRbTes;
            numberOfQbWrRbTes = DraftDefaults.NumberOfQbWrRbTes;
            picksTimed = DraftDefaults.PicksTimed;
            isPPR = DraftDefaults.IsPPR;
            numberOfBenchPlayers = DraftDefaults.NumberOfBenchPlayers;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static DraftSettingsService Instance {
            get {
                if (instance == null) {
                    lock (syncRoot) {
                        if (instance == null)
                            instance = new DraftSettingsService();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets or sets the number of teams.
        /// </summary>
        public int NumberOfTeams {
            get { return numberOfTeams; }
            set {
                if (numberOfTeams != value) {
                    numberOfTeams = value;
                    RaisePropertyChanged("NumberOfTeams");
                }
            }
        }

        /// <summary>
        /// Gets or sets the seconds per pick.
        /// </summary>
        public int SecondsPerPick {
            get { return secondsPerPick; }
            set {
                if (secondsPerPick != value) {
                    secondsPerPick = value;
                    RaisePropertyChanged("SecondsPerPick");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of QBS.
        /// </summary>
        public int NumberOfQbs {
            get { return numberOfQbs; }
            set {
                if (numberOfQbs != value) {
                    numberOfQbs = value;
                    RaisePropertyChanged("NumberOfQbs");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of RBS.
        /// </summary>
        public int NumberOfRbs {
            get { return numberOfRbs; }
            set {
                if (numberOfRbs != value) {
                    numberOfRbs = value;
                    RaisePropertyChanged("NumberOfRbs");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of WRS.
        /// </summary>
        public int NumberOfWrs {
            get { return numberOfWrs; }
            set {
                if (numberOfWrs != value) {
                    numberOfWrs = value;
                    RaisePropertyChanged("NumberOfWrs");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of tes.
        /// </summary>
        public int NumberOfTes {
            get { return numberOfTes; }
            set {
                if (numberOfTes != value) {
                    numberOfTes = value;
                    RaisePropertyChanged("NumberOfTes");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of PKS.
        /// </summary>
        public int NumberOfPks {
            get { return numberOfPks; }
            set {
                if (numberOfPks != value) {
                    numberOfPks = value;
                    RaisePropertyChanged("NumberOfPks");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of defs.
        /// </summary>
        public int NumberOfDefs {
            get { return numberOfDefs; }
            set {
                if (numberOfDefs != value) {
                    numberOfDefs = value;
                    RaisePropertyChanged("NumberOfDefs");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of wr RBS.
        /// </summary>
        public int NumberOfWrRbs {
            get { return numberOfWrRbs; }
            set {
                if (numberOfWrRbs != value) {
                    numberOfWrRbs = value;
                    RaisePropertyChanged("NumberOfWrRbs");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of wr rb tes.
        /// </summary>
        public int NumberOfWrRbTes {
            get { return numberOfWrRbTes; }
            set {
                if (numberOfWrRbTes != value) {
                    numberOfWrRbTes = value;
                    RaisePropertyChanged("NumberOfWrRbTes");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of wr rb tes.
        /// </summary>
        public int NumberOfQbWrRbTes {
            get { return numberOfQbWrRbTes; }
            set {
                if (numberOfQbWrRbTes != value) {
                    numberOfQbWrRbTes = value;
                    RaisePropertyChanged("NumberOfQbWrRbTes");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of bench players.
        /// </summary>
        public int NumberOfBenchPlayers {
            get { return numberOfBenchPlayers; }
            set {
                if (numberOfBenchPlayers != value) {
                    numberOfBenchPlayers = value;
                    RaisePropertyChanged("NumberOfBenchPlayers");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [picks timed].
        /// </summary>
        public bool PicksTimed {
            get { return picksTimed; }
            set {
                if (picksTimed != value) {
                    picksTimed = value;
                    RaisePropertyChanged("PicksTimed");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is PPR.
        /// </summary>
        public bool IsPPR {
            get { return isPPR; }
            set {
                if (isPPR != value) {
                    isPPR = value;
                    RaisePropertyChanged("IsPPR");
                }
            }
        }

        /// <summary>
        /// Gets or sets all positions.
        /// </summary>
        public List<string> AllPositions {
            get { return allPositions ?? (allPositions = new List<String>()); }
            set {
                if (allPositions != value) {
                    allPositions = value;
                    RaisePropertyChanged("AllPositions");
                }
            }
        }

        public int TeamSize {
            get {
                return NumberOfQbs
                    + NumberOfRbs
                    + NumberOfWrs
                    + NumberOfTes
                    + NumberOfWrRbs
                    + NumberOfWrRbTes
                    + NumberOfQbWrRbTes
                    + NumberOfDefs
                    + NumberOfPks
                    + NumberOfBenchPlayers;
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
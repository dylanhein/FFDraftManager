using FFDraftManager.Models;
using FFDraftManager.Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FFDraftManager.Dialogs {
    /// <summary>
    /// Controller View Model for the New Draft Dialog window.
    /// </summary>
    public class NewDraftDialogController : INotifyPropertyChanged {

        #region Private Data Members

        private Window window;

        private string team1Name;
        private string team2Name;
        private string team3Name;
        private string team4Name;
        private string team5Name;
        private string team6Name;
        private string team7Name;
        private string team8Name;
        private string team9Name;
        private string team10Name;
        private string team11Name;
        private string team12Name;

        private bool isTeam1User;
        private bool isTeam2User;
        private bool isTeam3User;
        private bool isTeam4User;
        private bool isTeam5User;
        private bool isTeam6User;
        private bool isTeam7User;
        private bool isTeam8User;
        private bool isTeam9User;
        private bool isTeam10User;
        private bool isTeam11User;
        private bool isTeam12User;

        private ICommand initializeDraftCommand;

        #endregion

        #region Construction

        public NewDraftDialogController(Window window) {
            this.window = window;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the draft settings.
        /// </summary>
        private DraftSettingsService DraftSettings {
            get { return DraftSettingsService.Instance; }
        }

        /// <summary>
        /// Gets or sets the draft settings.
        /// </summary>
        private FantasyTeamService FantasyTeams {
            get { return FantasyTeamService.Instance; }
        }

        /// <summary>
        /// Gets or sets the name of the team1.
        /// </summary>
        public string Team1Name {
            get { return team1Name; }
            set {
                if (team1Name != value) {
                    team1Name = value;
                    RaisePropertyChanged("Team1Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team2.
        /// </summary>
        public string Team2Name {
            get { return team2Name; }
            set {
                if (team2Name != value) {
                    team2Name = value;
                    RaisePropertyChanged("Team2Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team3.
        /// </summary>
        public string Team3Name {
            get { return team3Name; }
            set {
                if (team3Name != value) {
                    team3Name = value;
                    RaisePropertyChanged("Team3Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team4.
        /// </summary>
        public string Team4Name {
            get { return team4Name; }
            set {
                if (team4Name != value) {
                    team4Name = value;
                    RaisePropertyChanged("Team4Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team5.
        /// </summary>
        public string Team5Name {
            get { return team5Name; }
            set {
                if (team5Name != value) {
                    team5Name = value;
                    RaisePropertyChanged("Team5Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team6.
        /// </summary>
        public string Team6Name {
            get { return team6Name; }
            set {
                if (team6Name != value) {
                    team6Name = value;
                    RaisePropertyChanged("Team6Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team7.
        /// </summary>
        public string Team7Name {
            get { return team7Name; }
            set {
                if (team7Name != value) {
                    team7Name = value;
                    RaisePropertyChanged("Team7Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team8.
        /// </summary>
        public string Team8Name {
            get { return team8Name; }
            set {
                if (team8Name != value) {
                    team8Name = value;
                    RaisePropertyChanged("Team8Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team9.
        /// </summary>
        public string Team9Name {
            get { return team9Name; }
            set {
                if (team9Name != value) {
                    team9Name = value;
                    RaisePropertyChanged("Team9Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team10.
        /// </summary>
        public string Team10Name {
            get { return team10Name; }
            set {
                if (team10Name != value) {
                    team10Name = value;
                    RaisePropertyChanged("Team10Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team11.
        /// </summary>
        public string Team11Name {
            get { return team11Name; }
            set {
                if (team11Name != value) {
                    team11Name = value;
                    RaisePropertyChanged("Team11Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team12.
        /// </summary>
        public string Team12Name {
            get { return team12Name; }
            set {
                if (team12Name != value) {
                    team12Name = value;
                    RaisePropertyChanged("Team12Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team1 user.
        /// </summary>
        public bool IsTeam1User {
            get { return isTeam1User; }
            set {
                if (isTeam1User != value) {
                    isTeam1User = value;
                    RaisePropertyChanged("IsTeam1User");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team2 user.
        /// </summary>
        public bool IsTeam2User {
            get { return isTeam2User; }
            set {
                if (isTeam2User != value) {
                    isTeam2User = value;
                    RaisePropertyChanged("IsTeam2User");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team3 user.
        /// </summary>
        public bool IsTeam3User {
            get { return isTeam3User; }
            set {
                if (isTeam3User != value) {
                    isTeam3User = value;
                    RaisePropertyChanged("IsTeam3User");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team4 user.
        /// </summary>
        public bool IsTeam4User {
            get { return isTeam4User; }
            set {
                if (isTeam4User != value) {
                    isTeam4User = value;
                    RaisePropertyChanged("IsTeam4User");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team5 user.
        /// </summary>
        public bool IsTeam5User {
            get { return isTeam5User; }
            set {
                if (isTeam5User != value) {
                    isTeam5User = value;
                    RaisePropertyChanged("IsTeam5User");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team6 user.
        /// </summary>
        public bool IsTeam6User {
            get { return isTeam6User; }
            set {
                if (isTeam6User != value) {
                    isTeam6User = value;
                    RaisePropertyChanged("IsTeam6User");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team7 user.
        /// </summary>
        public bool IsTeam7User {
            get { return isTeam7User; }
            set {
                if (isTeam7User != value) {
                    isTeam7User = value;
                    RaisePropertyChanged("IsTeam7User");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team8 user.
        /// </summary>
        public bool IsTeam8User {
            get { return isTeam8User; }
            set {
                if (isTeam8User != value) {
                    isTeam8User = value;
                    RaisePropertyChanged("IsTeam8User");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team9 user.
        /// </summary>
        public bool IsTeam9User {
            get { return isTeam9User; }
            set {
                if (isTeam9User != value) {
                    isTeam9User = value;
                    RaisePropertyChanged("IsTeam9User");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team10 user.
        /// </summary>
        public bool IsTeam10User {
            get { return isTeam10User; }
            set {
                if (isTeam10User != value) {
                    isTeam10User = value;
                    RaisePropertyChanged("IsTeam10User");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team11 user.
        /// </summary>
        public bool IsTeam11User {
            get { return isTeam11User; }
            set {
                if (isTeam11User != value) {
                    isTeam11User = value;
                    RaisePropertyChanged("IsTeam11User");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is team12 user.
        /// </summary>
        public bool IsTeam12User {
            get { return isTeam12User; }
            set {
                if (isTeam12User != value) {
                    isTeam12User = value;
                    RaisePropertyChanged("IsTeam12User");
                }
            }
        }

        #endregion

        #region Commands

        public ICommand InitializeDraftCommand {
            get {
                return initializeDraftCommand ?? (initializeDraftCommand = new DelegateCommand(InitializeDraftCommandExecuted));
            }
        }

        private void InitializeDraftCommandExecuted(object sender) {
            BuildFantasyTeams();
            DraftStatusService.Instance.AddRound();
            DraftStatusService.Instance.TeamOnClock = FantasyTeams.Teams[0];
            DraftStatusService.Instance.DraftInProgress = true;
            window.Close();
        }

        #endregion

        #region Methods

        private void BuildFantasyTeams() {
            AddFantasyTeam(Team1Name, IsTeam1User, 1);
            AddFantasyTeam(Team2Name, IsTeam2User, 2);
            AddFantasyTeam(Team3Name, IsTeam3User, 3);
            AddFantasyTeam(Team4Name, IsTeam4User, 4);

            if (DraftSettings.NumberOfTeams > 4) {
                AddFantasyTeam(Team5Name, IsTeam5User, 5);
                AddFantasyTeam(Team6Name, IsTeam6User, 6);

                if (DraftSettings.NumberOfTeams > 6) {
                    AddFantasyTeam(Team7Name, IsTeam7User, 7);
                    AddFantasyTeam(Team8Name, IsTeam8User, 8);

                    if (DraftSettings.NumberOfTeams > 8) {
                        AddFantasyTeam(Team9Name, IsTeam9User, 9);
                        AddFantasyTeam(Team10Name, IsTeam10User, 10);

                        if (DraftSettings.NumberOfTeams > 10) {
                            AddFantasyTeam(Team11Name, IsTeam11User, 11);
                            AddFantasyTeam(Team12Name, IsTeam12User, 12);
                        }
                    }
                }
            }
        }

        private void AddFantasyTeam(string teamName, bool isUserTeam, int draftOrder) {
            FantasyTeams.AddTeam(new FantasyTeam {
                TeamName = teamName,
                IsUserTeam = isUserTeam,
                DraftOrder = draftOrder,
                Players = new ObservableCollection<Player>()
            });
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

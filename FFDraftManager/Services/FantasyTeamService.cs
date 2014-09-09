using FFDraftManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FFDraftManager.Services {
    public sealed class FantasyTeamService : INotifyPropertyChanged {

        #region Private Data Members

        private static volatile FantasyTeamService instance;
        private static object syncRoot = new Object();
        private List<FantasyTeam> teams;

        #endregion

        #region Construction

        private FantasyTeamService() { }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static FantasyTeamService Instance {
            get {
                if (instance == null) {
                    lock (syncRoot) {
                        if (instance == null)
                            instance = new FantasyTeamService();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Properties

        public List<FantasyTeam> Teams {
            get { return teams ?? (teams = new List<FantasyTeam>()); }
            set {
                if (teams != value) {
                    teams = value;
                    RaisePropertyChanged("Teams");
                }
            }
        }

        public FantasyTeam Team1 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 1); } }
        public FantasyTeam Team2 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 2); } }
        public FantasyTeam Team3 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 3); } }
        public FantasyTeam Team4 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 4); } }
        public FantasyTeam Team5 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 5); } }
        public FantasyTeam Team6 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 6); } }
        public FantasyTeam Team7 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 7); } }
        public FantasyTeam Team8 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 8); } }
        public FantasyTeam Team9 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 9); } }
        public FantasyTeam Team10 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 10); } }
        public FantasyTeam Team11 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 11); } }
        public FantasyTeam Team12 { get { return Teams.FirstOrDefault(t => t.DraftOrder == 12); } }
        public FantasyTeam UserTeam { get { return Teams.FirstOrDefault(t => t.IsUserTeam); } }

        #endregion

        #region Methods

        public void AddTeam(FantasyTeam team) {
            Teams.Add(team);
            RaisePropertyChanged("Teams");
            RaisePropertyChanged("Team1");
            RaisePropertyChanged("Team2");
            RaisePropertyChanged("Team3");
            RaisePropertyChanged("Team4");
            RaisePropertyChanged("Team5");
            RaisePropertyChanged("Team6");
            RaisePropertyChanged("Team7");
            RaisePropertyChanged("Team8");
            RaisePropertyChanged("Team9");
            RaisePropertyChanged("Team10");
            RaisePropertyChanged("Team11");
            RaisePropertyChanged("Team12");
            RaisePropertyChanged("UserTeam");
        }
        
        public void AddPlayer(FantasyTeam team, Player player) {
            team.Players.Add(player);
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
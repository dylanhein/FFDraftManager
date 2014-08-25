using FFDraftManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FFDraftManager.Services {
    public sealed class FantasyTeamService {

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
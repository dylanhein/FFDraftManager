using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FFDraftManager.Models {
    /// <summary>
    /// A class to represent a team in the draft.
    /// </summary>
    public class FantasyTeam {

        #region Private Data Members

        private int draftOrder;
        private string teamName;
        private bool isUserTeam;
        private ObservableCollection<Player> players;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the draft order.
        /// </summary>
        public int DraftOrder {
            get { return draftOrder; }
            set {
                if (draftOrder != value) {
                    draftOrder = value;
                    RaisePropertyChanged("DraftOrder");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        public string TeamName {
            get { return teamName; }
            set {
                if (teamName != value) {
                    teamName = value;
                    RaisePropertyChanged("TeamName");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is user team.
        /// </summary>
        public bool IsUserTeam {
            get { return isUserTeam; }
            set {
                if (isUserTeam != value) {
                    isUserTeam = value;
                    RaisePropertyChanged("IsUserTeam");
                }
            }
        }

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        public ObservableCollection<Player> Players {
            get { return players ?? (players = new ObservableCollection<Player>()); }
            set {
                if (players != value) {
                    players = value;
                    RaisePropertyChanged("Players");
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

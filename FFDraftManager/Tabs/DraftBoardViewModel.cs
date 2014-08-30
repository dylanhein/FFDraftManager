using FFDraftManager.Models;
using FFDraftManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FFDraftManager.Tabs {
    public class DraftBoardViewModel {

        #region Private Data Members


        
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the draft settings.
        /// </summary>
        public DraftSettingsService DraftSettings {
            get { return DraftSettingsService.Instance; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is team5 visible.
        /// </summary>
        public bool IsTeam5Visible {
            get { return DraftSettings.NumberOfTeams > 4; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is team6 visible.
        /// </summary>
        public bool IsTeam6Visible {
            get { return IsTeam5Visible; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is team7 visible.
        /// </summary>
        public bool IsTeam7Visible {
            get { return DraftSettings.NumberOfTeams > 6; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is team8 visible.
        /// </summary>
        public bool IsTeam8Visible {
            get { return IsTeam7Visible; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is team9 visible.
        /// </summary>
        public bool IsTeam9Visible {
            get { return DraftSettings.NumberOfTeams > 8; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is team10 visible.
        /// </summary>
        public bool IsTeam10Visible {
            get { return IsTeam9Visible; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is team11 visible.
        /// </summary>
        public bool IsTeam11Visible {
            get { return DraftSettings.NumberOfTeams > 10; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is team12 visible.
        /// </summary>
        public bool IsTeam12Visible {
            get { return IsTeam11Visible; }
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

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
        /// Gets the draft status.
        /// </summary>
        public DraftStatusService DraftStatus {
            get { return DraftStatusService.Instance; }
        }

        /// <summary>
        /// Gets the players.
        /// </summary>
        public PlayersService Players {
            get { return PlayersService.Instance; }
        }

        /// <summary>
        /// Gets the teams.
        /// </summary>
        public FantasyTeamService Teams {
            get { return FantasyTeamService.Instance; }
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

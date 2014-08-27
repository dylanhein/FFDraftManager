using FFDraftManager.Services;
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
    public class Round : INotifyPropertyChanged {

        #region Private Data Members

        private int roundNumber;

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets or sets the round number.
        /// </summary>
        public int RoundNumber {
            get { return roundNumber; }
            set {
                if (roundNumber != value) {
                    roundNumber = value;
                    RaisePropertyChanged("RoundNumber");
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

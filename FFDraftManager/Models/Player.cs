using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FFDraftManager.Models {
    /// <summary>
    /// A class to represent a player in the draft.
    /// </summary>
    public class Player
    {
        #region Private Data Members

        private string name;
        private string team;
        private PositionType position;
        private int adp;
        private double projectedPoints;
        private double previousSeasonPoints;
        private int positionRank;
        private int byeWeek;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name {
            get { return name; } 
            set {
                if (name != value) {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the team.
        /// </summary>
        public string Team {
            get { return team; }
            set {
                if (team != value) {
                    team = value;
                    RaisePropertyChanged("Team");
                }
            }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public PositionType Position {
            get { return position; }
            set {
                if (position != value) {
                    position = value;
                    RaisePropertyChanged("Position");
                }
            }
        }

        /// <summary>
        /// Gets or sets the adp.
        /// </summary>
        public int Adp {
            get { return adp; }
            set {
                if (adp != value) {
                    adp = value;
                    RaisePropertyChanged("Adp");
                }
            }
        }

        /// <summary>
        /// Gets or sets the position rank.
        /// </summary>
        public int PositionRank {
            get { return positionRank; }
            set {
                if (positionRank != value) {
                    positionRank = value;
                    RaisePropertyChanged("PositionRank");
                }
            }
        }

        /// <summary>
        /// Gets or sets the bye week.
        /// </summary>
        public int ByeWeek {
            get { return byeWeek; }
            set {
                if (byeWeek != value) {
                    byeWeek = value;
                    RaisePropertyChanged("ByeWeek");
                }
            }
        }

        /// <summary>
        /// Gets or sets the projected points.
        /// </summary>
        public double ProjectedPoints {
            get { return projectedPoints; }
            set {
                if (projectedPoints != value) {
                    projectedPoints = value;
                    RaisePropertyChanged("ProjectedPoints");
                }
            }
        }

        /// <summary>
        /// Gets or sets the previous season points.
        /// </summary>
        public double PreviousSeasonPoints {
            get { return previousSeasonPoints; }
            set {
                if (previousSeasonPoints != value) {
                    previousSeasonPoints = value;
                    RaisePropertyChanged("PreviousSeasonPoints");
                }
            }
        }

        #endregion

        #region PropertyChangedHelper

        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(property);
                handler(this, e);
            }
        }

        #endregion
    }

    #region Enums

    public enum PositionType {
        QB,
        RB,
        WR,
        TE,
        PK,
        DST
    }

    public enum PlayerTeam {
        Bears,
        Bengals,
        Bills,
        Broncos,
        Browns,
        Bucaneers,
        Cardinals,
        Chargers,
        Chiefs,
        Colts,
        Cowboys,
        Dolphins,
        Eagles,
        Falcons,
        FortyNiners,
        Giants,
        Jaguars,
        Jets,
        Lions,
        Packers,
        Panthers,
        Patriots,
        Raiders,
        Rams,
        Ravens,
        Redskins,
        Saints,
        Seahawks,
        Steelers,
        Texans,
        Titans,
        Vikings
    }

    #endregion  

}

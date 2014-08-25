using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using FFDraftManager.Models;

namespace FFDraftManager.Services {
    public sealed class PlayersService {

        #region Private Data Members

        private static readonly string assetsFolderName = "Assets";
        private static readonly string standardPlayersFileName = "StandardRankings2014.csv";
        private static readonly string pprPlayersFileName = "PprRankings2014.csv";
        private static readonly string standardPlayersFilePath = Path.Combine(Directory.GetCurrentDirectory().ToString(), assetsFolderName, standardPlayersFileName);
        private static readonly string pprPlayersFilePath = Path.Combine(Directory.GetCurrentDirectory().ToString(), assetsFolderName, pprPlayersFileName);
        private static volatile PlayersService instance;
        private static object syncRoot = new Object();

        private static ObservableCollection<Player> players = new ObservableCollection<Player>();
        private static ObservableCollection<Player> standardPlayers = new ObservableCollection<Player>();
        private static ObservableCollection<Player> pprPlayers = new ObservableCollection<Player>();

        private ObservableCollection<Player> availablePlayers;
        private ObservableCollection<Player> availableQbs;
        private ObservableCollection<Player> availableRbs;
        private ObservableCollection<Player> availableWrs;
        private ObservableCollection<Player> availableTes;
        private ObservableCollection<Player> availablePks;
        private ObservableCollection<Player> availableDefs;
        private ObservableCollection<Player> availableWrRbs;
        private ObservableCollection<Player> availableWrRbTes;

        #endregion

        #region Construction

        private PlayersService() {
            SetPlayerList();
            InitializeAvailablePlayers();
        }

        public static PlayersService Instance {
            get {
                if (instance == null) {
                    lock (syncRoot) {
                        if (instance == null)
                            instance = new PlayersService();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the draft settings.
        /// </summary>
        private DraftSettingsService DraftSettings {
            get { return DraftSettingsService.Instance; }
        }

        public ObservableCollection<Player> AllPlayers {
            get {
                return players ?? (players = new ObservableCollection<Player>());
            }
            set {
                if (players != value) {
                    players = value;
                    RaisePropertyChanged("AllPlayers");
                }
            }
        }

        public ObservableCollection<Player> AllStandardPlayers {
            get {
                if (standardPlayers == null || standardPlayers.Count == 0) { GetStandardPlayersFromFile(); }
                return standardPlayers;
            }
        }

        public ObservableCollection<Player> AllPprPlayers {
            get {
                if (pprPlayers == null || pprPlayers.Count == 0) { GetPprPlayersFromFile(); }
                return pprPlayers;
            }
        }

        /// <summary>
        /// Gets or sets the available players.
        /// </summary>
        public ObservableCollection<Player> AvailablePlayers {
            get { return availablePlayers ?? (availablePlayers = new ObservableCollection<Player>()); }
            set {
                if (availablePlayers != value) {
                    availablePlayers = value;
                    RaisePropertyChanged("AvailablePlayers");
                    RaisePropertyChanged("AvailableQbs");
                    RaisePropertyChanged("AvailableRbs");
                    RaisePropertyChanged("AvailableWrs");
                    RaisePropertyChanged("AvailableTes");
                    RaisePropertyChanged("AvailablePks");
                    RaisePropertyChanged("AvailableDefs");
                    RaisePropertyChanged("AvailableWrRbs");
                    RaisePropertyChanged("AvailableWrRbTes");
                }
            }
        }

        /// <summary>
        /// Gets the available QBS.
        /// </summary>
        public ObservableCollection<Player> AvailableQbs { get { return availableQbs; } }

        /// <summary>
        /// Gets the available RBS.
        /// </summary>
        public ObservableCollection<Player> AvailableRbs { get { return availableRbs; } }

        /// <summary>
        /// Gets the available WRS.
        /// </summary>
        public ObservableCollection<Player> AvailableWrs { get { return availableWrs; } }

        /// <summary>
        /// Gets the available tes.
        /// </summary>
        public ObservableCollection<Player> AvailableTes { get { return availableTes; } }

        /// <summary>
        /// Gets the available PKS.
        /// </summary>
        public ObservableCollection<Player> AvailablePks { get { return availablePks; } }

        /// <summary>
        /// Gets the available DSTS.
        /// </summary>
        public ObservableCollection<Player> AvailableDefs { get { return availableDefs; } }

        /// <summary>
        /// Gets the available wr RBS.
        /// </summary>
        public ObservableCollection<Player> AvailableWrRbs { get { return availableWrRbs; } }

        /// <summary>
        /// Gets the available wr rb tes.
        /// </summary>
        public ObservableCollection<Player> AvailableWrRbTes { get { return availableWrRbTes; } }

        #endregion

        #region Methods

        public void SetPlayerList() {
            AllPlayers.Clear();
            if (DraftSettings.IsPPR) {
                foreach (var player in AllPprPlayers) {
                    AllPlayers.Add(player);
                }
            }
            else {
                foreach (var player in AllStandardPlayers) {
                    AllPlayers.Add(player);
                }
            }
        }

        private static PositionType GetPositionTypeFromString(string inputString) {
            switch (inputString) {
                case "QB":
                    return PositionType.QB;
                case "RB":
                    return PositionType.RB;
                case "WR":
                    return PositionType.WR;
                case "TE":
                    return PositionType.TE;
                case "DST":
                    return PositionType.DST;
                case "K":
                    return PositionType.PK;
                default:
                    return PositionType.QB;
            }
        }

        public void SetAvailablePlayers() {
            AvailablePlayers.Clear();
            foreach (Player player in AllPlayers) {
                availablePlayers.Add(player);
            }

            AvailableQbs.Clear();
            AvailableRbs.Clear();
            AvailableWrs.Clear();
            AvailableTes.Clear();
            AvailableDefs.Clear();
            AvailablePks.Clear();
            AvailableWrRbs.Clear();
            AvailableWrRbTes.Clear();

            foreach (Player player in availablePlayers.Where(p => p.Position == PositionType.QB)) { availableQbs.Add(player); }
            foreach (Player player in availablePlayers.Where(p => p.Position == PositionType.RB)) { AvailableRbs.Add(player); }
            foreach (Player player in availablePlayers.Where(p => p.Position == PositionType.WR)) { AvailableWrs.Add(player); }
            foreach (Player player in availablePlayers.Where(p => p.Position == PositionType.TE)) { AvailableTes.Add(player); }
            foreach (Player player in availablePlayers.Where(p => p.Position == PositionType.DST)) { AvailableDefs.Add(player); }
            foreach (Player player in availablePlayers.Where(p => p.Position == PositionType.PK)) { AvailablePks.Add(player); }
            foreach (Player player in availablePlayers.Where(p => p.Position == PositionType.RB || p.Position == PositionType.WR)) { AvailableWrRbs.Add(player); }
            foreach (Player player in availablePlayers.Where(p => p.Position == PositionType.RB || p.Position == PositionType.WR || p.Position == PositionType.TE)) { AvailableWrRbTes.Add(player); }

            RaisePropertyChanged("AvailablePlayers");
            RaisePropertyChanged("AvailableQbs");
            RaisePropertyChanged("AvailableRbs");
            RaisePropertyChanged("AvailableWrs");
            RaisePropertyChanged("AvailableTes");
            RaisePropertyChanged("AvailableDefs");
            RaisePropertyChanged("AvailablePks");
            RaisePropertyChanged("AvailableWrRbs");
            RaisePropertyChanged("AvailableWrRbTes");
        }

        public void InitializeAvailablePlayers() {
            AvailablePlayers.Clear();
            foreach (Player player in AllPlayers) {
                availablePlayers.Add(player);
            }

            availableQbs = new ObservableCollection<Player>(availablePlayers.Where(p => p.Position == PositionType.QB));
            availableRbs = new ObservableCollection<Player>(availablePlayers.Where(p => p.Position == PositionType.RB));
            availableWrs = new ObservableCollection<Player>(availablePlayers.Where(p => p.Position == PositionType.WR));
            availableTes = new ObservableCollection<Player>(availablePlayers.Where(p => p.Position == PositionType.TE));
            availablePks = new ObservableCollection<Player>(availablePlayers.Where(p => p.Position == PositionType.PK));
            availableDefs = new ObservableCollection<Player>(availablePlayers.Where(p => p.Position == PositionType.DST));
            availableWrRbs = new ObservableCollection<Player>(availablePlayers.Where(p => p.Position == PositionType.RB || p.Position == PositionType.WR));
            availableWrRbTes = new ObservableCollection<Player>(availablePlayers.Where(p => p.Position == PositionType.RB || p.Position == PositionType.WR || p.Position == PositionType.TE));

            RaisePropertyChanged("AvailablePlayers");
        }

        #endregion

        #region Data I/O

        private static void GetStandardPlayersFromFile() {
            standardPlayers = GetPlayersFromFile(standardPlayersFilePath);
        }

        private static void GetPprPlayersFromFile() {
            pprPlayers = GetPlayersFromFile(pprPlayersFilePath);
        }

        private static ObservableCollection<Player> GetPlayersFromFile(string filePath) {
            players.Clear();
            string line;
            int qbCount = 0, rbCount = 0, wrCount = 0, teCount = 0, dstCount = 0, kCount = 0;
            ObservableCollection<Player> playerList = new ObservableCollection<Player>();

            // Read the file and parse it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null) {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] lineValues = line.Split(',');

                //Build Position Rank
                string position = lineValues[2].ToString();
                int positionRank = 0;
                switch (position) {
                    case "QB":
                        positionRank = ++qbCount;
                        break;
                    case "RB":
                        positionRank = ++rbCount;
                        break;
                    case "WR":
                        positionRank = ++wrCount;
                        break;
                    case "TE":
                        positionRank = ++teCount;
                        break;
                    case "DST":
                        positionRank = ++dstCount;
                        break;
                    case "K":
                        positionRank = ++kCount;
                        break;
                    default:
                        break;
                }

                int byeWeek = 0;
                if (!int.TryParse(lineValues[4], out byeWeek)) {
                    byeWeek = 0;
                }

                //Map data to new player object
                Player newPlayer = new Player {
                    Name = lineValues[1].ToString().Trim(),
                    Position = GetPositionTypeFromString(position),
                    Team = lineValues[3].ToString().Trim(),
                    Adp = Convert.ToInt32(lineValues[0]),
                    PositionRank = positionRank,
                    ByeWeek = byeWeek
                };
                
                playerList.Add(newPlayer);
            }

            file.Close();
            return playerList;
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
using AD41HN_HFT_2022231.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MovieDbApp.RestClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AD41HN_HFT_2022231.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        static RestService r;


        //Players......................................................
        #region
        public RestCollection<Player> Players { get; set; }
        public ICommand CreatePlayerCommand { get; set; }
        public ICommand DeletePlayerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }

        private Player selectedPlayer;

        public Player SelectedPlayer
        {
            get { return selectedPlayer; }

            set {
                    if (value != null)
                    {
                        selectedPlayer = new Player()
                        {
                            Name = value.Name,
                            Id = value.Id
                        };
                        OnPropertyChanged();
                        (DeletePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                    }
                }
        }
        #endregion
        //Players......................................................
        //Teams........................................................
        #region
        public RestCollection<Team> Teams { get; set; }
        public ICommand CreateTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand UpdateTeamCommand { get; set; }

        private Team selectedTeam;

        public Team SelectedTeam
        {
            get { return selectedTeam; }

            set
            {
                if (value != null)
                {
                    selectedTeam = new Team()
                    {
                        Name = value.Name,
                        Id = value.Id
                    };
                    OnPropertyChanged();
                    (DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        #endregion
        //Teams........................................................
        //Trainers.....................................................
        #region
        public RestCollection<Trainer> Trainers { get; set; }
        public ICommand CreateTrainerCommand { get; set; }
        public ICommand DeleteTrainerCommand { get; set; }
        public ICommand UpdateTrainerCommand { get; set; }

        private Trainer selectedTrainer;

        public Trainer SelectedTrainer
        {
            get { return selectedTrainer; }

            set
            {
                if (value != null)
                {
                    selectedTrainer = new Trainer()
                    {
                        Name = value.Name,
                        Id = value.Id
                    };
                    OnPropertyChanged();
                    (DeleteTrainerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        #endregion
        //Trainers.....................................................

        public ICommand Player1Command { get; set; }
        public ICommand Player2Command { get; set; }
        public ICommand Player3Command { get; set; }
        public ICommand TeamCommand { get; set; }
        public ICommand TrainerCommand { get; set; }

        public Player Player1collection { get; set; }
        public ObservableCollection<Player> Player1Obscollection { get; set; }

        public List<Player> Player2collection { get; set; }
        public ObservableCollection<Player> Playe2Obscollection { get; set; }

        public List<Player> Player3collection { get; set; }
        public ObservableCollection<Player> Player3Obscollection { get; set; }

        public List<Team> Teamcollection { get; set; }
        public ObservableCollection<Team> TeamObscollection { get; set; }

        public List<Trainer> Trainercollection { get; set; }
        public ObservableCollection<Trainer> TrainerObscollection { get; set; }






        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                //Players......................................................
                #region
                Players = new RestCollection<Player>("http://localhost:5218/", "player", "hub");
                CreatePlayerCommand = new RelayCommand(() =>
                {
                    Players.Add(new Player()
                    {
                        Name = SelectedPlayer.Name
                    });
                });
                //
                DeletePlayerCommand = new RelayCommand(() =>
                {
                    Players.Delete(SelectedPlayer.Id);
                },
                ()=>
                {
                    return SelectedPlayer != null;
                });
                //

                UpdatePlayerCommand = new RelayCommand(()=>
                {
                    Players.Update(SelectedPlayer);
                }
                
                );
                SelectedPlayer = new Player();
                #endregion
                //Players..............................................................
                //Teams................................................................
                #region
                Teams = new RestCollection<Team>("http://localhost:5218/", "team", "hub");
                CreateTeamCommand = new RelayCommand(() =>
                {
                    Teams.Add(new Team()
                    {
                        Name = SelectedTeam.Name,
                    }) ;
                    ;
                });
                //
                DeleteTeamCommand = new RelayCommand(() =>
                {
                    Teams.Delete(SelectedTeam.Id);
                },
                () =>
                {
                    return SelectedTeam != null;
                });
                //

                UpdateTeamCommand = new RelayCommand(() =>
                {
                    Teams.Update(SelectedTeam);
                }

                );
                SelectedTeam = new Team();
                #endregion
                //Teams................................................................
                //Trainers.............................................................
                #region
                Trainers = new RestCollection<Trainer>("http://localhost:5218/", "trainer", "hub");
                CreateTrainerCommand = new RelayCommand(() =>
                {
                    Trainers.Add(new Trainer()
                    {
                        Name = SelectedTrainer.Name
                    });
                });
                //
                DeleteTrainerCommand = new RelayCommand(() =>
                {
                    Trainers.Delete(SelectedTrainer.Id);
                },
                () =>
                {
                    return SelectedTrainer != null;
                });
                //

                UpdateTrainerCommand = new RelayCommand(() =>
                {
                    Trainers.Update(SelectedTrainer);
                }

                );
                SelectedTrainer = new Trainer();

                #endregion
                //Trainers.............................................................

                r = new RestService("http://localhost:5218/");

                // player1
                Player1Obscollection = new ObservableCollection<Player>();
                Player1Command = new RelayCommand(() =>
                {
                    Player1Obscollection.Clear();
                    int id = SelectedPlayer.Id;
                    Player1collection = r.Get<Player>(id, "Player");
                });



               
            }
        }
    }
}

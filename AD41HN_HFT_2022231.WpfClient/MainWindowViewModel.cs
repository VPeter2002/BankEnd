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
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        //Non Crud.....................................................
        #region

        public ObservableCollection<Player> NonCrudObs = new ObservableCollection<Player>();

        public List<Player> NonCrudPlayers1list { get; set; }
        public List<Trainer> NonCrudTrainers { get; set; }
        public List<Team> NonCrudTeam { get; set; }

        public ICommand GetGoolKeepers { get; set; }
        public ICommand GetHungarianPlayers { get; set; }
        public ICommand GetEnglishTrainers { get; set; }
        public ICommand GetTeamIds { get; set; }
        public ICommand GetGermanyPlayers { get; set; }


        #endregion
        //Non Crud.....................................................

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                //Players..............................................................
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
                        Name = SelectedTeam.Name
                    });
                    
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

                //Non Crud.............................................................
                #region
                r = new RestService("http://localhost:5218/");

                NonCrudObs = new ObservableCollection<Player>();

                GetGoolKeepers = new RelayCommand(() =>
                {
                    NonCrudObs.Clear();
                    string personname = SelectedPlayer.Name;
                    NonCrudPlayers1list = r.Get<Player>("Non_crud/GetGKs");
                    foreach (var item in NonCrudPlayers1list)
                    {
                        NonCrudObs.Add(item);
                    }
                });

                GetHungarianPlayers = new RelayCommand(() =>
                {
                    NonCrudObs.Clear();
                    string personname = SelectedPlayer.Name;
                    NonCrudPlayers1list = r.Get<Player>("Non_crud/GetHun");
                    foreach (var item in NonCrudPlayers1list)
                    {
                        NonCrudObs.Add(item);
                    }
                });
                GetEnglishTrainers = new RelayCommand(() =>
                {
                    NonCrudObs.Clear();
                    string personname = SelectedPlayer.Name;
                    NonCrudPlayers1list = r.Get<Player>("Non_crud/GetEng");
                    foreach (var item in NonCrudPlayers1list)
                    {
                        NonCrudObs.Add(item);
                    }
                });
                GetTeamIds = new RelayCommand(() =>
                {
                    NonCrudObs.Clear();
                    string personname = SelectedPlayer.Name;
                    NonCrudPlayers1list = r.Get<Player>("Non_crud/GetTeamIds");
                    foreach (var item in NonCrudPlayers1list)
                    {
                        NonCrudObs.Add(item);
                    }
                });
                GetGermanyPlayers = new RelayCommand(() =>
                {
                    NonCrudObs.Clear();
                    string personname = SelectedPlayer.Name;
                    NonCrudTrainers = r.Get<Trainer>("Non_crud/GetGermanyTrainers");
                    foreach (var item in NonCrudPlayers1list)
                    {
                        NonCrudObs.Add(item);
                    }
                });
                #endregion
                //Non Crud.............................................................
            }
        }
    }
}

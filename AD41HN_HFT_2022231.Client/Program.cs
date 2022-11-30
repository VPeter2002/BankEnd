using AD41HN_HFT_2022231.Models;
using ConsoleTools;
using Microsoft.EntityFrameworkCore;
using MovieDbApp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AD41HN_HFT_2022231.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "Player")
            {
                Console.Write("Enter Player Name: ");
                string name = Console.ReadLine();
                rest.Post(new Player() { Name = name }, "player");
            }
            else if (entity=="Team")
            {
                Console.Write("Enter Team Name: ");
                string name = Console.ReadLine();
                rest.Post(new Player() { Name = name }, "team");
            }
            else
            {
                Console.Write("Enter Trainer Name: ");
                string name = Console.ReadLine();
                rest.Post(new Player() { Name = name }, "trainer");
            }
        }
        static void List(string entity)
        {
            if (entity == "Player")
            {
                List<Player> Player = rest.Get<Player>("player");
                foreach (var item in Player)
                {
                    Console.WriteLine(item.Id + ": " + item.Name);
                }
            }
            else if (entity == "Team")
            {
                List<Team> Teams = rest.Get<Team>("team");
                foreach (var item in Teams)
                {
                    Console.WriteLine(item.Id + ": " + item.Name);
                }
            }
            else
            {
                List<Trainer> Trainers = rest.Get<Trainer>("trainer");
                foreach (var item in Trainers)
                {
                    Console.WriteLine(item.Id + ": " + item.Name);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Player")
            {
                Console.Write("Enter Player's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Player one = rest.Get<Player>(id, "player");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "player");
            }
            else if (entity=="Team")
            {
                Console.Write("Enter Team's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Team one = rest.Get<Team>(id, "team");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "team");
            }
            else
            {
                Console.Write("Enter Trainer's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Trainer one = rest.Get<Trainer>(id, "trainer");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "trainer");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Player")
            {
                Console.Write("Enter Player's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "player");
            }
            else if (entity == "Team")
            {
                Console.Write("Enter Team's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "team");
            }
            else
            {
                Console.Write("Enter Trainer's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "trainer");
            }
        }
        
        static void Main(string[] args)
        {
            

            rest = new RestService("http://localhost:5218/");

            var playerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Player"))
                .Add("Create", () => Create("Player"))
                .Add("Delete", () => Delete("Player"))
                .Add("Update", () => Update("Player"))
                .Add("Exit", ConsoleMenu.Close);

            var teamSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Team"))
                .Add("Create", () => Create("Team"))
                .Add("Delete", () => Delete("Team"))
                .Add("Update", () => Update("Team"))
                .Add("Exit", ConsoleMenu.Close);

            var trainerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Trainer"))
                .Add("Create", () => Create("Trainer"))
                .Add("Delete", () => Delete("Trainer"))
                .Add("Update", () => Update("Trainer"))
                .Add("Exit", ConsoleMenu.Close);




            var menu = new ConsoleMenu(args, level: 0)
                .Add("Player", () => playerSubMenu.Show())
                .Add("Team", () => teamSubMenu.Show())
                .Add("Trainer", () => trainerSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            
            menu.Show();




        }
    }
}


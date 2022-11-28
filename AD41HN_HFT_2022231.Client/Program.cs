

using AD41HN_HFT_2022231.Logic.Classes;
using AD41HN_HFT_2022231.Models;
using AD41HN_HFT_2022231.Repository;
using AD41HN_HFT_2022231.Repository.ModelRepositories;
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
        }
        static void List(string entity)
        {
            if (entity == "Player")
            {
                List<Player> actors = rest.Get<Player>("player");
                foreach (var item in actors)
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
                Player one = rest.Get<Player>(id, "actor");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "player");
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
        }
        static void Main(string[] args)
        {
            

            rest = new RestService("http://localhost:5218/", "db");

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


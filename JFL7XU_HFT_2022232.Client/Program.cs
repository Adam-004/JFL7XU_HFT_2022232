using JFL7XU_HFT_2022232.Models;
using System;
using ConsoleTools;
using System.ComponentModel.DataAnnotations;

namespace JFL7XU_HFT_2022232.Client
{
    class Program
    {
        static RestService rest;
        #region Methods
        static void Create(string model)
        {
            if (model == "Owner")
            {
                string Name;
                int Age;
                int ID;

                Console.Write("Owner's id:\t");
                ID = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.Write("Owner's name:\t");
                Name = Console.ReadLine();

                Console.WriteLine();
                Console.Write("Owner's age:\t");
                Age = int.Parse(Console.ReadLine());

                var o = new Owner(ID, Name, Age);
                rest.Post(o, "Owner");
            }
            else if (model == "Ship")
            {
                string Name;
                int Size;
                int ID;
                int Yom;
                int Type;
                int oID;

                Console.Write("Starships's id:\t");
                ID = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.Write("Starships's name:\t");
                Name = Console.ReadLine();
                Console.WriteLine();

                Console.Write("Starships's size (in tons):\t");
                Size = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.Write("Starships's year of manufacture:\t");
                Yom = int.Parse(Console.ReadLine());

                Console.WriteLine("Starship types:");
                for (int i = 1; i < 5; i++)
                {
                    Console.WriteLine(i + " = " + ((ShipType)i));
                }
                Console.WriteLine("Starships's type (number):\t");
                Type = int.Parse(Console.ReadLine());

                Console.WriteLine("Owner's ID:\t");
                oID = int.Parse(Console.ReadLine());

                var o = new Starship(ID, Name, Size, Yom, Type, oID);
                rest.Post(o, "Starship");
            }
            else if (model == "Hangar")
            {
                int ID;
                string Name;
                string Location;
                int oID;

                Console.Write("Hangars's id:\t");
                ID = int.Parse(Console.ReadLine());

                Console.WriteLine();

                Console.Write("Hangars's name:\t");
                Name = Console.ReadLine();

                Console.WriteLine();
                Console.Write("Hangars's location:\t");
                Location = Console.ReadLine();

                Console.Write("Owner's id:\t");
                oID = int.Parse(Console.ReadLine());

                var o = new Hangar(ID, Name, Location, oID);
                rest.Post(o, "Hangars");
            }
        }
        static void Read(string model)
        {

        }
        static void ReadAll(string model)
        {

        }
        static void Update(string model)
        {

        }
        static void Delete(string model)
        {

        }
        static void ListShips_WhichBuiltAfter(int year)
        {

        }
        static void ListHangars_WithShipsMoreThan(int quantity)
        {

        }
        static void ListHangars_WithShipsLessThan(int quantity)
        {

        }
        static void ListOwners_OlderThan(int year)
        {

        }
        static void ListOwners_YoungerThan(int year)
        {

        }
        static void ListOwners_YoungerAndHasMoreShipsThan(int year, int quantity)
        {

        }
        static void ListStatistics()
        {

        }
        #endregion

        public static void Main(string[] args)
        {
            rest = new RestService("http://localhost:40566/","FuturisticProperties");

            #region Menu
            //Statistics Menu
            var mainstatsSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List all ships that are built after given year", () => ListShips_WhichBuiltAfter())
                .Add("List all hangars that has more than 'X' ships", () => ListHangars_WithShipsMoreThan())
                .Add("List all hangars that has less than 'X' ships", () => ListHangars_WithShipsLessThan())
                .Add("List all owners who are older than", () => ListOwners_OlderThan())
                .Add("List all owners who are younger than", () => ListOwners_YoungerThan())
                .Add("List all owners who are younger than 'X' and has more ships than", () => ListOwners_YoungerAndHasMoreShipsThan())
                .Add("List report", () => ListStatistics())
                .Add("Exit", ConsoleMenu.Close)
                .Configure(config => {
                    config.Selector = "O> ";
                    config.EnableFilter = true;
                    config.Title = "Statistics Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            //Hangars Menu
            var hangarSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Create new hangar", () => Create("Hangar"))
                .Add("Read hangar by given ID", () => Read("Hangar"))
                .Add("Update specified hangar", () => Update("Hangar"))
                .Add("Delete specified hangar", () => Delete("Hangar"))
                .Add("List all hangar", () => ReadAll("Hangar"))
                .Add("Exit", ConsoleMenu.Close)
                .Configure(config => {
                    config.Selector = "O> ";
                    config.EnableFilter = true;
                    config.Title = "Hangars Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            //Starships Menu
            var starshipSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Create new starship", () => Create("Ship"))
                .Add("Read starship by given ID", () => Read("Ship"))
                .Add("Update specified starship", () => Update("Ship"))
                .Add("Delete specified starship", () => Delete("Ship"))
                .Add("List all starships", () => ReadAll("Ship"))
                .Add("Exit", ConsoleMenu.Close)
                .Configure(config => {
                    config.Selector = "O> ";
                    config.EnableFilter = true;
                    config.Title = "Starships Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            //Owners Menu
            var ownerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Create new owner", () => Create("Owner"))
                .Add("Read owner by given ID", () => Read("Owner"))
                .Add("Update specified owner", () => Update("Owner"))
                .Add("Delete specified owner", () => Delete("Owner"))
                .Add("List all owners", () => ReadAll("Owner"))
                .Add("Exit", ConsoleMenu.Close)
                .Configure(config => {
                    config.Selector = "O> ";
                    config.EnableFilter = true;
                    config.Title = "Owners Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            //Main menu
            var menu = new ConsoleMenu(args, level: 0)
                .Add("Owners", () => ownerSubMenu.Show())
                .Add("Hangars", () => hangarSubMenu.Show())
                .Add("Starships", () => starshipSubMenu.Show())
                .Add("Main Statistics", () => mainstatsSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close)
                .Configure(config => {
                     config.Selector = "O> ";
                     config.EnableFilter = true;
                     config.Title = "Main Menu";
                     config.EnableBreadcrumb = true;
                     config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                 });
            menu.Show();
            #endregion
        }
    }
}

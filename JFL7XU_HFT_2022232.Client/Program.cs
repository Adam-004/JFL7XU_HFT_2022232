using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.Client;
using System;
using ConsoleTools;


namespace JFL7XU_HFT_2022232.Client
{
    class Program
    {

        public static void Main(string[] args)
        {
            Functions.Init();
            #region Menu
            //Statistics Menu
            var mainstatsSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List all ships that are built after given year", () => Functions.ExpHandle(() => Functions.ListShips_WhichBuiltAfter()))
                .Add("List all hangars that has more than 'X' ships", () => Functions.ExpHandle(() => Functions.ListHangars_WithShipsMoreThan()))
                .Add("List all hangars that has less than 'X' ships", () => Functions.ExpHandle(() => Functions.ListHangars_WithShipsLessThan()))
                .Add("List all owners who are older than", () => Functions.ExpHandle(() => Functions.ListOwners_OlderThan()))
                .Add("List all owners who are younger than", () => Functions.ExpHandle(() => Functions.ListOwners_YoungerThan()))
                .Add("List all owners who are younger than 'X' and has more ships than", () => Functions.ExpHandle(() => Functions.ListOwners_YoungerAndHasMoreShipsThan()))
                .Add("List report", () => Functions.ListStatistics())
                .Add("Return", ConsoleMenu.Close)
                .Configure(config => {
                    config.Selector = "O> ";
                    config.EnableFilter = true;
                    config.Title = "Statistics Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            //Hangars Menu
            var hangarSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Create new hangar", () => Functions.ExpHandle(() => Functions.Create("Hangar")))
                .Add("Read hangar by given ID", () => Functions.ExpHandle(() => Functions.Read("Hangar")))
                .Add("Update specified hangar", () => Functions.ExpHandle(() => Functions.Update("Hangar")))
                .Add("Delete specified hangar", () => Functions.ExpHandle(() => Functions.Delete("Hangar")))
                .Add("List all hangar", () => Functions.ReadAll("Hangar"))
                .Add("Return", ConsoleMenu.Close)
                .Configure(config => {
                    config.Selector = "O> ";
                    config.EnableFilter = true;
                    config.Title = "Hangars Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            //Starships Menu
            var starshipSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Create new starship", () => Functions.ExpHandle(() => Functions.Create("Ship")))
                .Add("Read starship by given ID", () => Functions.ExpHandle(() => Functions.Read("Ship")))
                .Add("Update specified starship", () => Functions.ExpHandle(() => Functions.Update("Ship")))
                .Add("Delete specified starship", () => Functions.ExpHandle(() => Functions.Delete("Ship")))
                .Add("List all starships", () => Functions.ReadAll("Ship"))
                .Add("Return", ConsoleMenu.Close)
                .Configure(config => {
                    config.Selector = "O> ";
                    config.EnableFilter = true;
                    config.Title = "Starships Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            //Owners Menu
            var ownerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Create new owner", () => Functions.ExpHandle(() => Functions.Create("Owner")))
                .Add("Read owner by given ID", () => Functions.ExpHandle(() => Functions.Read("Owner")))
                .Add("Update specified owner", () => Functions.ExpHandle(() => Functions.Update("Owner")))
                .Add("Delete specified owner", () => Functions.ExpHandle(() => Functions.Delete("Owner")))
                .Add("List all owners", () => Functions.ReadAll("Owner"))
                .Add("Return", ConsoleMenu.Close)
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

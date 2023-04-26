using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Client
{
    public static class Functions
    {
        static RestService rest;
        public static void Init()
        {
            rest = new RestService("http://localhost:40566/");
        }
        #region CRUD
        public static void Create(string model)
        {
            Console.Clear();
            Console.WriteLine("Create " + model);
            if (model == "Owner")
            {
                string Name;
                int Age;
                int ID;

                Console.Write("Owner's ID:\t");
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

                Console.Write("Starships's ID:\t");
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

                Console.Write("Hangars's ID:\t");
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
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        public static void Read(string model)
        {
            Console.Clear();
            Console.Write("Read "+model+"\n\nID to search:\t");
            int ID = int.Parse(Console.ReadLine());
            object o = null;
            if (model == "Owner")
            {
                o = rest.Get<Owner>(ID, "Owner");
            }
            else if (model == "Ship")
            {
                o = rest.Get<Owner>(ID, "Starship");
            }
            else if (model == "Hangar")
            {
                o = rest.Get<Owner>(ID, "Hangar");
            }
            Console.Clear();
            Console.WriteLine("Listed " + model + " with ID: "+ID);
            Console.WriteLine(o.ToString());
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        public static void ReadAll(string model)
        {
            Console.Clear();
            Console.WriteLine("Listed all " + model + "s");
            if (model == "Owner")
            {
                List<Owner> Out = rest.Get<Owner>("Owner");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else if (model == "Ship")
            {
                List<Starship> Out = rest.Get<Starship>("Starship");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else if (model == "Hangar")
            {
                List<Hangar> Out = rest.Get<Hangar>("Hangar");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        public static void Update(string model)
        {
            Console.Clear();
            Console.WriteLine("Update " + model+"\n");
            if (model == "Owner")
            {
                string Name;
                int Age;
                int ID;

                List<Owner> Out = rest.Get<Owner>("Owner");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.Write("Owner's ID:\t");
                ID = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.Write("Owner's name:\t");
                Name = Console.ReadLine();

                Console.WriteLine();
                Console.Write("Owner's age:\t");
                Age = int.Parse(Console.ReadLine());

                var o = new Owner(ID, Name, Age);
                rest.Put(o, "Owner");

                Console.Clear();
                Console.WriteLine("Updated " + model + " on ID: " + ID);
                Out = rest.Get<Owner>("Owner");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else if (model == "Ship")
            {
                string Name;
                int Size;
                int ID;
                int Yom;
                int Type;
                int oID;

                List<Starship> Out = rest.Get<Starship>("Starship");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.Write("Starships's id:\t");
                ID = int.Parse(Console.ReadLine());
                Console.WriteLine();

                Console.Write("Starship's name:\t");
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
                rest.Put(o, "Starship");

                Console.Clear();
                Console.WriteLine("Updated " + model + " on ID: " + ID);
                Out = rest.Get<Starship>("Starship");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else if (model == "Hangar")
            {
                int ID;
                string Name;
                string Location;
                int oID;

                List<Hangar> Out = rest.Get<Hangar>("Hangar");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }

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
                rest.Put(o, "Hangars");

                Console.Clear();
                Console.WriteLine("Updated "+model+" on ID: " + ID);
                Out = rest.Get<Hangar>("Hangar");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        public static void Delete(string model)
        {
            int ID;

            Console.Clear();
            Console.WriteLine("Delete " + model + "\n");
            if (model == "Owner")
            {
                List<Owner> Out = rest.Get<Owner>("Owner");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.Write("Owner to delete:\nOwner's ID:\t");
                ID = int.Parse(Console.ReadLine());
                Console.WriteLine();

                rest.Delete(ID, "Owner");

                Console.Clear();
                Console.WriteLine("Updated " + model + " on ID: " + ID);

                Out = rest.Get<Owner>("Owner");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else if (model == "Ship")
            {
                List<Starship> Out = rest.Get<Starship>("Starship");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.Write("Starship to delete:\nStarship's ID:\t");
                ID = int.Parse(Console.ReadLine());
                Console.WriteLine();

                rest.Delete(ID, "Owner");

                Console.Clear();
                Console.WriteLine("Updated " + model + " on ID: " + ID);

                Out = rest.Get<Starship>("Starship");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else if (model == "Hangar")
            {
                List<Hangar> Out = rest.Get<Hangar>("Hangar");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.Write("Hangar to delete:\nHangar's ID:\t");
                ID = int.Parse(Console.ReadLine());
                Console.WriteLine();

                rest.Delete(ID, "Hangar");

                Console.Clear();
                Console.WriteLine("Updated " + model + " on ID: " + ID);

                Out = rest.Get<Hangar>("Hangar");
                foreach (var item in Out)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        #endregion
        #region Non-CRUD
        public static void ListShips_WhichBuiltAfter(int year)
        {

        }
        public static void ListHangars_WithShipsMoreThan(int quantity)
        {

        }
        public static void ListHangars_WithShipsLessThan(int quantity)
        {

        }
        public static void ListOwners_OlderThan(int year)
        {

        }
        public static void ListOwners_YoungerThan(int year)
        {

        }
        public static void ListOwners_YoungerAndHasMoreShipsThan(int year, int quantity)
        {

        }
        public static void ListStatistics()
        {

        }
        #endregion
    }
}

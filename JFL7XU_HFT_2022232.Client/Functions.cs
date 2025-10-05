using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Client
{
    public static class Functions
    {
        static RestService rest;
        public static void Init()
        {
            rest = new RestService("http://localhost:40567/");
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

                Console.Write("Starships's age of manufacture:\t");
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
                rest.Post(o, "Hangar");
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
                o = rest.Get<Starship>(ID, "Starship");
            }
            else if (model == "Hangar")
            {
                o = rest.Get<Hangar>(ID, "Hangar");
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

                Console.Write("\nOwner's ID:\t");
                ID = int.Parse(Console.ReadLine());

                Console.Write("Owner's name:\t");
                Name = Console.ReadLine();

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

                Console.Write("\nStarships's id:\t");
                ID = int.Parse(Console.ReadLine());

                Console.Write("Starship's name:\t");
                Name = Console.ReadLine();

                Console.Write("Starships's size (in tons):\t");
                Size = int.Parse(Console.ReadLine());

                Console.Write("Starships's age of manufacture:\t");
                Yom = int.Parse(Console.ReadLine());

                Console.WriteLine("\nStarship types:");
                for (int i = 1; i < 5; i++)
                {
                    Console.WriteLine(i + " = " + ((ShipType)i));
                }
                Console.Write("Starships's type (number):\t");
                Type = int.Parse(Console.ReadLine());

                Console.Write("Owner's ID:\t");
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

                Console.Write("\nHangars's id:\t");
                ID = int.Parse(Console.ReadLine());

                Console.Write("Hangars's name:\t");
                Name = Console.ReadLine();
                Console.Write("Hangars's location:\t");
                Location = Console.ReadLine();

                Console.Write("Owner's id:\t");
                oID = int.Parse(Console.ReadLine());

                var o = new Hangar(ID, Name, Location, oID);
                rest.Put(o, "Hangar");

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

                Console.Write("\nOwner to delete:\nOwner's ID:\t");
                ID = int.Parse(Console.ReadLine());
                Console.WriteLine();

                rest.Delete(ID, "Owner");

                Console.Clear();
                Console.WriteLine("Deleted " + model + " on ID: " + ID);

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

                Console.Write("\nStarship to delete:\nStarship's ID:\t");
                ID = int.Parse(Console.ReadLine());
                Console.WriteLine();

                rest.Delete(ID, "Starship");

                Console.Clear();
                Console.WriteLine("Deleted " + model + " on ID: " + ID);

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

                Console.Write("\nHangar to delete:\nHangar's ID:\t");
                ID = int.Parse(Console.ReadLine());
                Console.WriteLine();

                rest.Delete(ID, "Hangar");

                Console.Clear();
                Console.WriteLine("Deleted " + model + " on ID: " + ID);

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
        public static void ListShips_WhichBuiltAfter()
        {

            int year;

            Console.Clear();
            Console.Write($"Year: ");
            year = int.Parse(Console.ReadLine());
            
            Console.Clear();
            Console.WriteLine($"Listing Ships which are built after {year}:\n");

            List<Starship> Out = rest.Get<Starship>("NonCrud/ListShips_WhichBuiltAfter/"+year);
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        public static void ListHangars_WithShipsMoreThan()
        {
            int quantity;

            Console.Clear();
            Console.Write($"Quantity: ");
            quantity = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.WriteLine($"Listing hangars that have more than {quantity} Ships:\n");

            List<Hangar> Out = rest.Get<Hangar>("NonCrud/ListHangars_WithShipsMoreThan/" + quantity);
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        public static void ListHangars_WithShipsLessThan()
        {
            int quantity;

            Console.Clear();
            Console.Write($"Quantity: ");
            quantity = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.WriteLine($"Listing hangars that have less than {quantity} Ships:\n");

            List<Hangar> Out = rest.Get<Hangar>("NonCrud/ListHangars_WithShipsLessThan/" + quantity);
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        public static void ListOwners_OlderThan()
        {
            int age;

            Console.Clear();
            Console.Write($"Age: ");
            age = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.WriteLine($"Listing owners who are older than {age}:\n");

            List<Owner> Out = rest.Get<Owner>("NonCrud/ListOwners_OlderThan/" + age);
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        public static void ListOwners_YoungerThan()
        {
            int age;

            Console.Clear();
            Console.Write($"Age: ");
            age = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.WriteLine($"Listing owners who are younger than {age}:\n");

            List<Owner> Out = rest.Get<Owner>("NonCrud/ListOwners_YoungerThan/" + age);
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        public static void ListOwners_YoungerAndHasMoreShipsThan()
        {
            int age;
            int quantity;

            Console.Clear();
            Console.Write($"Age: ");
            age = int.Parse(Console.ReadLine());
            Console.Write($"Quantity: ");
            quantity = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.WriteLine($"Listing owners who are younger than {age} and has more Ships than {quantity}:\n");

            List<Owner> Out = rest.Get<Owner>($"NonCrud/ListOwners_YoungerAndHasMoreShipsThan/{age}/{quantity}");
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        public static void ListStatistics()
        {
            Console.Clear();
            Console.WriteLine("Listing Statistics\n");
            List<OwnershipStatistics> Out = rest.Get<OwnershipStatistics>("NonCrud/ListStatistics");
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadLine();
        }
        #endregion
        #region ExceptionHandling
        public static void ExpHandle(Action function)
        {
            try
            {
                function();
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine("Error: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("\nPress any key to continue.");
                Console.ReadLine();
            }
        }
        #endregion
    }
}

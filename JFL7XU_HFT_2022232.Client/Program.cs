using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace JFL7XU_HFT_2022232.Client
{
    class Program
    {
        public static void Main()
        {
            //Tesztelés

            /*IEnumerable<OwnershipStatistics> Out = NonCrudLogic.ListStatistics();
            foreach (var item in Out)
            {
                Console.WriteLine(item.ToString());
            }*/
            /*var owner = OwnerLogic.Read(11);
            Console.WriteLine("Owner: " + owner.Name);
            /*foreach (var item in OwnerRepo.ReadAll())
            {
                Console.WriteLine(item.ID+"\t"+item.Name);
            }
            Console.WriteLine();
            var newOwner = new Owner(10,"Balogh Ádám",21);
            OwnerRepo.Create(newOwner);
            foreach (var item in OwnerRepo.ReadAll())
            {
                Console.WriteLine(item.ID + "\t" + item.Name);
            }
            Console.WriteLine();
            OwnerRepo.Delete(newOwner.ID);
            foreach (var item in OwnerRepo.ReadAll())
            {
                Console.WriteLine(item.ID + "\t" + item.Name);
            }
            /*Console.WriteLine("---Owners---");
            var FirstOwner = OwnerRepo.Read(3);
            Console.WriteLine(FirstOwner.Name);
            foreach (var item in FirstOwner.Ships)
            {
                Console.WriteLine(((ShipType)item.Type).ToString());
            }
            /*foreach (var item in OwnerRepo.ReadAll())
            {
                foreach (var prop in item.GetType().GetProperties())
                {
                    Console.WriteLine(prop.GetValue(item));
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("---Starships---");
            foreach (var item in StarshipRepo.ReadAll())
            {
                foreach (var prop in item.GetType().GetProperties())
                {
                    Console.WriteLine(prop.GetValue(item));
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("---Hangars---");
            foreach (var item in HangarRepo.ReadAll())
            {
                foreach (var prop in item.GetType().GetProperties())
                {
                    Console.WriteLine(prop.GetValue(item));
                }
                Console.WriteLine("\n");
            }*/
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleTables;

namespace ApiClient
{
    class Program
    {
        static async Task ShowAllPeople(string token, string pageNumber)
        {
            var client = new HttpClient();
            var responseAsStream = await client.GetStreamAsync($"https://swapi.dev/api/{token}/?page={pageNumber}");
            var starWarsPeople = await JsonSerializer.DeserializeAsync<PeopleResultsContainer>(responseAsStream);

            var table = new ConsoleTable("Name", "Height", "Birth year");

            foreach (var person in starWarsPeople.results)
            {
                table.AddRow(person.Name, person.Height, person.BirthYear);
            }


            table.Write();
        }



        static async Task Main(string[] args)
        {

            var token = "";
            var pageNumber = "";
            if (args.Length == 0)
            {
                Console.Write("What list would you like? ");
                token = Console.ReadLine();

                Console.Write("What page would you like? ");
                pageNumber = Console.ReadLine();

            }
            else
            {
                token = args[0];
            }

            await ShowAllPeople(token, pageNumber);
            Console.ReadLine();
        }
    }
}

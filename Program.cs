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

        static async Task ShowAllSpecies(string token, string pageNumber)
        {
            var client = new HttpClient();
            var responseAsStream = await client.GetStreamAsync($"https://swapi.dev/api/{token}/?page={pageNumber}");
            var starWarsSpecies = await JsonSerializer.DeserializeAsync<SpeciesResultsContainer>(responseAsStream);

            var table = new ConsoleTable("Name", "Classification", "Average Height", "Language");

            foreach (var species in starWarsSpecies.results)
            {
                table.AddRow(species.Name, species.Classification, species.AvgHeight, species.Language);
            }


            table.Write();
        }





        static async Task Main(string[] args)
        {

            var token = "";
            var pageNumber = "";
            if (args.Length == 0)
            {
                var exitChoice = false;
                while (exitChoice == false)
                {
                    Console.Write("What list would you like? ");
                    token = Console.ReadLine();

                    Console.Write("What page would you like? ");
                    pageNumber = Console.ReadLine();

                    switch (token)
                    {
                        case "species":
                            await ShowAllSpecies(token, pageNumber);
                            Console.ReadLine();
                            Console.WriteLine("Press ENTER");
                            break;
                        case "people":
                            await ShowAllPeople(token, pageNumber);
                            Console.ReadLine();
                            Console.WriteLine("Press ENTER");
                            break;
                        case "quit":
                            exitChoice = true;
                            break;
                    }
                }

            }
            else
            {
                token = args[0];
            }

        }


    }
}

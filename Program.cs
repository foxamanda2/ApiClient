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


            table.Write(Format.Minimal);
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


            table.Write(Format.Minimal);
        }

        static async Task SearchPerson(string tokenPerson)
        {
            var client = new HttpClient();
            var responseAsStream = await client.GetStreamAsync($"https://swapi.dev/api/people/?search={tokenPerson}");
            var starWarsPerson = await JsonSerializer.DeserializeAsync<PeopleResultsContainer>(responseAsStream);

            var table = new ConsoleTable("Name", "Height", "Hair Color", "Eye Color", "Gender");

            foreach (var person in starWarsPerson.results)
            {
                table.AddRow(person.Name, person.Height, person.HairColor, person.EyeColor, person.Gender);
            }

            table.Write(Format.Minimal);
        }

        static async Task Main(string[] args)
        {
            var tokenPicked = "";
            var pageNumber = "";

            var exitChoice = false;
            while (exitChoice == false)
            {
                Console.Write("What list would you like? (q to quit) ");
                tokenPicked = Console.ReadLine();
                switch (tokenPicked)
                {
                    case "species":
                        Console.WriteLine("Please Pick an Option:");
                        Console.WriteLine(" (P)rint species on page.");
                        var userChoiceSpecies = Console.ReadLine().ToLower();

                        if (userChoiceSpecies == "p")
                        {
                            Console.Write("What page would you like? ");
                            pageNumber = Console.ReadLine();
                            await ShowAllSpecies(tokenPicked, pageNumber);
                            Console.WriteLine("Press ENTER");
                            Console.ReadLine();
                        }
                        break;
                    case "people":
                        Console.WriteLine("Please Pick an Option:");
                        Console.WriteLine(" (P)rint people on page. \n (F)ind a person.\n");
                        var userChoicePeople = Console.ReadLine().ToLower();

                        if (userChoicePeople == "p")
                        {
                            Console.Write("What page would you like? ");
                            pageNumber = Console.ReadLine();
                            await ShowAllPeople(tokenPicked, pageNumber);
                            Console.WriteLine("Press ENTER");
                            Console.ReadLine();
                        }
                        if (userChoicePeople == "f")
                        {
                            Console.Write("What person are you looking for?\n");
                            var pickedPerson = Console.ReadLine();
                            await SearchPerson(pickedPerson);
                        }
                        break;
                    case "q":
                        exitChoice = true;
                        break;
                }
            }
        }
    }
}


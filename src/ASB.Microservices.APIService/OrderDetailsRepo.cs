using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ASB.Microservices.APIService.Client.Commands;

namespace ASB.Microservices.APIService
{
    public static class OrderDetailsRepo
    {
        // try to change this to usual Dictionary - there's big chance to run into race condition and errors :)
        private static readonly ConcurrentDictionary<int, OrderDetailsResult> Items = new ConcurrentDictionary<int, OrderDetailsResult>();
        
        public static OrderDetailsResult Get(int id)
        {
            if (Items.TryGetValue(id, out var details))
            {
                return details;
            }
            
            var rnd = new Random();
            details = new OrderDetailsResult
            {
                OrderId = id,
                DateCreated = DateTime.Now.AddDays(-rnd.Next(5, 500)),
                Amount = (decimal) (10 + 1000 * rnd.NextDouble()),
                ItemsCount = 1 + rnd.Next(20),
                UserId = 1 + rnd.Next(2000),
                UserName = Names[rnd.Next(Names.Count)] 
            };

            Items[id] = details;
            return details;
        }

        private static readonly List<string> Names = new List<string>()
        {
            "Renee Macleod",
            "Pixie Mccann",
            "Keane Holland",
            "Dane Odonnell",
            "Aayan Broadhurst",
            "Dixie Hodge",
            "Jolyon Mccarthy",
            "Nur Barrett",
            "Adaline Merrill",
            "Hudson Collier",
            "Abraham Newton",
            "Kaitlan Dunne",
            "Marcus Adam",
            "Remi Carpenter",
            "Rhydian Devlin",
            "Presley Horne",
            "Juniper Logan",
            "Patrick Morris",
            "Diesel Barr",
            "Everett Liu",
            "Evelina Chapman",
            "Jakub Kennedy",
            "Billie James",
            "Ava-Rose Cabrera",
            "Sonnie Mcknight",
            "Tyler Burn",
            "Jenson Bolton",
            "Safiya Grimes",
            "Sacha Bevan",
            "Bo Thompson",
            "Theodor Thomson",
            "Georga Velez",
            "Zahra Mercer",
            "Aubrey Gunn",
            "Nabeela Mill",
            "Faheem Warren",
            "Arisha Turnbull",
            "William Rivas",
            "Aran Huang",
            "Albi House",
            "Kyron Nielsen",
            "Jonah Carrillo",
            "Juan Kirk",
            "Khadeejah Rayner",
            "Matei Aldred",
            "Frazer Aguilar",
            "Tanisha Wade",
            "Alara Bray",
            "Arissa Schmidt",
            "Elicia Franco",
            "Amrit Bannister",
            "Ayush Davies",
            "Camron Wang",
            "Rylee Pate",
            "Connar Wilson",
            "Kingsley Clark",
            "Celyn Lindsay",
            "Frank Merrill",
            "Vishal Reeve",
            "Chaya Lutz",
            "Branden Proctor",
            "Nadine Parker",
            "Serena Norris",
            "Clarke Ray",
            "Kavan Hoover",
            "Emmanuella Goulding",
            "Alishba Aguirre",
            "Lia Washington",
            "Fintan Clemons",
            "Annabel Best",
            "Levison Rees",
            "Iosif Cousins",
            "Billie-Jo Acosta",
            "Kiran Walters",
            "Catrin Knott",
            "Lily-Rose Meyers",
            "Garin Ryan",
            "Rosemary Whitworth",
            "Sakina Bauer",
            "Timur Beard",
            "Theresa Campos",
            "Alfie Milne",
            "Cem Lozano",
            "Bertha Wall",
            "Eesha Mohammed",
            "Hamish Lowry",
            "Kaif Kendall",
            "Mylo Ponce",
            "Aleena Traynor",
            "Kenan Bradford",
            "Helen Ritter",
            "Aiden Olson",
            "Amiyah Holcomb",
            "Aislinn Atkins",
            "Amina Medrano",
            "Darlene Lindsey",
            "Ralphie Betts",
            "Isabel Charles",
            "Alaina Reyna",
            "Olivia Suarez",
            "Gia Freeman",
            "Matilda Mcintyre",
            "Emilija Jefferson",
            "Alayna Kelly",
            "Kaycee Berry",
            "Jerry Cresswell",
            "Fabian Metcalfe",
            "Chace Morrison",
            "Saskia Manning",
            "Theia Randall",
        };
    }
}
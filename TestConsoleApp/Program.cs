// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using OpenAISharp;
using OpenAISharp.API;
using System.Globalization;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;


OpenAIConfiguration.Load("appsettings.json");
Console.WriteLine($"{OpenAISettings.OrganizationID}, {OpenAISettings.ApiKey}");
Models _result = await Models.List(true);
Console.WriteLine(_result.ToString());
Model result = await Models.Get("text-davinci-003", true);
Console.WriteLine( result );
Console.WriteLine("Done");
Console.ReadLine(); 

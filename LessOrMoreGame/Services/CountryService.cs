using LessOrMoreGame.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace LessOrMoreGame.wwwroot.Services
{
    public class CountryService : ICountryService
    {
        private IWebHostEnvironment WebHostEnvironment { get; }

        public CountryService(IWebHostEnvironment webHostEnvironment) 
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName 
        { 
            get{ return Path.Combine(WebHostEnvironment.WebRootPath, "Data", "Countries.json"); }
        }

        private IEnumerable<Country> GetCountries()
        {
            StreamReader _Reader = File.OpenText(JsonFileName);
            IEnumerable<Country>? _Countries = new List<Country>();

            _Countries = JsonSerializer.Deserialize<Country[]>(_Reader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

            return _Countries ?? new List<Country>();
        }

        public StartGameModel StartGame()
        {
            StartGameModel _GameModel = new StartGameModel();
            Random _Random = new Random();
            IEnumerable<Country>? AllCountries = GetCountries();
            
            List<Country> _Country = AllCountries.OrderBy(country => _Random.Next()).Take(2).ToList();
            _GameModel.Country1 = _Country[0];
            _GameModel.Country2 = _Country[1];

            return _GameModel;
        }
    }
}

using LessOrMoreGame.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

        private IEnumerable<CountryModel> GetCountries()
        {
            StreamReader _Reader = File.OpenText(JsonFileName);
            IEnumerable<CountryModel>? _Countries = new List<CountryModel>();

            _Countries = JsonSerializer.Deserialize<CountryModel[]>(_Reader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });

            return _Countries ?? new List<CountryModel>();
        }

        private CountryModel GetCountryById(int id)
        {
            IEnumerable<CountryModel>? AllCountries = GetCountries();

            return AllCountries.FirstOrDefault(country => country.ID == id);
        }

        public StartGameModel StartGame()
        {
            StartGameModel _GameModel = new StartGameModel();
            Random _Random = new Random();
            IEnumerable<CountryModel>? AllCountries = GetCountries();
            
            List<CountryModel> _Country = AllCountries.OrderBy(country => _Random.Next()).Take(2).ToList();
            _GameModel.Country1 = _Country[0];
            _GameModel.Country2 = _Country[1];

            return _GameModel;
        }

        public GuessModel Guess(CheckAnswerModel checkAnswerModel)
        {
            GuessModel _Guess = new GuessModel();

            CountryModel _SelectedCountry = GetCountryById(checkAnswerModel.CountrySelectedID);
            CountryModel _OtherCountry = GetCountryById(checkAnswerModel.OtherCountryID);

            _Guess.IsCorrect = _SelectedCountry.Population > _OtherCountry.Population;

            IEnumerable<CountryModel> availableCountries = GetCountries()
                .Where(country => country.ID != _SelectedCountry.ID && country.ID != _OtherCountry.ID);

            Random _Random = new Random();
            _Guess.NewCountry = availableCountries
                .OrderBy(country => _Random.Next())
                .FirstOrDefault();

            return _Guess;
        }
    }
}

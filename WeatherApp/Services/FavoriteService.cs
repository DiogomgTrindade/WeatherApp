﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Entities;

namespace WeatherApp.Services
{
    public class FavoriteService
    {
        private readonly string FavouritesKey = "FavoriteCities";

        public async Task<List<City>> GetFavoriteCitiesAsync()
        {
            var favoritesJson = await SecureStorage.GetAsync(FavouritesKey);
            return string.IsNullOrEmpty(favoritesJson)
                ? new List<City>() :
                System.Text.Json.JsonSerializer.Deserialize<List<City>>(favoritesJson);
        }

        public async Task AddToFavoritesAsync(City city)
        {
            var favorites = await GetFavoriteCitiesAsync();
            if(!favorites.Any(c => c.Name == city.Name))
            {
                city.isFavorite = true;
                favorites.Add(city);
                await SaveFavoritesAsync(favorites);
            }
        }

        private async Task SaveFavoritesAsync(List<City> favorites)
        {
            var favoritesJson = System.Text.Json.JsonSerializer.Serialize(favorites);
            await SecureStorage.SetAsync(FavouritesKey, favoritesJson);
            Preferences.Set(FavouritesKey, favoritesJson);
        }

        public async Task RemoveFromFavoritesAsync(City city)
        {
            var favorites = await GetFavoriteCitiesAsync();
            var favoriteToRemove = favorites.FirstOrDefault(c => c.Name == city.Name);
            if (favoriteToRemove != null)
            {
                city.isFavorite = false;
                favorites.Remove(favoriteToRemove);
                await SaveFavoritesAsync(favorites);
            }
        }

        public async Task<bool> IsFavoriteAsync(City city)
        {
            var favorites = await GetFavoriteCitiesAsync();
            return favorites.Any(c => c.Name == city.Name);
        }

    }
}

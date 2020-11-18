using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace euro_diffusion
{
    class Grid
    {
        private const int Length = 10;
        private const int Width = 10;

        private City[,] Cities = new City[Length + 2, Width + 2];

        public List<Country> Countries;

        public Grid(List<Country> countries)
        {
            InitializeCities();
            Countries = countries;
            for (int i = 0; i < countries.Count; i++)
            {
                AddCountryToGrid(Countries[i], i);
            }

            SetNeighbours();
        }

        private void InitializeCities()
        {
            for (int i = 1; i < Length + 2; i++)
            {
                for (int y = 1; y < Width + 2; y++)
                {
                    Cities[i, y] = null;
                }
            }
        }

        private void AddCountryToGrid(Country country, int index)
        {
            int xl = country.XL;
            int yl = country.YL;
            int xh = country.XH;
            int yh = country.YH;

            for (var i = xl; i <= xh; i++)
            {
                for (var j = yl; j <= yh; j++)
                {
                    if (Cities[i, j] != null)
                        throw new ArgumentException("countries intercept");
                    Cities[i, j] = new City(Countries.Count, index);
                    country.Cities.Add(Cities[i, j]);
                }
            }
        }

        private void SetNeighbours()
        {
            for (int i = 1; i < Length + 1; i++)
            {
                for (int y = 1; y < Width + 1; y++)
                {
                    var curCity = Cities[i, y];
                    if (curCity == null)
                        continue;

                    curCity.AddNeighbour(Cities[i, y + 1]);
                    curCity.AddNeighbour(Cities[i, y - 1]);
                    curCity.AddNeighbour(Cities[i + 1, y]);
                    curCity.AddNeighbour(Cities[i - 1, y]);
                }
            }
        }

        private void DoTick()
        {
            foreach (var city in Cities)
            {
                city?.PayToNeighbours();
            }
            foreach (var city in Cities)
            {
                city?.AddCachedMoney();
            }
        }

        public string SolveCase()
        {
            string result = "";

            int tick = 0;

            var countriesToCheck = new List<Country>();
            countriesToCheck.AddRange(Countries);

            while (countriesToCheck.Any())
            {
                var completedCountries = new List<Country>();
                foreach (var country in countriesToCheck)
                {
                    if (country.IsCompleted)
                    {
                        result += $"{country.Name} {tick} \n";
                        completedCountries.Add(country);
                    }
                }

                foreach (var completedCountry in completedCountries)
                {
                    countriesToCheck.Remove(completedCountry);
                }

                if (countriesToCheck.Any())
                {
                    DoTick();
                    tick++;
                }
            }

            return result;
        }
    }
}

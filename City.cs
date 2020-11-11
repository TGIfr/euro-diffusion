using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace euro_diffusion
{
    class City
    {
        private const int StartMoney = 1000000;
        private const int RepresentativePortionDiv = 1000;
        private bool _isCompleted;

        public Dictionary<int, int> Money = new Dictionary<int, int>();
        public Dictionary<int, int> MoneyCache = new Dictionary<int, int>();
        public readonly int MoneyCountries;
        public List<City> Neighbours = new List<City>();

        public bool IsCompleted {
            get
            {
                if (!_isCompleted && Money.Keys.Count == MoneyCountries)
                    _isCompleted = true;

                return _isCompleted;
            }
        }

        public City(int numberOfCountries, int ownCountry)
        {
            MoneyCountries = numberOfCountries;
            Money[ownCountry] = StartMoney;
        }

        public void AddNeighbour(City neighbour)
        {
            if(neighbour != null)
                Neighbours.Add(neighbour);
        }

        public void AddMoney(int amount, int country)
        {
            if(MoneyCache.ContainsKey(country))
                MoneyCache[country] += amount;
            else
                MoneyCache[country] = amount;
        }

        public void PayToNeighbours()
        {
            foreach (var neighbour in Neighbours)
            {
                var keys = Money.Keys.ToList();
                foreach (var key in keys)
                {
                    if (Money[key] > RepresentativePortionDiv)
                    {
                        var moneyToGive = Money[key] / RepresentativePortionDiv;
                        neighbour.AddMoney(moneyToGive, key);
                        Money[key] -= moneyToGive;
                    }
                }
            }
        }

        public void AddCachedMoney()
        {
            foreach (var i in MoneyCache)
            {
                if (Money.ContainsKey(i.Key))
                    Money[i.Key] += i.Value;
                else
                    Money[i.Key] = i.Value;
            }
            MoneyCache.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace euro_diffusion
{
    class Country
    {
        private const int MaxNameLength = 25;
        private bool _isCompleted;
        private const int MaxSize = 10;
        private const int MinSise = 1;
        public bool IsCompleted
        {
            get
            {
                if (!_isCompleted && Cities.All(x => x.IsCompleted))
                    _isCompleted = true;

                return _isCompleted;
            }
        }

        public int XL { get; set; }
        public int YL { get; set; }
        public int XH { get; set; }
        public int YH { get; set; }
        public string Name { get; set; }    

        public List<City> Cities = new List<City>();

        public Country(string name, int xl, int yl, int xh, int yh)
        {
            if(name.Length > MaxNameLength)
                throw new ArgumentException("country name should be 25 chars at max");
            if(xl > xh || yl > yh)
                throw new ArgumentException("invalid country coordinates");
            if(new[] {xl, yl, xh, yh}.ToList().Any(x => x < MinSise || x > MaxSize))
                throw new ArgumentException("invalid range country coordinates");
            XL = xl;
            XH = xh;
            YH = yh;
            YL = yl;
            Name = name;
        }
    }
}

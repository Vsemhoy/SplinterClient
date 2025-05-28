using SplinterCoreNTS.Entity.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplinterCoreNTS.Entity
{
    public class FlatRoom : IRoomAcParams
    {
        public double NoiseLevel { get; set; }

        public string id { get; set; }
        public string Name { get; set; }
        public double Height { get; set; } // in millimeters


        public FlatRoom()
        {
            this.id = Guid.NewGuid().ToString("N");
            this.Name = "New space";
            this.Height = 2.7;
        }
    }
}

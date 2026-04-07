using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Model
{
  
    [CollectionDataContract]
    public class CityList : List<City>
    {
        public CityList() { }

        public CityList(IEnumerable<City> list) : base(list) { }

     
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Model
{
    [CollectionDataContract]
    public class UserList : List<User>
    {
        public UserList() { }

        public UserList(IEnumerable<User> list) : base(list) { }     
    }
}

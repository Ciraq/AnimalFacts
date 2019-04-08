using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsFact
{
    public class Item
    {
        public string _AnimmalName;
        public int _ID;

        public Item(int id, string name)
        {
            _AnimmalName = name;
            _ID = id;
        }

        public string AnimalName
        {
            get { return _AnimmalName.ToLower(); }
            set { _AnimmalName = value; }
        }
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
    }
}

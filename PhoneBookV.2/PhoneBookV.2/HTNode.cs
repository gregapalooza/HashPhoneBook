using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookV._2
{
    /*
        This is a node class
        used for collisions
        used as a linked list
    */
    class HTNode
    {
        private int key;
        private Person data;
        private HTNode next;

        public int Key { get => key; set => key = value; }
        internal Person Data { get => data; set => data = value; }
        internal HTNode Next { get => next; set => next = value; }

        public HTNode() { }

        public HTNode(int key, Person data)
        {
            Key = key;
            Data = data;
            Next = null;    // Assigned when needed
        }

    }
}

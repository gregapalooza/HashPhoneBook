using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookV._2
{
    class Person
    {
        /*
            Person class can build an
            instance of the data that 
            needs to be held in node
        */
        private string lname;
        private string number;

        public string Lname { get => lname; set => lname = value; }
        public string Number { get => number; set => number = value; }

        public Person() { }

        public Person(string lname, string number)
        {
            Lname = lname;
            Number = number;
        }

        public override string ToString()
        {
            string info = Lname + ": " + Number;
            return info;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*
 * Name: Greg Vaggalis
 * Date: 4/19/2021
*/

namespace PhoneBookV._2
{
    class Program
    {
        // Class-level variables:
        Person thisguy = new Person();
        // Person[] phbk = new Person[100];
        HTNode htnode;
        HTNode[] hashtbl;
        const int size = 101; // Size of array also a Prime Number
        string rspn = "Y";

        static void Main(string[] args)
        {
            Program p = new Program();
            p.SetTable();
            p.ReadFile();
            // p.PrintArry(p.hashtbl);
            p.UserSearch();
            Console.ReadLine();
        }

        /// <summary>
        /// Method used to search for names in the 
        /// phonebook. Uses class-level rspn var in 
        /// do-while loop
        /// </summary>
        public void UserSearch()
        {
            do
            {
                Console.WriteLine("Enter the last name of the person you are searching: ");
                string lname = Console.ReadLine();
                int key = HashFunction(lname);
                SearchTable(key, lname);

                Console.WriteLine("Do you want to search again?Y/N");
                string ans = Console.ReadLine().ToUpper();
                rspn = ans;
            } while (rspn != "N");

        }

        /// <summary>
        /// Method used to search the hash table array 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="lname"></param>
        public void SearchTable(int key, string lname)
        {
            HTNode lastnode = hashtbl[key]; // Attach array index to seperate var so as to not delete it from array when looping through

            if(lastnode != null)    // Search index if not null
            {
                while(lastnode.Next != null)    // loop through linked list
                {
                    // if name is found
                    if (lname == lastnode.Data.Lname)
                    {
                        Console.WriteLine(lastnode.Data.ToString());
                        lastnode = lastnode.Next;
                    }
                    else
                    {
                        lastnode = lastnode.Next;
                    }
                }
                if (lastnode.Next == null && lname == lastnode.Data.Lname)  // Checks last item in the linked list to determine if it matches
                {
                    Console.WriteLine(lastnode.Data.ToString());
                }
                else
                {
                    Console.WriteLine("Could not find the person.");
                }

            }
            else
            {
                Console.WriteLine("That person is not found.");
            }
        }

        /// <summary>
        /// Creates a Hash key based off 
        /// last name totals and index pos
        /// % by array size
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int HashFunction(string name)
        {
            int hashkey = 0;
            for (int i = 0; i < name.Length; i++)
            {
                hashkey += ((int)name[i]) * (i + 1);
            }

            hashkey %= size;

            return hashkey;
        }

        /// <summary>
        /// Sets up Hash Table
        /// all array entries null
        /// </summary>
        public void SetTable()
        {
            hashtbl = new HTNode[size];
            for (int i = 0; i < size; i++)
            {
                hashtbl[i] = null;
            }
        }

        /// <summary>
        /// Creates an instance of a HTNode class
        /// </summary>
        /// <param name="p"> Person is data the node will hold </param>
        /// <returns> New node made </returns>
        public HTNode CreateNodeInstance(Person p)
        {
            HTNode newnode;
            string name = p.Lname;
            int hashkey = 0;
            for(int i = 0; i < name.Length; i++)
            {
                hashkey += ((int)name[i]) * (i + 1);    // Loop through string array and add ASCII chars
            }

            hashkey %= size;    // Get the modulus of hashkey
            newnode = new HTNode(hashkey, p);

            return newnode;
        }

        /// <summary>
        /// method to insert node into hash table
        /// </summary>
        /// <param name="node"></param>
        public void InsertToHashTable(HTNode node)
        {
            int hash = node.Key;

            if(hashtbl[hash] == null)
            {
                hashtbl[hash] = node;   // If nothing is there add to table 
            }
            else
            {
                HTNode lastNode = hashtbl[hash];

                while(lastNode.Next != null)    // loop through nodes until empty space is found
                {
                    lastNode = lastNode.Next;
                }

                lastNode.Next = node;   // Add to free space found
            }
        }

        /// <summary>
        /// Method to open and read file and
        /// creates person then node instances
        /// and then adds the node to hash table
        /// </summary>
        public void ReadFile()
        {
            // int counter = 0;

            try
            {
                using (StreamReader sr = new StreamReader("PhoneBook.txt")) // File is located in the bin >> Debug of project.
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        // Console.WriteLine(line);
                        CreatePersonInstance(line);             // Create Person
                        htnode = CreateNodeInstance(thisguy);   // Create node
                        InsertToHashTable(htnode);              // Insert node
                        // phbk[counter] = thisguy;
                        // counter++;
                    }
                }
            }
            catch (Exception e)
            {
                // If you can't open the file this will display
                Console.WriteLine("The file could not be read");
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// Read info from  .txt file and creates Person instance
        /// </summary>
        /// <param name="info"> line from file passed to method </param>
        private void CreatePersonInstance(string info)
        {
            string[] fields = info.Split(',');
            thisguy = new Person(fields[0],
                                 fields[1]);
        }

        /*
        public void PrintArry(HTNode[] book)
        {
           for (int i = 0; i < book.Length; i++)
            {
                if(book[i] != null)
                {
                    if (book[i].Next != null)
                    {
                        while(book[i].Next != null)
                        {
                            Console.WriteLine(book[i].Data.ToString());
                            book[i] = book[i].Next;
                        }
                        Console.WriteLine(book[i].Data.ToString());
                    }
                    else
                    {
                        Console.WriteLine(book[i].Data.ToString());
                    }
                }
                else
                {
                    continue;
                }
            }
        }
        */
    }
}

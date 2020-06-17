using System;
using System.Collections.Generic; 

namespace AdvancedAlgorithmsFinalProjectExperiments
{
    class Program
    {
        //Track found solutions for brute force
        public List<Solution> KnownSolutions = new List<Solution>();
        //Create and populate test list of items
        public static List<Item> Things = new List<Item>()
        {
            //    Value, Weight
            new Item(10, 3),
            new Item(4, 4),
            new Item(15, 6),
            new Item(15, 10),
            new Item(17, 12),
            new Item(9, 15)
        }; 

        static int max(int valueA, int valueB)
        {
            return (valueA > valueB) ? valueA : valueB;
        }

        //recursive method for an optimal this isn't for counting solutions, we need dynamic programming for our actual solution
        static int knapSack(int maxCap, List<Item> items, int n)
        {
            //no items in list or capacity is 0 end now
            if (n == 0 || maxCap == 0)
                return 0;

            // If weight is > capacity, 
            // cant be included in the optimal solution 
            if (items[n - 1].weight > maxCap)
                return knapSack(maxCap, items, n - 1);

            // either spot n item included or it's not included 
            else
                return max(
                    items[n - 1].value + knapSack(
                                     maxCap - items[n - 1].weight, items, n - 1),
                    knapSack(maxCap, items, n - 1));
        }
        //Brute force returns the number of items in list Solutions
        public int BruteForceMethod(int maxCap, int n)
        {
            KnownSolutions.Clear(); 
            for(int i =0; i <= n; i++)
            {
                for(int cnt = 0; cnt <= maxCap; cnt++)
                {

                }
            }
            return KnownSolutions.Count; 
        }

        //MCMC
        public int MCMCMethod()
        {
            KnownSolutions.Clear();


            return KnownSolutions.Count;
        }

        //Check to see if a new solution is an existing solution 
        //If we consider 3, 4, 5 and 4, 3, 5 different this works, but if they're the same,
        //this should be changed to ulitze SequenceEqual
        public bool checkSolution(List<Item> test, List<Solution> existing)
        {
            foreach(Solution sol in existing)
            {
                for (int i =0; i < sol.Items.Count; i++)
                {
                    if (sol.Items[i].value == test[i].value)
                        return false; 
                }
            }
            return true; 
        }

       static void Main(string[] args)
        {
            Program program = new Program();
            int maxCapacity = 30;
            int n = Things.Count;
            Console.WriteLine(knapSack(maxCapacity, Things, n));
            Console.WriteLine("Brute force found ", program.BruteForceMethod(maxCapacity, n), "number of solutions");
            Console.WriteLine("MCMC ", program.MCMCMethod(), "number of solutions");

        }
    }



    public class Solution
    {
        public List<Item> Items;
        public int totalWeight; 

    }

    public class Item
    {
       public int value;
       public int weight; 

       public Item(int v, int w)
        {
            value = v;
            weight = w; 
        }
    }
}

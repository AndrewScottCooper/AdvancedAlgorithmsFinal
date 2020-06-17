using System;
using System.Collections.Generic;
using System.Linq;


namespace AdvancedAlgorithmsFinalProjectExperiments
{
    class Program
    {
        //Track found solutions for brute force
        public List<Solution> KnownSolutions = new List<Solution>();
        public Random rand = new Random();
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
            //Total possible weight of all items
            for (int i = 0; i < 5000; i++)
            {
                int itemTW = MaxWeight(Things);
            }

            int omega = 1; 
           //second for loop
              //omega = omega * p[i]


            //return 1/omega
            return KnownSolutions.Count;
        }

        //Compare if new solution already exist, if it doesn't then its new and returns true
        public bool checkSolution(List<Item> test, List<Solution> existing)
        {
           foreach(Solution sol in existing)
            {
                if (sol.Items.SequenceEqual(test))
                    return false;
            }
            return true; 
        }

        public int MaxWeight(List<Item> items)
        {
            int totalWeight = 0;
            foreach (Item I in items)
                totalWeight += I.weight;

            return totalWeight;
        }

        public void MCMCRandomWalk()
        {
            //
            int[,] randomWalkMatrix = new int[100, Things.Count];

            for(int i = 0; i < 10000; i++)
            {
                for (int row = 0; row < 100; row++)
                {
                    //things.count + 1 because last value is non inclusive
                    int col = rand.Next(0, Things.Count + 1);
                    randomWalkMatrix[row, col] = 1; 
                    
                }
            }



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

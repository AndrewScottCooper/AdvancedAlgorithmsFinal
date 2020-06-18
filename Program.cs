using System;
using System.Collections.Generic;
using System.Linq;


namespace AdvancedAlgorithmsFinalProjectExperiments
{
    class Program
    {
        //Track found solutions for brute force
        public List<Solution> KnownSolutions;
        public Random rand = new Random();
        


        public Program()
        {
            KnownSolutions = new List<Solution>();

        }
        //Create and populate test list of items
        public static List<Item> Things = new List<Item>()
        {
            //    Value, Weight
            new Item(10, 3),
            new Item(4, 4),
            new Item(15, 6),
            new Item(15, 10),
            new Item(17, 12),
            new Item(9, 15),
            new Item(10, 20)
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
        //This doesn't find optimal solutions it just finds solutions
        public int BruteForceMethod(int maxCap, int n)
        {
            int randomIndex;
            int attempts;

            KnownSolutions.Clear();
          //large number to sample possible solutions, more items, higher this should be
            for (int i =0; i <= 10000; i++)
            {
                Solution tempSol = new Solution();
                //random amount of times to attempt to solve knapsack
                attempts = rand.Next(0, n + 100);
                for (int cnt = 0; cnt <= attempts; cnt++)
                {
                  randomIndex = rand.Next(1, Things.Count);
                    //if list is empty add frist item to the list
                    if(tempSol == null){
                       tempSol.Items.Add(Things[randomIndex]);
                    }
                    //If it isnt in the list and if adding its weight is less than max then it works
                    else if(!tempSol.Items.Contains(Things[randomIndex]) && (tempSol.totalWeight + Things[randomIndex].weight) <= maxCap) {
                        tempSol.Items.Add(Things[randomIndex]);
                        tempSol.totalWeight += Things[randomIndex].weight; 
                    }                 
                }
                //check the solution doesn't already exist in some form
                if(checkSolution(tempSol.Items, KnownSolutions))
                {
                    KnownSolutions.Add(new Solution(tempSol.Items, tempSol.totalWeight)); 
                }
               
            }
            Console.Write("Brute Force found ");
            Console.Write(KnownSolutions.Count.ToString(), "number of solutions"); 
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

        public void MCMCRandomWalk(int maxCap)
        {

            KnownSolutions.Clear();
            for(int i = 0; i < 10000; i++)
            {
                int[,] randomWalkMatrix = new int[100, Things.Count];
                Solution tempSol = new Solution();

                for (int row = 0; row < 100; row++)
                {
                    //things.count + 1 because last value is non inclusive
                    int col = rand.Next(0, Things.Count);
                    
                    int prob = rand.Next(1, 2*Things.Count); 
                    //if prob 1/ 2*Things.Count then flip else leave it alone 
                    if(prob == 1)
                    {
                        randomWalkMatrix[row, col] = 1;
                    }                     
               }
                //Nested for loop, work through the random matrix to build a solution
                for(int r = 0; r < 100; r++)
                {
                    for(int c = 0; c< Things.Count; c++)
                    {
                        //if a bit was flipped 
                        if(randomWalkMatrix[r,c] == 1)
                        {
                            //if the list is empty add the item to with the corrisponding column index
                          if(tempSol.Items == null)
                            {
                                tempSol.Items.Add(Things[c]); 
                            }
                          //otherwise, check we dont already have the item in the list, and that the weight would be less than or the limit 
                          //by adding the item to the list 
                          else if (!tempSol.Items.Contains(Things[c]) && (tempSol.totalWeight + Things[c].weight) <= maxCap)
                            {
                                tempSol.Items.Add(Things[c]);
                                tempSol.totalWeight += Things[c].weight; 
                            }
                        }
                    }
                }
                //if this solution doesn't already exist in some way add it to known solutions
                if (checkSolution(tempSol.Items, KnownSolutions))
                {
                    KnownSolutions.Add(new Solution(tempSol.Items, tempSol.totalWeight));
                   
                }
            }
          //  for(int j= 0; j < 100; j++)
          //  {
             //Check  Y[j] * w <= Wi-1
          //  }
            //Return Ys
            Console.WriteLine(KnownSolutions.Count); 
        }

       static void Main(string[] args)
        {
            Program program = new Program();
            int maxCapacity = 50;
            int n = Things.Count;
            Console.WriteLine(knapSack(maxCapacity, Things, n));
            program.BruteForceMethod(maxCapacity, n);
            Console.WriteLine("\n MCMC ", program.MCMCMethod(), "number of solutions");
            program.MCMCRandomWalk(maxCapacity);
        }
    }



    public class Solution
    {
        public List<Item> Items;
        public int totalWeight; 

        public Solution()
        {
            Items = new List<Item>();
            totalWeight = 0; 
        }
        public Solution( List<Item> items, int TW)
        {
            Items = items;
            totalWeight = TW;
        }

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

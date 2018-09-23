using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Permutations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("数字(用\",\"隔开):");
            string[] nums = Console.ReadLine().Split(',');
            int[] IntArr = Array.ConvertAll<string, int>(nums, s => int.Parse(s));
            Console.Write("个数:");
            int num = Convert.ToInt32(Console.ReadLine());
            int i = 0, j = 0;
            Console.WriteLine("\n组合");
            List<int[]> ListCombination1 = PermutationAndCombination<int>.GetCombination(IntArr, num); //求全部的3-3组合
            foreach (int[] arr in ListCombination1)
            {
                foreach (int item in arr)
                {
                    Console.Write(item + " ");
                }
                i++;
                Console.WriteLine("");
            }
            Console.WriteLine("共" + i + "种");
            Console.WriteLine("\n排列");
            List<int[]> ListCombination2 = PermutationAndCombination<int>.GetPermutation(IntArr, num); //求全部的5取3排列
            foreach (int[] arr in ListCombination2)
            {
                foreach (int item in arr)
                {
                    Console.Write(item + " ");
                }
                j++;
                Console.WriteLine("");
            }
            Console.WriteLine("共" + j + "种");
            Console.ReadKey();
        }
    }
}

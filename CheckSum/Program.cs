using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace CheckSum
{
    class Program
    {
        private static int[] FillArray(int numCount) { 

            List<int> num = new();
            if (numCount > 1)
            {
                Random rnd = new();
                for (int i = 0; i < numCount; i++)
                {
                    int numItem = rnd.Next(numCount * 2) - numCount;
                    num.Add(numItem);
                }
                num.Sort();
                num = num.Distinct().ToList();
                int last = num[^1];
                for (int i = num.Count; i < numCount; i++)
                {
                    last += rnd.Next(1, 5);
                    num.Add(last);
                }
            }
            return num.ToArray();
        }

        public static List<int> NaiveTrySum(int[] num, int k)
        {
            int first = 0;
            int second = 0;

            for (int i = 0; i < num.Length / 2; i++)
            {
                first = num[i];
                second = num.Where(n => n == (k - first)).FirstOrDefault();
                if (second > 0)
                {
                    break;
                }
            }
            var res = new List<int>();
            if (second > 0)
            {
                res.Add(first);
                res.Add(second);
            }
            return res;
        }

        public static List<int> NonNaiveTrySum(int[] num, int k)
        {
            int first = 0;
            int second = 0;
            for (int i = 0; i < num.Length / 2; i++)
            {
                first = num[i];
                int found = k - first;
                for (int j = num.Length - 1; j > 1; j--)
                {
                    if (found == num[j])
                    {
                        second = num[j];
                        break;
                    }
                    else  if (found > num[j])
                    {
                        break;
                    }
                }
                if (second > 0)
                {
                    break;
                }
            }
            var res = new List<int>();
            if (second > 0)
            {
                res.Add(first);
                res.Add(second);
            }
            return res;
        }

        static void Main()
        {
            int numCount = Convert.ToInt32(Console.ReadLine().Trim());
            if (numCount > 1)
            {
                var onum = FillArray(numCount);

                Random rnd = new();
                int kseed = onum[^1];
                int k = rnd.Next(kseed);
                var nums = NonNaiveTrySum(onum, k);
                Console.WriteLine($"k = {k}");
                if (nums.Count > 0)
                {
                    Console.WriteLine($"first = {nums[0]}");
                    Console.WriteLine($"second = {nums[1]}");
                }
                else 
                {
                    Console.WriteLine("not fount");
                }
            }
            else
            {
                Console.WriteLine("incorrect param");
            }
        }
    }
}

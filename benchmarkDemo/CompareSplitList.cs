using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace benchmarkDemo
{

    public class CompareSplitList
    {      

        public List<int> GetData()
        {
            return Enumerable.Range(1, 10035).ToList();
        }

        [ParamsSource(nameof(GetData))]
        public List<int> lists { get; set; } = new List<int>();

        [Benchmark]
        public void EachItems()
        {
            const int itemCount = 100;
            int InsertCount = lists.Count();
            int eachCount = InsertCount / itemCount;
            int otherCount = InsertCount % itemCount;
            if (eachCount > 0)
            {
                for (int n = 0; n < eachCount; n++)
                {
                    var temps = lists.Skip(n * itemCount).Take(itemCount).ToList();
                }
                if (otherCount > 0)
                {
                    var temps = lists.Skip(eachCount * itemCount).Take(otherCount).ToList();
                }
            }
        }

        [Benchmark]
        public void ForeachList()
        {
            List<int> temps = new List<int>();
            foreach (var item in lists)
            {
                temps.Add(item);
                if (temps.Count() == 1000)
                {     
                    //save
                    temps = new List<int>();
                }
            } 
            //save
        }
    }
}

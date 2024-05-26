using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benchmarkDemo
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class demo1
    {
        private readonly byte[] _data = new byte[1000];

        [Benchmark]
        public void AllocateMemory()
        {
            var newData = new byte[1000];
            for (int i = 0; i < _data.Length; i++)
            {
                newData[i] = _data[i];
            }
        }
    }
}


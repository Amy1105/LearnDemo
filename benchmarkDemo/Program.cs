﻿// See https://aka.ms/new-console-template for more information
using benchmarkDemo;
using BenchmarkDotNet.Running;

//var summary = BenchmarkRunner.Run<demo1>();

//BenchmarkRunner.Run<VerifyDecimalPlaces>();

var summary = BenchmarkRunner.Run<CompareSplitList>();


//object?  n = null;
//Console.WriteLine(n.ToString());
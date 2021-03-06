﻿#region Copyright notice and license

// Copyright 2019 The gRPC Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;

namespace Grpc.AspNetCore.Microbenchmarks
{
    public class Program
    {
#if !PROFILE
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
#else
        // Profiling option. This will call methods explicitly, in-process
        static async Task Main(string[] args)
        {
            UnaryServerCallHandlerBenchmark benchmark = new UnaryServerCallHandlerBenchmark();
            benchmark.GlobalSetup();
            for (int i = 0; i < 100; i++)
            {
                await benchmark.HandleCallAsync();
            }

            Console.WriteLine("Press any key to start.");
            Console.ReadKey();
            for (int i = 0; i < 1; i++)
            {
                await benchmark.HandleCallAsync();
            }

            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadKey();
        }
#endif
    }
}

﻿#region using

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Benchmarking.Util;

#endregion

namespace Benchmarking.Arithmetic
{
	internal class Integer : Benchmark
	{
		private readonly uint LENGTH = 10000000;

		private byte randomByte;
		private int randomInt;
		private long randomLong;
		private short randomShort;
		private byte[] resultByteArray;
		private int[] resultIntArray;
		private long[] resultLongArray;
		private short[] resultShortArray;

		public Integer(Options options) : base(options)
		{
			LENGTH *= BenchmarkRater.ScaleVolume(options.Threads);
		}

		[MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
		public override void Run()
		{
			var tasks = new Task[options.Threads];

			for (var i = 0; i < options.Threads; i++)
			{
				var i1 = i;
				tasks[i] = ThreadAffinity.RunAffinity(1uL << i, () =>
				{
					// LOAD
					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultByteArray[j] = randomByte;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultShortArray[j] = randomShort;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultIntArray[j] = randomInt;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultLongArray[j] = randomLong;
					}

					// ADD

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultByteArray[j] += randomByte;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultShortArray[j] += randomShort;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultIntArray[j] += randomInt;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultLongArray[j] += randomLong;
					}

					// SUBTRACT

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultByteArray[j] -= randomByte;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultShortArray[j] -= randomShort;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultIntArray[j] -= randomInt;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultLongArray[j] -= randomLong;
					}

					// MULTIPLY

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultByteArray[j] *= randomByte;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultShortArray[j] *= randomShort;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultIntArray[j] *= randomInt;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultLongArray[j] *= randomLong;
					}

					// DIVIDE

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultByteArray[j] /= randomByte;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultShortArray[j] /= randomShort;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultIntArray[j] /= randomInt;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultLongArray[j] /= randomLong;
					}

					// MODULO

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultByteArray[j] %= randomByte;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultShortArray[j] %= randomShort;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultIntArray[j] %= randomInt;
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultLongArray[j] %= randomLong;
					}

					// VARIOUS

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultByteArray[j] = Math.Max(randomByte, resultByteArray[j]);
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultShortArray[j] = Math.Min(randomShort, resultShortArray[j]);
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultIntArray[j] = (int) Math.BigMul(randomInt, randomInt);
					}

					for (var j = 0 + i1 * (LENGTH / options.Threads); j < LENGTH / options.Threads; j++)
					{
						resultLongArray[j] = Math.BigMul(randomInt, randomInt);
					}

					BenchmarkRunner.ReportProgress();
				});
			}

			Task.WaitAll(tasks);
		}

		public override double GetComparison()
		{
			switch (options.Threads)
			{
				case 1:
				{
					return 1292.0d;
				}
				default:
				{
					return 52.0d;
				}
			}
		}

		public override string GetDescription()
		{
			return "Integer arithmetic performance";
		}

		public override void Initialize()
		{
			var rand = new Random();

			randomInt = rand.Next();
			randomByte = (byte) rand.Next();
			randomLong = (long) int.MaxValue + rand.Next();
			randomShort = (short) rand.Next();

			resultByteArray = new byte[LENGTH];
			resultIntArray = new int[LENGTH];
			resultLongArray = new long[LENGTH];
			resultShortArray = new short[LENGTH];
		}

		public override string GetName()
		{
			return "arithmetic_int";
		}

		public override string[] GetCategories()
		{
			return new[] {"int", "arithmetic"};
		}

		public override double GetDataThroughput(double timeInMillis)
		{
			return sizeof(byte) * LENGTH * LENGTH * 6 / (timeInMillis / 1000)
			       + sizeof(short) * LENGTH * LENGTH * 6 / (timeInMillis / 1000)
			       + sizeof(int) * LENGTH * LENGTH * 6 / (timeInMillis / 1000)
			       + sizeof(long) * LENGTH * LENGTH * 6 / (timeInMillis / 1000);
		}
	}
}
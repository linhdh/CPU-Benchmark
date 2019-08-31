﻿#region using

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

#endregion

namespace Benchmarking.Arithmetic
{
	internal class Integer : Benchmark
	{
		// 200 "MB"
		private const int LENGTH = 200000000;
		private readonly byte[] resultByteArray = new byte[LENGTH];
		private readonly int[] resultIntArray = new int[LENGTH];
		private readonly long[] resultLongArray = new long[LENGTH];
		private readonly short[] resultShortArray = new short[LENGTH];

		private byte randomByte;
		private int randomInt;
		private long randomLong;
		private short randomShort;

		public Integer(Options options) : base(options)
		{
		}

		[MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
		public override void Run()
		{
			var tasks = new Task[options.Threads];

			for (var i = 0; i < options.Threads; i++)
			{
				var i1 = i;
				tasks[i] = Task.Run(() =>
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
				});
			}

			Task.WaitAll(tasks);
		}

		public override double GetReferenceValue()
		{
			if (options.Threads == 1)
			{
				return 27743.0d;
			}

			return 1083.0d;
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
		}
	}
}
﻿#region using

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Benchmarking.Util;
#if NETCOREAPP3_0
using System.Runtime.Intrinsics.X86;

#endif

#endregion

namespace Benchmarking.Extension
{
	internal class SSE : Benchmark
	{
		private readonly uint numberOfIterations = 20000000;
		private const float randomFloatingNumber = float.Epsilon;

		public SSE(Options options) : base(options)
		{
			numberOfIterations *= BenchmarkRater.ScaleVolume(options.Threads);
		}

		public override void Run()
		{
#if NETCOREAPP3_0
			if (!Sse.IsSupported)
			{
				return;
			}

			var threads = new Task[options.Threads];

			for (var i = 0; i < options.Threads; i++)
			{
				threads[i] = ThreadAffinity.RunAffinity(1uL << i, () =>
				{
					var randomFloatingSpan = new Span<float>(new[] {randomFloatingNumber});
					var dst = new Span<float>(new float[256]);

					var iterations = numberOfIterations / options.Threads;

					for (var j = 0; j < iterations; j++)
					{
						AddScalarU(randomFloatingSpan, dst);
						MultiplyScalarU(randomFloatingSpan, dst);
					}

					BenchmarkRunner.ReportProgress();
				});
			}

			Task.WaitAll(threads);
#else
			return;
#endif
		}

		public override ulong Stress(CancellationToken cancellationToken)
		{
#if NETCOREAPP3_0
			if (!Sse.IsSupported)
			{
				return 0uL;
			}

			var threads = new Task[options.Threads];
			var completed = 0uL;

			for (var i = 0; i < options.Threads; i++)
			{
				threads[i] = ThreadAffinity.RunAffinity(1uL << i, () =>
				{
					var threadCompleted = 0uL;
					var randomFloatingSpan = new Span<float>(new[] { randomFloatingNumber });
					var dst = new Span<float>(new float[256]);

					while (!cancellationToken.IsCancellationRequested)
					{
						AddScalarU(randomFloatingSpan, dst);
						MultiplyScalarU(randomFloatingSpan, dst);

						dst.Clear();
						threadCompleted++;
					}

					lock (threads)
					{
						completed += threadCompleted;
					}
				}, ThreadPriority.BelowNormal);
			}

			Task.WaitAll(threads);

			return completed / numberOfIterations;
#else
			return 0uL;
#endif
		}

		public override string GetDescription()
		{
			return "SSE benchmark of addition and multiplication on 256 floats (8192 bits)";
		}

		public override double GetComparison()
		{
			switch (options.Threads)
			{
				case 1:
				{
					return 1010.0d;
				}
				default:
				{
					return 200.0d;
				}
			}
		}

		public override string[] GetCategories()
		{
			return new[] {"extension", "float", "sse"};
		}

		public override double GetDataThroughput(double timeInMillis)
		{
			return sizeof(float) * 256 * numberOfIterations * 2 / (timeInMillis / 1000);
		}

#if NETCOREAPP3_0
		private unsafe void MultiplyScalarU(Span<float> scalar, Span<float> dst)
		{
			fixed (float* pdst = dst)
			fixed (float* psrc = scalar)
			{
				var pDstEnd = pdst + dst.Length;
				var pDstCurrent = pdst;

				var scalarVector128 = Sse.LoadScalarVector128(psrc);

				while (pDstCurrent < pDstEnd)
				{
					var dstVector = Sse.LoadVector128(pDstCurrent);
					dstVector = Sse.Multiply(dstVector, scalarVector128);
					Sse.Store(pDstCurrent, dstVector);

					pDstCurrent += 4;
				}
			}
		}

		private unsafe void AddScalarU(Span<float> scalar, Span<float> dst)
		{
			fixed (float* pdst = dst)
			fixed (float* psrc = scalar)
			{
				var pDstEnd = pdst + dst.Length;
				var pDstCurrent = pdst;

				var scalarVector128 = Sse.LoadScalarVector128(psrc);

				while (pDstCurrent < pDstEnd)
				{
					var dstVector = Sse.LoadVector128(pDstCurrent);
					dstVector = Sse.Add(dstVector, scalarVector128);
					Sse.Store(pDstCurrent, dstVector);

					pDstCurrent += 4;
				}
			}
		}
#endif
	}
}
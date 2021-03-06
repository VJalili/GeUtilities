﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Model;
using System;
using Xunit;

namespace Genometric.GeUtilities.Tests.Intervals.Parsers.Stats
{
    public class IntervalStats
    {
        private Peak[] CreatePeaks(int[] intersCoord)
        {
            var rtv = new Peak[intersCoord.Length / 2];
            for (int i = 0; i < intersCoord.Length; i += 2)
                rtv[i / 2] = new Peak(
                    left: intersCoord[i],
                    right: intersCoord[i + 1],
                    value: 100.0,
                    summit: intersCoord[i] + (intersCoord[i + 1] - intersCoord[i]) / 2,
                    name: "GeUtilities_" + i);
            return rtv;
        }

        [Theory]
        [InlineData(new int[0])]
        [InlineData(new int[] { 10, 20 })]
        [InlineData(new int[] { 10, 20, 30, 32 })]
        [InlineData(new int[] { 10, 20, 30, 32, 40, 80 })]
        public void TestCount(int[] intersCoord)
        {
            var stats = new GeUtilities.Intervals.Parsers.Model.BedStats();
            foreach (var peak in CreatePeaks(intersCoord))
                stats.Update(peak);

            Assert.True(stats.Count == intersCoord.Length / 2);
        }

        [Theory]
        [InlineData(new int[0], 0)]
        [InlineData(new int[] { 10, 20 }, 10)]
        [InlineData(new int[] { 10, 20, 30, 32 }, 10)]
        [InlineData(new int[] { 10, 20, 30, 32, 40, 80 }, 40)]
        public void TestWidthMax(int[] intersCoord, int maxWidth)
        {
            var stats = new GeUtilities.Intervals.Parsers.Model.BedStats();
            foreach (var peak in CreatePeaks(intersCoord))
                stats.Update(peak);

            Assert.True(stats.WidthMax == maxWidth);
        }

        [Theory]
        [InlineData(new int[0], uint.MaxValue)]
        [InlineData(new int[] { 10, 20 }, 10)]
        [InlineData(new int[] { 10, 20, 30, 32 }, 2)]
        [InlineData(new int[] { 10, 20, 30, 32, 40, 80 }, 2)]
        public void TestWidthMin(int[] intersCoord, uint minWidth)
        {
            var stats = new GeUtilities.Intervals.Parsers.Model.BedStats();
            foreach (var peak in CreatePeaks(intersCoord))
                stats.Update(peak);

            Assert.True(stats.WidthMin == minWidth);
        }

        [Theory]
        [InlineData(new int[0], 0)]
        [InlineData(new int[] { 10, 20 }, 10)]
        [InlineData(new int[] { 10, 20, 30, 32 }, 6)]
        [InlineData(new int[] { 10, 20, 30, 32, 40, 80 }, 17.333)]
        public void TestWidthMean(int[] intersCoord, double mean)
        {
            var stats = new GeUtilities.Intervals.Parsers.Model.BedStats();
            foreach (var peak in CreatePeaks(intersCoord))
                stats.Update(peak);

            Assert.True(Math.Round(stats.WidthMean, 3) == mean);
        }

        [Theory]
        [InlineData(new int[0], 0)]
        [InlineData(new int[] { 10, 20 }, 0)]
        [InlineData(new int[] { 10, 20, 30, 32 }, 4)]
        [InlineData(new int[] { 10, 20, 30, 32, 40, 80 }, 16.3571)]
        public void TestWidthPSTDV(int[] intersCoord, double pstdv)
        {
            var stats = new GeUtilities.Intervals.Parsers.Model.BedStats();
            foreach (var peak in CreatePeaks(intersCoord))
                stats.Update(peak);

            Assert.True(Math.Round(stats.WidthPSTDV, 4) == pstdv);
        }
    }
}

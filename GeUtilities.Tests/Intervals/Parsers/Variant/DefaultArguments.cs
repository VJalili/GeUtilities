﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers;
using System.Linq;
using Xunit;

namespace Genometric.GeUtilities.Tests.Intervals.Parsers.Vcf
{
    public class DefaultArguments
    {
        [Fact]
        public void AssignHashKey()
        {
            // Arrange
            var rg = new RegionGenerator();
            using (var file = new TempFileCreator(rg))
            {
                // Act
                var parser = new VcfParser();
                var parsedData = parser.Parse(file.TempFilePath);

                // Assert
                Assert.True(parsedData.Chromosomes[rg.Chr].Strands[rg.Strand].Intervals.ToList()[0].GetHashCode() != 0);
            }
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 2)]
        public void AvoidHeader(int headerCount, byte readOffset)
        {
            // Arrange
            using (var file = new TempFileCreator(new RegionGenerator(), headerLineCount: headerCount))
            {
                // Act
                var parser = new VcfParser()
                {
                    ReadOffset = readOffset
                };
                var parsedData = parser.Parse(file.TempFilePath);

                // Assert
                Assert.True(parsedData.Chromosomes.Count == 1);
            }
        }
    }
}
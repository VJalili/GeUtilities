﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Intervals.Parsers;
using Genometric.GeUtilities.Intervals.Parsers.Model;
using System.Linq;
using Xunit;

namespace Genometric.GeUtilities.Tests.Intervals.Parsers.Bed
{
    public class ColumnsOrder
    {
        [Theory]
        [InlineData(0, 1, 2, 3, 4)]
        [InlineData(1, 0, 2, 3, 4)]
        [InlineData(1, 2, 0, 3, 4)]
        [InlineData(1, 2, 3, 0, 4)]
        [InlineData(1, 2, 3, 4, 0)]
        [InlineData(4, 3, 2, 1, 0)]
        [InlineData(2, 1, 0, 4, 3)]
        [InlineData(5, 6, 7, 8, 9)]
        public void ColumnsShuffle(byte chrColumn, byte leftColumn, sbyte rightColumn, byte nameColumn, byte valueColumn)
        {
            // Arrange
            var rg = new RegionGenerator
            {
                ChrColumn = chrColumn,
                LeftColumn = leftColumn,
                RightColumn = rightColumn,
                NameColumn = nameColumn,
                ValueColumn = valueColumn
            };

            using (var file = new TempFileCreator(rg))
            {
                // Act
                var parser = new BedParser(
                    new BedColumns()
                    {
                        Chr = chrColumn,
                        Left = leftColumn,
                        Right = rightColumn,
                        Name = nameColumn,
                        Value = valueColumn
                    });
                var parsedPeak = parser.Parse(file.Path).Chromosomes[rg.Chr].Strands[rg.Strand].Intervals.ToList()[0];

                // Assert
                Assert.True(parsedPeak.CompareTo(rg.Peak) == 0);
            }
        }

        [Fact]
        public void ColumnsSetters()
        {
            // Arrange
            var rg = new RegionGenerator
            {
                ChrColumn = 2,
                LeftColumn = 2,
                RightColumn = 9,
                ValueColumn = 0,
                SummitColumn = 2,
                NameColumn = 2,
                StrandColumn = 0,
            };

            using (var file = new TempFileCreator(rg))
            {
                // Act
                var parser = new BedParser(rg.Columns);
                var parsedPeak = parser.Parse(file.Path).Chromosomes[rg.Chr].Strands[rg.Strand].Intervals.ToList()[0];

                // Assert
                Assert.True(parsedPeak.CompareTo(rg.Peak) == 0);
            }
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(0, -1)]
        [InlineData(0, 99)]
        [InlineData(3, -1)]
        [InlineData(3, 99)]
        [InlineData(10, -1)]
        [InlineData(10, 99)]
        public void TestSummit(sbyte summitColumn, int summit)
        {
            // Arrange
            var rg = new RegionGenerator { SummitColumn = summitColumn };
            rg.Summit = summit == -1 ? rg.Left + ((rg.Right - rg.Left) / 2) : summit;

            using (var file = new TempFileCreator(rg))
            {
                // Act
                var parser = new BedParser(rg.Columns);
                var parsedPeak = parser.Parse(file.Path).Chromosomes[rg.Chr].Strands[rg.Strand].Intervals.ToList()[0];

                // Assert
                Assert.True(parsedPeak.Summit == rg.Summit);
            }
        }

        [Theory]
        [InlineData(0, '*')]
        [InlineData(3, '*')]
        [InlineData(9, '*')]
        [InlineData(1, '+')]
        [InlineData(2, '-')]
        [InlineData(-1, '*')]
        public void TestStrand(sbyte strandColumn, char strand)
        {
            // Arrange
            var rg = new RegionGenerator
            {
                Strand = strand,
                StrandColumn = strandColumn
            };

            using (var file = new TempFileCreator(rg))
            {
                // Act
                var parser = new BedParser(rg.Columns)
                {
                    UnspecifiedStrandChar = strand
                };

                // Assert
                Assert.True(parser.Parse(file.Path).Chromosomes[rg.Chr].Strands.ContainsKey(strand));
            }
        }

        [Fact]
        public void TestMultiStrand()
        {
            // Arrange
            string[] peaks = new string[]
            {
                "chr1\t10\t20\t*\tGeUtilities_00\t0.1",
                "chr1\t30\t40\t+\tGeUtilities_01\t0.11",
                "chr1\t50\t60\t-\tGeUtilities_02\t0.111",
                "chr1\t50\t60\t#\tGeUtilities_02\t0.111", // Any strand name other than '+', '-', and '*' will be parsed as '*'.
            };

            using (var file = new TempFileCreator(peaks))
            {
                // Act
                var parser = new BedParser(
                    new BedColumns
                    {
                        Chr = 0,
                        Left = 1,
                        Right = 2,
                        Strand = 3,
                        Name = 4,
                        Value = 5
                    })
                { UnspecifiedStrandChar = '*' };

                var parsedData = parser.Parse(file.Path);

                // Assert
                Assert.True(parsedData.Chromosomes["chr1"].Strands.ContainsKey('*'));
                Assert.True(parsedData.Chromosomes["chr1"].Strands.ContainsKey('+'));
                Assert.True(parsedData.Chromosomes["chr1"].Strands.ContainsKey('-'));
                Assert.False(parsedData.Chromosomes["chr1"].Strands.ContainsKey('#'));
            }
        }

        [Fact]
        public void DontFailIfGivenColumnNumberIsInvalid()
        {
            // Arrange
            using (var file = new TempFileCreator("chr1\t10\t20"))
            {
                var parser = new BedParser(new BedColumns() { Chr = 0, Left = 1, Right = 2, Name = 3, Strand = 4, Summit = 5, Value = 6 })
                {
                    DropPeakIfInvalidValue = false,
                };

                // Act
                var parsedPeak = parser.Parse(file.Path).Chromosomes["chr1"].Strands['.'].Intervals.ToList()[0];

                // Assert
                Assert.True(parsedPeak.Left == 10 && parsedPeak.Right == 20);
            }
        }
    }
}

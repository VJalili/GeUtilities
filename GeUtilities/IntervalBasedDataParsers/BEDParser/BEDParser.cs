﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IntervalBasedDataParsers.Model.Defaults;
using Genometric.GeUtilities.ReferenceGenomes;

namespace Genometric.GeUtilities.Parsers
{
    public class BEDParser : BEDParser<ChIPSeqPeak>
    {
        public BEDParser(
            string sourceFilePath,
            Assemblies assembly = Assemblies.Unknown,
            double defaultValue = 1E-8,
            PValueFormat pValueFormat = PValueFormat.SameAsInput,
            bool dropPeakIfInvalidValue = true,
            byte startOffset = 0,
            bool readOnlyValidChrs = true,
            uint maxLinesToBeRead = uint.MaxValue,
            HashFunction hashFunction = HashFunction.One_at_a_Time) :
            this(
                sourceFilePath: sourceFilePath,
                chrColumn: 0,
                leftEndColumn: 1,
                rightEndColumn: 2,
                nameColumn: 3,
                valueColumn: 4,
                strandColumn: -1,
                summitColumn: -1,
                assembly: assembly,
                defaultValue: defaultValue,
                pValueFormat: pValueFormat,
                dropPeakIfInvalidValue: dropPeakIfInvalidValue,
                startOffset: startOffset,
                readOnlyValidChrs: readOnlyValidChrs,
                maxLinesToBeRead: maxLinesToBeRead,
                hashFunction: hashFunction)
        { }

        public BEDParser(
            string sourceFilePath,
            byte chrColumn,
            byte leftEndColumn,
            sbyte rightEndColumn,
            byte nameColumn,
            byte valueColumn,
            sbyte strandColumn = -1,
            sbyte summitColumn = -1,
            Assemblies assembly = Assemblies.Unknown,
            double defaultValue = 1E-8,
            PValueFormat pValueFormat = PValueFormat.SameAsInput,
            bool dropPeakIfInvalidValue = true,
            byte startOffset = 0,
            bool readOnlyValidChrs = true,
            uint maxLinesToBeRead = uint.MaxValue,
            HashFunction hashFunction = HashFunction.One_at_a_Time) :
            base(
                sourceFilePath: sourceFilePath,
                chrColumn: chrColumn,
                leftEndColumn: leftEndColumn,
                rightEndColumn: rightEndColumn,
                nameColumn: nameColumn,
                valueColumn: valueColumn,
                strandColumn: strandColumn,
                summitColumn: summitColumn,
                assembly: assembly,
                defaultValue: defaultValue,
                pValueFormat: pValueFormat,
                dropPeakIfInvalidValue: dropPeakIfInvalidValue,
                startOffset: startOffset,
                readOnlyValidChrs: readOnlyValidChrs,
                maxLinesToBeRead: maxLinesToBeRead,
                hashFunction: hashFunction)
        { }
    }
}

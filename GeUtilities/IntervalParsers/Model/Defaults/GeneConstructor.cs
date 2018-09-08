﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;

namespace Genometric.GeUtilities.IntervalParsers.Model.Defaults
{
    public class GeneConstructor : IRefSeqConstructor<Gene>
    {
        public Gene Construct(int left, int right, string refSeqID, string geneSymbol, string hashSeed = "")
        {
            return new Gene(left, right, refSeqID, geneSymbol, hashSeed);
        }
    }
}

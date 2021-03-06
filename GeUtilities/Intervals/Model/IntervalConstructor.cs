﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;

namespace Genometric.GeUtilities.Intervals.Model
{
    public class IntervalConstructor : IIntervalConstructor<Interval>
    {
        public Interval Construct(int left, int right, string hashSeed = "")
        {
            return new Interval(left, right, hashSeed);
        }
    }
}

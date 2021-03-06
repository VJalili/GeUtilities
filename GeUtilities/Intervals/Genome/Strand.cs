﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System.Collections.Generic;

namespace Genometric.GeUtilities.Intervals.Genome
{
    public class Strand<I>
        where I : IInterval<int>
    {
        private readonly Dictionary<int, I> _intervals;
        public IReadOnlyCollection<I> Intervals
        {
            get { return _intervals.Values; }
        }

        public Strand()
        {
            _intervals = new Dictionary<int, I>();
        }

        public void Add(I interval)
        {
            _intervals.Add(interval.GetHashCode(), interval);
        }

        public bool TryGet(int hashkey, out I interval)
        {
            return _intervals.TryGetValue(hashkey, out interval);
        }

        public bool Contains(I interval)
        {
            return _intervals.ContainsKey(interval.GetHashCode());
        }
    }
}

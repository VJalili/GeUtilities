﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System;

namespace Genometric.GeUtilities.Parsers
{
    public sealed class BEDStats : IntervalStats
    {
        private double _pValueSum;
        private double _pValueSTDVTemp;

        public string Percentage { set; get; }
        public double PValueMax { set; get; }
        public double PValueMin { set; get; }
        public double PValueMean { set; get; }
        public double PValueSTDV { set; get; }

        public BEDStats() : base()
        {
            PValueMin = 1;
        }

        public new void Update(IInterval<int> interval)
        {
            base.Update(interval);

            // You can use `as` for safe casting as the following:
            // IChIPSeqPeak peak = interval as IChIPSeqPeak;
            // however, casting error must not appear here unless
            // a there is an issue in initializing these classes.
            // Therefore, I think an "unsafe" casting will help 
            // spotting such issues.
            IChIPSeqPeak peak = (IChIPSeqPeak)interval;

            if (!double.IsNaN(peak.Value))
            {
                _pValueSum += peak.Value;
                PValueMax = Math.Max(PValueMax, peak.Value);
                PValueMin = Math.Min(PValueMin, peak.Value);
                PValueMean = _pValueSum / Count;
                _pValueSTDVTemp += Math.Pow(peak.Value - PValueMean, 2.0);
                PValueSTDV = Math.Sqrt(_pValueSTDVTemp / Count);
            }
        }
    }
}

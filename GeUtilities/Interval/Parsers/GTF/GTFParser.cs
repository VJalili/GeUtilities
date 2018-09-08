﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.Interval.Model;
using Genometric.GeUtilities.Interval.Parsers.Model;

namespace Genometric.GeUtilities.Interval.Parsers
{
    public class GTFParser : GTFParser<GeneralFeature>
    {
        public GTFParser() : this(new GTFColumns())
        { }

        public GTFParser(GTFColumns columns) : base(columns, new GeneralFeatureConstructor())
        { }
    }
}

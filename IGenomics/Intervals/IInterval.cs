﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

namespace Genometric.GeUtilities.IGenomics
{
    public interface IInterval<C, M>
    {
        /// <summary>
        /// Sets and gets the left-end of the interval.
        /// </summary>
        C Left { set; get; }

        /// <summary>
        /// Sets and gets the right-end of the interval.
        /// </summary>
        C Right { set; get; }

        /// <summary>
        /// Sets and gets the descriptive metadata
        /// of the interval. It could be a reference
        /// to a memory object, or a pointer, or 
        /// an entry ID on database.
        /// </summary>
        M Metadata { set; get; }

        /// <summary>
        /// Sets and gets the the hashKey of the interval.
        /// </summary>
        uint HashKey { set; get; }
    }
}
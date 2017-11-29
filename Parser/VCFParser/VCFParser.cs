﻿// Licensed to the Genometric organization (https://github.com/Genometric) under one or more agreements.
// The Genometric organization licenses this file to you under the GNU General Public License v3.0 (GPLv3).
// See the LICENSE file in the project root for more information.

using Genometric.GeUtilities.IGenomics;
using System;

namespace Genometric.GeUtilities.Parsers
{
    public sealed class VCFParser<I, M> : Parser<I, M>
        where I : IInterval<int, M>, new()
        where M : IVCF, new()
    {
        public VCFParser(
            string source,
            Genomes species,
            Assemblies assembly)
        {
            InitializeDefaultValues();
            _source = source;
            _genome = species;
            _assembly = assembly;
            _readOnlyValidChrs = false;
            Initialize();
        }

        #region .::.         private Variables declaration               .::.

        private byte _idColumn { set; get; }
        private byte _refbpColumn { set; get; }
        private byte _altbpColumn { set; get; }
        private byte _qualityColumn { set; get; }
        private byte _filterColumn { set; get; }
        private byte _infoColumn { set; get; }

        #endregion

        private void InitializeDefaultValues()
        {
            maxLinesToBeRead = uint.MaxValue;
            _chrColumn = 0;
            _leftColumn = 1;
            _idColumn = 2;
            _refbpColumn = 3;
            _altbpColumn = 4;
            _qualityColumn = 5;
            _filterColumn = 6;
            _infoColumn = 7;
            _parsingType = ParsingType.VCF;
        }

        protected override I ParseLine(string[] line, uint lineCounter, out string intervalName)
        {
            I rtv = new I
            {
                Metadata = new M()
            };
            intervalName = "GeUtil_" + new Random().Next(10000, 100000).ToString();

            if(_idColumn < line.Length)
            {
                rtv.Metadata.ID = line[_idColumn];
            }
            else
            {
                _dropLine = true;
                DropLine("\tLine " + lineCounter.ToString() + "\t:\tInvalid ID column.");
            }

            if (_refbpColumn < line.Length)
            {
                rtv.Metadata.RefBase = new BasePair[line[_refbpColumn].Length];
                for (int i = 0; i < line[_refbpColumn].Length; i++)
                {
                    switch (line[_refbpColumn][i])
                    {
                        case 'A': rtv.Metadata.RefBase[i] = BasePair.A; break;
                        case 'C': rtv.Metadata.RefBase[i] = BasePair.C; break;
                        case 'G': rtv.Metadata.RefBase[i] = BasePair.G; break;
                        case 'N': rtv.Metadata.RefBase[i] = BasePair.N; break;
                        case 'T': rtv.Metadata.RefBase[i] = BasePair.T; break;
                        case 'U': rtv.Metadata.RefBase[i] = BasePair.U; break;
                        default:
                            _dropLine = true;
                            DropLine("\tLine " + lineCounter.ToString() + "\t:\tInvalid REF column.");
                            break;
                    }
                }
            }
            else
            {
                _dropLine = true;
                DropLine("\tLine " + lineCounter.ToString() + "\t:\tInvalid REF column.");
            }


            if (_altbpColumn < line.Length)
            {
                rtv.Metadata.AltBase = new BasePair[line[_altbpColumn].Length];
                for (int i = 0; i < line[_altbpColumn].Length; i++)
                {
                    switch (line[_altbpColumn][i])
                    {
                        case 'A': rtv.Metadata.AltBase[i] = BasePair.A; break;
                        case 'C': rtv.Metadata.AltBase[i] = BasePair.C; break;
                        case 'G': rtv.Metadata.AltBase[i] = BasePair.G; break;
                        case 'N': rtv.Metadata.AltBase[i] = BasePair.N; break;
                        case 'T': rtv.Metadata.AltBase[i] = BasePair.T; break;
                        case 'U': rtv.Metadata.AltBase[i] = BasePair.U; break;
                        default:
                            _dropLine = true;
                            DropLine("\tLine " + lineCounter.ToString() + "\t:\tInvalid ALT column.");
                            break;
                    }
                }
            }
            else
            {
                _dropLine = true;
                DropLine("\tLine " + lineCounter.ToString() + "\t:\tInvalid ALT column.");
            }


            if(_qualityColumn < line.Length)
            {
                if (double.TryParse(line[_qualityColumn], out double quality))
                    rtv.Metadata.Quality = quality;
            }
            else
            {
                _dropLine = true;
                DropLine("\tLine " + lineCounter.ToString() + "\t:\tInvalid quality column.");
            }


            if(_filterColumn < line.Length)
            {
                rtv.Metadata.Filter = line[_filterColumn];
            }
            else
            {
                _dropLine = true;
                DropLine("\tLine " + lineCounter.ToString() + "\t:\tInvalid filter column.");
            }


            if (_infoColumn < line.Length)
            {
                rtv.Metadata.Info = line[_infoColumn];
            }
            else
            {
                _dropLine = true;
                DropLine("\tLine " + lineCounter.ToString() + "\t:\tInvalid info column.");
            }

            return rtv;
        }

        public ParsedVariants<int, I, M> Parse()
        {
            var rtv = (ParsedVariants<int, I, M>)PARSE();
            Status = "100";
            return rtv;
        }
    }
}
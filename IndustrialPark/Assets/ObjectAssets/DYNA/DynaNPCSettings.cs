﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using static IndustrialPark.ConverterFunctions;

namespace IndustrialPark
{
    public class DynaNPCSettings : DynaBase
    {
        public override string Note => "Version is always 2";

        public DynaNPCSettings() : base() { }

        public DynaNPCSettings(IEnumerable<byte> enumerable) : base (enumerable)
        {
            Unknown1 = Switch(BitConverter.ToInt32(data, 0x0));
            Flags1 = data[0x4];
            Flags2 = data[0x5];
            Flags3 = data[0x6];
            Flags4 = data[0x7];
            Flags5 = data[0x8];
            Flags6 = data[0x9];
            Flags7 = data[0xA];
            Flags8 = data[0xB];
            Flags9 = data[0xC];
            Flags10 = data[0xD];
            Flags11 = data[0xE];
            Flags12 = data[0xF];
            Unknown2 = Switch(BitConverter.ToInt32(data, 0x10));
            UnknownFloat = Switch(BitConverter.ToSingle(data, 0x14));
            Unknown3 = Switch(BitConverter.ToInt32(data, 0x18));
        }

        public override byte[] ToByteArray()
        {
            List<byte> list = new List<byte>();
            list.AddRange(BitConverter.GetBytes(Switch(Unknown1)));
            list.Add(Flags1);
            list.Add(Flags2);
            list.Add(Flags3);
            list.Add(Flags4);
            list.Add(Flags5);
            list.Add(Flags6);
            list.Add(Flags7);
            list.Add(Flags8);
            list.Add(Flags9);
            list.Add(Flags10);
            list.Add(Flags11);
            list.Add(Flags12);
            list.AddRange(BitConverter.GetBytes(Switch(Unknown2)));
            list.AddRange(BitConverter.GetBytes(Switch(UnknownFloat)));
            list.AddRange(BitConverter.GetBytes(Switch(Unknown3)));
            return list.ToArray();
        }

        public int Unknown1 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags1 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags2 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags3 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags4 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags5 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags6 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags7 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags8 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags9 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags10 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags11 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags12 { get; set; }
        public int Unknown2 { get; set; }
        [TypeConverter(typeof(FloatTypeConverter))]
        public float UnknownFloat { get; set; }
        public int Unknown3 { get; set; }
    }
}
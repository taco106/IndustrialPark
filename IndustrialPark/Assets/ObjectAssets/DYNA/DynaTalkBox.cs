﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using static IndustrialPark.ConverterFunctions;

namespace IndustrialPark
{
    public class DynaTalkBox : DynaBase
    {
        public override string Note => "Version is always 11";

        public DynaTalkBox() : base()
        {
            TextBoxID1 = 0;
            TextBoxID2 = 0;
            TextBoxID3 = 0;
            PointerID = 0;
            TextID1 = 0;
            TextID2 = 0;
            TextID3 = 0;
            TextID4 = 0;
            TextID5 = 0;
        }

        public DynaTalkBox(IEnumerable<byte> enumerable) : base (enumerable)
        {
            TextBoxID1 = Switch(BitConverter.ToUInt32(data, 0x0));
            TextBoxID2 = Switch(BitConverter.ToUInt32(data, 0x4));
            TextBoxID3 = Switch(BitConverter.ToUInt32(data, 0x8));
            Flags1 = data[0xC];
            Flags2 = data[0xD];
            Flags3 = data[0xE];
            Flags4 = data[0xF];
            UnknownInt1 = Switch(BitConverter.ToInt32(data, 0x10));
            PointerID = Switch(BitConverter.ToUInt32(data, 0x14));
            Flags5 = data[0x18];
            Flags6 = data[0x19];
            Flags7 = data[0x1A];
            Flags8 = data[0x1B];
            UnknownFloat = Switch(BitConverter.ToSingle(data, 0x1C));
            UnknownInt2 = Switch(BitConverter.ToInt32(data, 0x20));
            TextID1 = Switch(BitConverter.ToUInt32(data, 0x24));
            TextID2 = Switch(BitConverter.ToUInt32(data, 0x28));
            TextID3 = Switch(BitConverter.ToUInt32(data, 0x2C));
            TextID4 = Switch(BitConverter.ToUInt32(data, 0x30));
            TextID5 = Switch(BitConverter.ToUInt32(data, 0x34));
        }

        public override byte[] ToByteArray()
        {
            List<byte> list = new List<byte>();
            list.AddRange(BitConverter.GetBytes(Switch(TextBoxID1)));
            list.AddRange(BitConverter.GetBytes(Switch(TextBoxID2)));
            list.AddRange(BitConverter.GetBytes(Switch(TextBoxID3)));
            list.Add(Flags1);
            list.Add(Flags2);
            list.Add(Flags3);
            list.Add(Flags4);
            list.AddRange(BitConverter.GetBytes(Switch(UnknownInt1)));
            list.AddRange(BitConverter.GetBytes(Switch(PointerID)));
            list.Add(Flags5);
            list.Add(Flags6);
            list.Add(Flags7);
            list.Add(Flags8);
            list.AddRange(BitConverter.GetBytes(Switch(UnknownFloat)));
            list.AddRange(BitConverter.GetBytes(Switch(UnknownInt2)));
            list.AddRange(BitConverter.GetBytes(Switch(TextID1)));
            list.AddRange(BitConverter.GetBytes(Switch(TextID2)));
            list.AddRange(BitConverter.GetBytes(Switch(TextID3)));
            list.AddRange(BitConverter.GetBytes(Switch(TextID4)));
            list.AddRange(BitConverter.GetBytes(Switch(TextID5)));
            return list.ToArray();
        }

        public AssetID TextBoxID1 { get; set; }
        public AssetID TextBoxID2 { get; set; }
        public AssetID TextBoxID3 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags1 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags2 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags3 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags4 { get; set; }
        public int UnknownInt1 { get; set; }
        public AssetID PointerID { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags5 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags6 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags7 { get; set; }
        [TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flags8 { get; set; }
        [TypeConverter(typeof(FloatTypeConverter))]
        public float UnknownFloat { get; set; }
        public int UnknownInt2 { get; set; }
        public AssetID TextID1 { get; set; }
        public AssetID TextID2 { get; set; }
        public AssetID TextID3 { get; set; }
        public AssetID TextID4 { get; set; }
        public AssetID TextID5 { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HipHopFile;
using static IndustrialPark.ConverterFunctions;

namespace IndustrialPark
{
    public class KeyFrame
    {
        public short Frame { get; set; }
        public short Xrot { get; set; }
        public short Yrot { get; set; }
        public short Zrot { get; set; }
        public short Scale { get; set; }
        public short Xpos { get; set; }
        public short Ypos { get; set; }
        public short Zpos { get; set; }
    }

    public class KeyFrameMap
    {
        public int Unknown { get; set; }
        public short Unknown1 { get; set; }
        public short Unknown2 { get; set; }
    }

    public class AssetANIM : Asset
    {
        public AssetANIM(Section_AHDR AHDR) : base(AHDR) { }

        public int Unknown_04
        {
            get => ReadInt(0x04);
            set => Write(0x04, value);
        }

        public short NumBones
        {
            get => ReadShort(0x08);
            set => Write(0x08, value);
        }

        public short NumFrames
        {
            get => ReadShort(0x0A);
            set => Write(0x0A, value);
        }

        public int NumKeyFrames
        {
            get => ReadInt(0xC);
            set => Write(0xC, value);
        }

        public int Unknown10
        {
            get => ReadInt(0x10);
            set => Write(0x10, value);
        }

        public int Unknown14
        {
            get => ReadInt(0x14);
            set => Write(0x14, value);
        }

        public int Unknown18
        {
            get => ReadInt(0x18);
            set => Write(0x18, value);
        }

        private int KeyFramesSectionStart { get => 0x1C; }

        public KeyFrame[] KeyFrames
        {
            get
            {
                List<KeyFrame> keyFrames = new List<KeyFrame>();
                for (int i = KeyFramesSectionStart; i < KeyFramesSectionStart + NumKeyFrames * 0x10; i += 0x10)
                {
                    keyFrames.Add(new KeyFrame
                    {
                        Frame = ReadShort(i + 0x00),
                        Xrot = ReadShort(i + 0x02),
                        Yrot = ReadShort(i + 0x04),
                        Zrot = ReadShort(i + 0x06),
                        Scale = ReadShort(i + 0x08),
                        Xpos = ReadShort(i + 0x0A),
                        Ypos = ReadShort(i + 0x0C),
                        Zpos = ReadShort(i + 0x0E)
                    });
                }
                return keyFrames.ToArray();
            }
            set
            {
                List<byte> before = AHDR.data.Take(KeyFramesSectionStart).ToList();
                List<byte> after = AHDR.data.Skip(KeyFramesSectionStart + NumKeyFrames * 0x10).ToList();
                foreach (KeyFrame k in value)
                {
                    before.AddRange(BitConverter.GetBytes(Switch(k.Frame)));
                    before.AddRange(BitConverter.GetBytes(Switch(k.Xrot)));
                    before.AddRange(BitConverter.GetBytes(Switch(k.Yrot)));
                    before.AddRange(BitConverter.GetBytes(Switch(k.Zrot)));
                    before.AddRange(BitConverter.GetBytes(Switch(k.Scale)));
                    before.AddRange(BitConverter.GetBytes(Switch(k.Xpos)));
                    before.AddRange(BitConverter.GetBytes(Switch(k.Ypos)));
                    before.AddRange(BitConverter.GetBytes(Switch(k.Zpos)));
                }
                before.AddRange(after);
                Data = before.ToArray();
                NumKeyFrames = value.Length;
            }
        }

        private int TimeMapSectionStart { get => KeyFramesSectionStart + NumKeyFrames * 0x10; }

        public float[] TimeMap
        {
            get
            {
                List<float> timeMap = new List<float>();
                for (int i = TimeMapSectionStart; i < TimeMapSectionStart + 4 * NumFrames; i += 4)
                    timeMap.Add(ReadFloat(i));
                return timeMap.ToArray();
            }
            set
            {
                List<byte> before = AHDR.data.Take(TimeMapSectionStart).ToList();
                List<byte> after = AHDR.data.Skip(TimeMapSectionStart + 4 * NumFrames).ToList();
                foreach (float k in value)
                    before.AddRange(BitConverter.GetBytes(Switch(k)));
                before.AddRange(after);
                Data = before.ToArray();
                NumFrames = (short)value.Length;
            }
        }

        private int KeyFrameMapSectionStart { get => TimeMapSectionStart + NumFrames * 4; }

        public KeyFrameMap[] KeyFrameMap
        {
            get
            {
                List<KeyFrameMap> keyFrameMap = new List<KeyFrameMap>();
                for (int i = KeyFrameMapSectionStart; i < KeyFrameMapSectionStart + 8 * (NumFrames - 1); i += 8)
                {
                    keyFrameMap.Add(new KeyFrameMap()
                    {
                        Unknown = ReadInt(i),
                        Unknown1 = ReadShort(i + 4),
                        Unknown2 = ReadShort(i + 6),
                    });
                }
                return keyFrameMap.ToArray();
            }
            set
            {
                List<byte> before = AHDR.data.Take(KeyFrameMapSectionStart).ToList();
                foreach (KeyFrameMap i in value)
                {
                    before.AddRange(BitConverter.GetBytes(Switch(i.Unknown)));
                    before.AddRange(BitConverter.GetBytes(Switch(i.Unknown1)));
                    before.AddRange(BitConverter.GetBytes(Switch(i.Unknown2)));
                }

                Data = before.ToArray();
            }
        }
    }
}
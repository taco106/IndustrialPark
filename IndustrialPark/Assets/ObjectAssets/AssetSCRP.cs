﻿using HipHopFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using static HipHopFile.Functions;

namespace IndustrialPark
{
    public class AssetSCRP : ObjectAsset
    {
        public AssetSCRP(Section_AHDR AHDR) : base(AHDR) { }

        public override bool HasReference(uint assetID)
        {
            foreach (Link link in TimedLinksBFBB)
            {
                if (link.TargetAssetID == assetID)
                    return true;
                if (link.ArgumentAssetID == assetID)
                    return true;
                if (link.SourceCheckAssetID == assetID)
                    return true;
            }

            return base.HasReference(assetID);
        }

        [Category("Scripted Event")]
        public float UnknownFloat08
        {
            get => ReadFloat(0x08);
            set => Write(0x08, value);
        }

        [Category("Scripted Event"), ReadOnly(true)]
        public int TimedLinkCount
        {
            get => ReadInt(0x0C);
            set => Write(0x0C, value);
        }

        [Category("Scripted Event (Movie Only)"), TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flag1
        {
            get => ReadByte(0x10);
            set => Write(0x10, value);
        }

        [Category("Scripted Event (Movie Only)"), TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flag2
        {
            get => ReadByte(0x11);
            set => Write(0x11, value);
        }

        [Category("Scripted Event (Movie Only)"), TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flag3
        {
            get => ReadByte(0x12);
            set => Write(0x12, value);
        }

        [Category("Scripted Event (Movie Only)"), TypeConverter(typeof(HexByteTypeConverter))]
        public byte Flag4
        {
            get => ReadByte(0x13);
            set => Write(0x13, value);
        }

        private int TimedLinksStartOffset => currentGame == Game.Incredibles ? 0x14 : 0x10;

        private void WriteTimedLinks(Link[] links)
        {
            List<byte> newData = Data.Take(TimedLinksStartOffset).ToList();
            List<byte> restOfOldData = Data.Skip(TimedLinksStartOffset + Link.sizeOfStruct * TimedLinkCount).ToList();

            foreach (Link i in links)
                newData.AddRange(i.ToByteArray());

            newData.AddRange(restOfOldData);
            Data = newData.ToArray();

            TimedLinkCount = links.Length;
        }

        
        [Category("Scripted Event"), Editor(typeof(LinkListEditor), typeof(UITypeEditor))]
        public LinkBFBB[] TimedLinksBFBB
        {
            get
            {
                LinkBFBB[] events = new LinkBFBB[TimedLinkCount];

                for (int i = 0; i < TimedLinkCount; i++)
                    events[i] = new LinkBFBB(Data, TimedLinksStartOffset + i * Link.sizeOfStruct, true);

                LinkListEditor.IsTimed = true;
                return events;
            }
            set
            {
                WriteTimedLinks(value);
            }
        }

        [Category("Scripted Event"), Editor(typeof(LinkListEditor), typeof(UITypeEditor))]
        public LinkTSSM[] TimedLinksTSSM
        {
            get
            {
                LinkTSSM[] events = new LinkTSSM[TimedLinkCount];

                for (int i = 0; i < TimedLinkCount; i++)
                    events[i] = new LinkTSSM(Data, TimedLinksStartOffset + i * Link.sizeOfStruct, true);

                LinkListEditor.IsTimed = true;
                return events;
            }
            set
            {
                WriteTimedLinks(value);
            }
        }

        [Category("Scripted Event"), Editor(typeof(LinkListEditor), typeof(UITypeEditor))]
        public LinkIncredibles[] TimedLinksIncredibles
        {
            get
            {
                LinkIncredibles[] events = new LinkIncredibles[TimedLinkCount];

                for (int i = 0; i < TimedLinkCount; i++)
                    events[i] = new LinkIncredibles(Data, TimedLinksStartOffset + i * Link.sizeOfStruct, true);

                LinkListEditor.IsTimed = true;
                return events;
            }
            set
            {
                WriteTimedLinks(value);
            }
        }
    }
}
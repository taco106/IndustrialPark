﻿using HipHopFile;
using System.Collections.Generic;
using System.ComponentModel;

namespace IndustrialPark
{
    public class DynaSceneProperties : AssetDYNA
    {
        private const string dynaCategoryName = "Scene Properties";

        protected override short constVersion => 1;

        [Category(dynaCategoryName)]
        public AssetID[] idle03Extras { get; set; }
        [Category(dynaCategoryName)]
        public AssetID[] idle04Extras { get; set; }
        [Category(dynaCategoryName)]
        public AssetByte bombCount { get; set; }
        [Category(dynaCategoryName)]
        public AssetByte extraIdleDelay { get; set; }
        [Category(dynaCategoryName)]
        public AssetByte hdrGlow { get; set; }
        [Category(dynaCategoryName)]
        public AssetByte hdrDarken { get; set; }
        [Category(dynaCategoryName)]
        public AssetID BackgroundMusic_SoundAssetID { get; set; }
        [Category(dynaCategoryName)]
        public int scenePropertiesFlags { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle waterTileWidth { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle lodFadeDistance { get; set; }
        [Category(dynaCategoryName)]
        public int UnknownInt24 { get; set; }
        [Category(dynaCategoryName)]
        public int UnknownInt28 { get; set; }
        [Category(dynaCategoryName)]
        public int UnknownInt2C { get; set; }
        [Category(dynaCategoryName)]
        public int UnknownInt30 { get; set; }

        private const string incOnly = "Incredibles only";

        [Category(dynaCategoryName), Description(incOnly)]
        public int UnknownInt34 { get; set; }
        [Category(dynaCategoryName), Description(incOnly)]
        public int UnknownInt38 { get; set; }
        [Category(dynaCategoryName), Description(incOnly)]
        public int UnknownInt3C { get; set; }
        [Category(dynaCategoryName), Description(incOnly)]
        public int UnknownInt40 { get; set; }
        [Category(dynaCategoryName), Description(incOnly)]
        public int UnknownInt44 { get; set; }
        [Category(dynaCategoryName), Description(incOnly)]
        public int UnknownInt48 { get; set; }
        [Category(dynaCategoryName), Description(incOnly)]
        public int UnknownInt4C { get; set; }
        [Category(dynaCategoryName), Description(incOnly)]
        public int UnknownInt50 { get; set; }
        [Category(dynaCategoryName), Description(incOnly)]
        public int UnknownInt54 { get; set; }

        private bool inc = false;

        public DynaSceneProperties(string assetName) : base(assetName, DynaType.SceneProperties, 1)
        {
            idle03Extras = new AssetID[0];
            idle04Extras = new AssetID[0];
            bombCount = 0x14;
            extraIdleDelay = 0x05;
            hdrGlow = 0x03;
            hdrDarken = 0x1E;
            BackgroundMusic_SoundAssetID = 0;
            scenePropertiesFlags = 1;
            waterTileWidth = 0;
            lodFadeDistance = 4;
            UnknownInt24 = 0;
            UnknownInt28 = 0;
            UnknownInt2C = 0;
            UnknownInt30 = 0;
        }

        public DynaSceneProperties(Section_AHDR AHDR, Game game, Endianness endianness) : base(AHDR, DynaType.SceneProperties, game, endianness)
        {
            using (var reader = new EndianBinaryReader(AHDR.data, endianness))
            {
                reader.BaseStream.Position = dynaDataStartPosition;

                idle03Extras = new AssetID[reader.ReadInt32()];
                var idle03ExtraOffset = reader.ReadInt32() + 0x10;
                idle04Extras = new AssetID[reader.ReadInt32()];
                var idle04ExtraOffset = reader.ReadInt32() + 0x10;

                bombCount = reader.ReadByte();
                extraIdleDelay = reader.ReadByte();
                hdrGlow = reader.ReadByte();
                hdrDarken = reader.ReadByte();
                BackgroundMusic_SoundAssetID = reader.ReadUInt32();
                scenePropertiesFlags = reader.ReadInt32();
                waterTileWidth = reader.ReadSingle();
                lodFadeDistance = reader.ReadSingle();
                UnknownInt24 = reader.ReadInt32();
                UnknownInt28 = reader.ReadInt32();
                UnknownInt2C = reader.ReadInt32();
                UnknownInt30 = reader.ReadInt32();

                if (reader.BaseStream.Length - Link.sizeOfStruct * _links.Length - idle03Extras.Length * 4 - idle04Extras.Length * 4 != reader.BaseStream.Position)
                {
                    inc = true;

                    UnknownInt34 = reader.ReadInt32();
                    UnknownInt38 = reader.ReadInt32();
                    UnknownInt3C = reader.ReadInt32();
                    UnknownInt40 = reader.ReadInt32();
                    UnknownInt44 = reader.ReadInt32();
                    UnknownInt48 = reader.ReadInt32();
                    UnknownInt4C = reader.ReadInt32();
                    UnknownInt50 = reader.ReadInt32();
                    UnknownInt54 = reader.ReadInt32();
                }

                reader.BaseStream.Position = idle03ExtraOffset;
                for (int i = 0; i < idle03Extras.Length; i++)
                    idle03Extras[i] = reader.ReadUInt32();
                reader.BaseStream.Position = idle04ExtraOffset;
                for (int i = 0; i < idle04Extras.Length; i++)
                    idle04Extras[i] = reader.ReadUInt32();
            }
        }

        protected override byte[] SerializeDyna(Game game, Endianness endianness)
        {
            using (var writer = new EndianBinaryWriter(endianness))
            {
                writer.Write(idle03Extras.Length);
                writer.Write(0);
                writer.Write(idle04Extras.Length);
                writer.Write(0);
                writer.Write(bombCount);
                writer.Write(extraIdleDelay);
                writer.Write(hdrGlow);
                writer.Write(hdrDarken);
                writer.Write(BackgroundMusic_SoundAssetID);
                writer.Write(scenePropertiesFlags);
                writer.Write(waterTileWidth);
                writer.Write(lodFadeDistance);
                writer.Write(UnknownInt24);
                writer.Write(UnknownInt28);
                writer.Write(UnknownInt2C);
                writer.Write(UnknownInt30);

                if (inc)
                {
                    writer.Write(UnknownInt34);
                    writer.Write(UnknownInt38);
                    writer.Write(UnknownInt3C);
                    writer.Write(UnknownInt40);
                    writer.Write(UnknownInt44);
                    writer.Write(UnknownInt48);
                    writer.Write(UnknownInt4C);
                    writer.Write(UnknownInt50);
                    writer.Write(UnknownInt54);
                }

                var idle03Pos = (int)writer.BaseStream.Position;
                foreach (var u in idle03Extras)
                    writer.Write(u);

                var idle04Pos = (int)writer.BaseStream.Position;
                foreach (var u in idle04Extras)
                    writer.Write(u);

                writer.BaseStream.Position = 0x4;
                writer.Write(idle03Pos);

                writer.BaseStream.Position = 0xC;
                writer.Write(idle04Pos);

                return writer.ToArray();
            }
        }

        public override bool HasReference(uint assetID) => BackgroundMusic_SoundAssetID == assetID || base.HasReference(assetID);

        public override void Verify(ref List<string> result)
        {
            if (BackgroundMusic_SoundAssetID == 0)
                result.Add("Scene Properties with no song reference");
            Verify(BackgroundMusic_SoundAssetID, ref result);
            base.Verify(ref result);
        }

        public override void SetDynamicProperties(DynamicTypeDescriptor dt)
        {
            if (!inc)
            {
                dt.RemoveProperty("UnknownInt34");
                dt.RemoveProperty("UnknownInt38");
                dt.RemoveProperty("UnknownInt3C");
                dt.RemoveProperty("UnknownInt40");
                dt.RemoveProperty("UnknownInt44");
                dt.RemoveProperty("UnknownInt48");
                dt.RemoveProperty("UnknownInt4C");
                dt.RemoveProperty("UnknownInt50");
                dt.RemoveProperty("UnknownInt54");
            }
            base.SetDynamicProperties(dt);
        }
    }
}
﻿using HipHopFile;
using System.Collections.Generic;
using System.ComponentModel;

namespace IndustrialPark
{
    public enum PlayerEnum
    {
        Patrick = 0,
        Sandy = 1
    }

    public class DynaGObjectBusStop : AssetDYNA
    {
        private const string dynaCategoryName = "game_object:BusStop";

        protected override short constVersion => 2;

        [Category(dynaCategoryName)]
        public AssetID MRKR_ID { get; set; }
        [Category(dynaCategoryName)]
        public PlayerEnum Player { get; set; }
        [Category(dynaCategoryName)]
        public AssetID CAM_ID { get; set; }
        [Category(dynaCategoryName)]
        public AssetID SIMP_ID { get; set; }
        [Category(dynaCategoryName)]
        public AssetSingle Delay { get; set; }

        public DynaGObjectBusStop(string assetName, uint mrkrAssetId, uint camAssetId, uint simpAssetId) : base(assetName, DynaType.game_object__BusStop, 2)
        {
            MRKR_ID = mrkrAssetId;
            CAM_ID = camAssetId;
            SIMP_ID = simpAssetId;

            Delay = 1.5f;
        }

        public DynaGObjectBusStop(Section_AHDR AHDR, Game game, Endianness endianness) : base(AHDR, DynaType.game_object__BusStop, game, endianness)
        {
            using (var reader = new EndianBinaryReader(AHDR.data, endianness))
            {
                reader.BaseStream.Position = dynaDataStartPosition;

                MRKR_ID = reader.ReadUInt32();
                Player = (PlayerEnum)reader.ReadInt32();
                CAM_ID = reader.ReadUInt32();
                SIMP_ID = reader.ReadUInt32();
                Delay = reader.ReadSingle();
            }
        }

        protected override byte[] SerializeDyna(Game game, Endianness endianness)
        {
            using (var writer = new EndianBinaryWriter(endianness))
            {
                writer.Write(MRKR_ID);
                writer.Write((int)Player);
                writer.Write(CAM_ID);
                writer.Write(SIMP_ID);
                writer.Write(Delay);

                return writer.ToArray();
            }
        }

        public override bool HasReference(uint assetID)
        {
            if (MRKR_ID == assetID)
                return true;
            if (CAM_ID == assetID)
                return true;
            if (SIMP_ID == assetID)
                return true;

            return base.HasReference(assetID);
        }

        public override void Verify(ref List<string> result)
        {
            if (MRKR_ID == 0)
                result.Add("Bus stop with no MRKR reference");
            Verify(MRKR_ID, ref result);
            if (CAM_ID == 0)
                result.Add("Bus stop with no CAM reference");
            Verify(CAM_ID, ref result);
            if (SIMP_ID == 0)
                result.Add("Bus stop with no SIMP reference");
            Verify(SIMP_ID, ref result);

            base.Verify(ref result);
        }
    }
}
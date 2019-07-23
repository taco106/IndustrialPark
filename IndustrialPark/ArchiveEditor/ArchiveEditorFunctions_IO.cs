﻿using HipHopFile;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using static HipHopFile.Functions;

namespace IndustrialPark
{
    public partial class ArchiveEditorFunctions
    {
        public void ExportHip(string fileName)
        {
            HipSection[] hipFile = SetupStream(ref HIPA, ref PACK, ref DICT, ref STRM);
            HipArrayToIni(hipFile, fileName, true, true);
        }
        
        public void ImportHip(string[] fileNames)
        {
            foreach (string fileName in fileNames)
                ImportHip(fileName);
        }

        public void ImportHip(string fileName)
        {
            if (Path.GetExtension(fileName).ToLower() == ".hip" || Path.GetExtension(fileName).ToLower() == ".hop")
                ImportHip(HipFileToHipArray(fileName));
            else if (Path.GetExtension(fileName).ToLower() == ".ini")
                ImportHip(IniToHipArray(fileName));
            else
                MessageBox.Show("Invalid file: " + fileName);
        }

        public void ImportHip(HipSection[] hipSections)
        {
            UnsavedChanges = true;

            foreach (HipSection i in hipSections)
            {
                if (i is Section_DICT dict)
                {
                    foreach (Section_AHDR AHDR in dict.ATOC.AHDRList)
                    {
                        if (AHDR.assetType == AssetType.COLL && ContainsAssetWithType(AssetType.COLL))
                        {
                            foreach (Section_LHDR LHDR in dict.LTOC.LHDRList)
                                LHDR.assetIDlist.Remove(AHDR.assetID);

                            MergeCOLL(AHDR);
                            continue;
                        }
                        else if (AHDR.assetType == AssetType.JAW && ContainsAssetWithType(AssetType.JAW))
                        {
                            foreach (Section_LHDR LHDR in dict.LTOC.LHDRList)
                                LHDR.assetIDlist.Remove(AHDR.assetID);

                            MergeJAW(AHDR);
                            continue;
                        }
                        else if (AHDR.assetType == AssetType.LODT && ContainsAssetWithType(AssetType.LODT))
                        {
                            foreach (Section_LHDR LHDR in dict.LTOC.LHDRList)
                                LHDR.assetIDlist.Remove(AHDR.assetID);

                            MergeLODT(AHDR);
                            continue;
                        }
                        else if (AHDR.assetType == AssetType.PIPT && ContainsAssetWithType(AssetType.PIPT))
                        {
                            foreach (Section_LHDR LHDR in dict.LTOC.LHDRList)
                                LHDR.assetIDlist.Remove(AHDR.assetID);

                            MergePIPT(AHDR);
                            continue;
                        }
                        else if (AHDR.assetType == AssetType.SHDW && ContainsAssetWithType(AssetType.SHDW))
                        {
                            foreach (Section_LHDR LHDR in dict.LTOC.LHDRList)
                                LHDR.assetIDlist.Remove(AHDR.assetID);

                            MergeSHDW(AHDR);
                            continue;
                        }
                        else if (AHDR.assetType == AssetType.SNDI && ContainsAssetWithType(AssetType.SNDI))
                        {
                            foreach (Section_LHDR LHDR in dict.LTOC.LHDRList)
                                LHDR.assetIDlist.Remove(AHDR.assetID);

                            MergeSNDI(AHDR);
                            continue;
                        }

                        if (ContainsAsset(AHDR.assetID))
                        {
                            DialogResult result = MessageBox.Show($"Asset [{AHDR.assetID.ToString("X8")}] {AHDR.ADBG.assetName} already present in archive. Do you wish to overwrite it?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {
                                RemoveAsset(AHDR.assetID);
                                DICT.ATOC.AHDRList.Add(AHDR);
                                AddAssetToDictionary(AHDR, false);
                            }
                            else
                            {
                                foreach (Section_LHDR LHDR in dict.LTOC.LHDRList)
                                    LHDR.assetIDlist.Remove(AHDR.assetID);
                            }
                        }
                        else
                        {
                            DICT.ATOC.AHDRList.Add(AHDR);
                            AddAssetToDictionary(AHDR, false);
                        }
                    }

                    foreach (Section_LHDR LHDR in dict.LTOC.LHDRList)
                        if (LHDR.assetIDlist.Count != 0)
                            DICT.LTOC.LHDRList.Add(LHDR);

                    break;
                }
            }

            DICT.LTOC.LHDRList = DICT.LTOC.LHDRList.OrderBy(f => f.layerType, new LHDRComparer()).ToList();

            RecalculateAllMatrices();
        }
    }
}

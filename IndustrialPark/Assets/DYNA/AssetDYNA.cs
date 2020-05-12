﻿using HipHopFile;
using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static IndustrialPark.ArchiveEditorFunctions;

namespace IndustrialPark
{
    public class AssetDYNA : BaseAsset, IRenderableAsset, IClickableAsset, IRotatableAsset, IScalableAsset
    {
        public AssetDYNA(Section_AHDR AHDR, Game game, Platform platform) : base(AHDR, game, platform)
        {
            SetDynaSpecific(false);
        }

        public override bool HasReference(uint assetID) => DynaSpec.HasReference(assetID) || base.HasReference(assetID);

        public override void Verify(ref List<string> result)
        {
            DynaSpec.Verify(ref result);
            base.Verify(ref result);
        }

        public void SetDynaSpecific(bool reset)
        {
            switch (Type)
            {
                case DynaType.Enemy__SB__BucketOTron: DynaSpec = new DynaEnemyBucketOTron(this); break;
                case DynaType.Enemy__SB__CastNCrew: DynaSpec = new DynaEnemyCastNCrew(this); break;
                case DynaType.Enemy__SB__Critter: DynaSpec = new DynaEnemyCritter(this); break;
                case DynaType.Enemy__SB__Dennis: DynaSpec = new DynaEnemyDennis(this); break;
                case DynaType.Enemy__SB__FrogFish: DynaSpec = new DynaEnemyFrogFish(this); break;
                case DynaType.Enemy__SB__Mindy: DynaSpec = new DynaEnemyMindy(this); break;
                case DynaType.Enemy__SB__Neptune: DynaSpec = new DynaEnemyNeptune(this); break;
                case DynaType.Enemy__SB__Standard: DynaSpec = new DynaEnemyStandard(this); break;
                case DynaType.Enemy__SB__SupplyCrate: DynaSpec = new DynaSupplyCrate(this); break;
                case DynaType.Enemy__SB__Turret: DynaSpec = new DynaEnemyTurret(this); break;
                case DynaType.Incredibles__Icon: DynaSpec = new DynaIncrediblesIcon(this); break;
                case DynaType.JSPExtraData: DynaSpec = new DynaJSPExtraData(this); break;
                case DynaType.SceneProperties: DynaSpec = new DynaSceneProperties(this); break;
                case DynaType.effect__Lightning: DynaSpec = new DynaEffectLightning(this); break;
                case DynaType.effect__Rumble: DynaSpec = new DynaRumble(this); break;
                case DynaType.effect__RumbleSphericalEmitter: DynaSpec = new DynaRumbleSphere(this); break;
                case DynaType.effect__ScreenFade: DynaSpec = new DynaEffectScreenFade(this); break;
                case DynaType.effect__smoke_emitter: DynaSpec = new DynaSmokeEmitter(this); break;
                case DynaType.game_object__BoulderGenerator: DynaSpec = new DynaBoulderGen(this); break;
                case DynaType.game_object__BusStop: DynaSpec = new DynaBusStop(this); break;
                case DynaType.game_object__Camera_Tweak: DynaSpec = new DynaCamTweak(this); break;
                case DynaType.game_object__Flythrough: DynaSpec = new DynaFlythrough(this); break;
                case DynaType.game_object__IN_Pickup: DynaSpec = new DynaInPickup(this); break;
                case DynaType.game_object__NPCSettings: DynaSpec = new DynaNPCSettings(this); break;
                case DynaType.game_object__Ring: DynaSpec = new DynaRing(this); break;
                case DynaType.game_object__RingControl: DynaSpec = new DynaRingControl(this); break;
                case DynaType.game_object__Taxi: DynaSpec = new DynaTaxi(this); break;
                case DynaType.game_object__Teleport: DynaSpec = new DynaTeleport(this, Version); break;
                case DynaType.game_object__Vent: DynaSpec = new DynaVent(this); break;
                case DynaType.game_object__VentType: DynaSpec = new DynaVentType(this); break;
                case DynaType.game_object__bungee_drop: DynaSpec = new DynaBungeeDrop(this); break;
                case DynaType.game_object__bungee_hook: DynaSpec = new DynaBungeeHook(this); break;
                case DynaType.game_object__flame_emitter: DynaSpec = new DynaFlameEmitter(this); break;
                case DynaType.game_object__talk_box: DynaSpec = new DynaTalkBox(this); break;
                case DynaType.game_object__task_box: DynaSpec = new DynaTaskBox(this); break;
                case DynaType.game_object__text_box: DynaSpec = new DynaTextBox(this); break;
                case DynaType.hud__meter__font: DynaSpec = new DynaHudMeterFont(this, Version); break;
                case DynaType.hud__meter__unit: DynaSpec = new DynaHudMeterUnit(this); break;
                case DynaType.hud__model: DynaSpec = new DynaHudModel(this); break;
                case DynaType.hud__text: DynaSpec = new DynaHudText(this); break;
                case DynaType.interaction__Launch: DynaSpec = new DynaInteractionLaunch(this); break;
                case DynaType.pointer: DynaSpec = new DynaPointer(this); break;
                case DynaType.Checkpoint:
                case DynaType.Effect__particle_generator:
                case DynaType.Enemy__SB:
                case DynaType.Interest_Pointer:
                case DynaType.audio__conversation:
                case DynaType.camera__binary_poi:
                case DynaType.camera__preset:
                case DynaType.camera__transition_path:
                case DynaType.camera__transition_time:
                case DynaType.effect__BossBrain:
                case DynaType.effect__Flamethrower:
                case DynaType.effect__LightEffectFlicker:
                case DynaType.effect__LightEffectStrobe:
                case DynaType.effect__RumbleBoxEmitter:
                case DynaType.effect__ScreenWarp:
                case DynaType.effect__Splash:
                case DynaType.effect__Waterhose:
                case DynaType.effect__grass:
                case DynaType.effect__light:
                case DynaType.effect__spark_emitter:
                case DynaType.effect__spotlight:
                case DynaType.effect__uber_laser:
                case DynaType.effect__water_body:
                case DynaType.game_object__FreezableObject:
                case DynaType.game_object__Grapple:
                case DynaType.game_object__Hangable:
                case DynaType.game_object__RaceTimer:
                case DynaType.game_object__RubbleGenerator:
                case DynaType.game_object__Turret:
                case DynaType.game_object__bullet_mark:
                case DynaType.game_object__bullet_time:
                case DynaType.game_object__camera_param_asset:
                case DynaType.game_object__dash_camera_spline:
                case DynaType.game_object__laser_beam:
                case DynaType.game_object__rband_camera_asset:
                case DynaType.game_object__train_car:
                case DynaType.game_object__train_junction:
                case DynaType.hud__image:
                case DynaType.interaction__IceBridge:
                case DynaType.interaction__Lift:
                case DynaType.interaction__SwitchLever:
                case DynaType.interaction__Turn:
                case DynaType.logic__FunctionGenerator:
                case DynaType.logic__reference:
                case DynaType.npc__CoverPoint:
                case DynaType.npc__NPC_Custom_AV:
                case DynaType.npc__group:
                case DynaType.ui__box:
                case DynaType.ui__controller:
                case DynaType.ui__image:
                case DynaType.ui__model:
                case DynaType.ui__text:
                case DynaType.ui__text__userstring:
                case DynaType.Unknown1:
                case DynaType.Unknown2:
                case DynaType.Unknown3:
                case DynaType.Unknown4:
                case DynaType.Unknown5:
                case DynaType.Unknown6:
                case DynaType.Unknown7:
                case DynaType.Unknown8:
                case DynaType.Unknown9:
                case DynaType.Unknown10:
                case DynaType.Unknown12:
                case DynaType.Unknown13:
                case DynaType.Unknown14:
                case DynaType.Unknown15:
                    DynaSpec = new DynaDefault(this, Data.Length - LinkCount * Link.sizeOfStruct - 0x10); break;
                default:
                    throw new System.Exception("Unknown DYNA type: " + Type.ToString("X8"));
            }

            if (reset)
            {
                List<byte> dataBefore = Data.Take(0x10).ToList();
                for (int i = 0; i < DynaSpec.StructSize; i++)
                    dataBefore.Add(0);
                dataBefore.AddRange(Data.Skip(EventStartOffset));
                Data = dataBefore.ToArray();
            }

            if (IsRenderableClickable)
            {
                CreateTransformMatrix();
                if (!renderableAssetSetCommon.Contains(this))
                    renderableAssetSetCommon.Add(this);
            }
            else if (renderableAssetSetCommon.Contains(this))
                renderableAssetSetCommon.Remove(this);
        }
        
        [Category("Dynamic")]
        public DynaType Type
        {
            get => (DynaType)ReadUInt(0x8);
            set
            {
                Write(0x8, (uint)value);
                SetDynaSpecific(true);
            }
        }
        
        [Category("Dynamic")]
        public short Version
        {
            get => ReadShort(0xC);
            set => Write(0xC, value);
        }

        [Category("Dynamic")]
        public short Handle
        {
            get => ReadShort(0xE);
            set => Write(0xE, value);
        }

        [Category("Dynamic"), DisplayName("Properties")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DynaBase DynaSpec { get; set; }

        public static bool dontRender = false;

        [Browsable(false)]
        public bool IsRenderableClickable { get => DynaSpec.IsRenderableClickable; }

        [Browsable(false)]
        public Vector3 Position => new Vector3(PositionX, PositionY, PositionZ);
        [Browsable(false)]
        public float PositionX { get => DynaSpec.PositionX; set => DynaSpec.PositionX = value; }
        [Browsable(false)]
        public float PositionY { get => DynaSpec.PositionY; set => DynaSpec.PositionY = value; }
        [Browsable(false)]
        public float PositionZ { get => DynaSpec.PositionZ; set => DynaSpec.PositionZ = value; }
        [Browsable(false)]
        public float Yaw { get => DynaSpec.Yaw; set => DynaSpec.Yaw = value; }
        [Browsable(false)]
        public float Pitch { get => DynaSpec.Pitch; set => DynaSpec.Pitch = value; }
        [Browsable(false)]
        public float Roll { get => DynaSpec.Roll; set => DynaSpec.Roll = value; }
        [Browsable(false)]
        public float ScaleX { get => DynaSpec.ScaleX; set => DynaSpec.ScaleX = value; }
        [Browsable(false)]
        public float ScaleY { get => DynaSpec.ScaleY; set => DynaSpec.ScaleY = value; }
        [Browsable(false)]
        public float ScaleZ { get => DynaSpec.ScaleZ; set => DynaSpec.ScaleZ = value; }

        public float? IntersectsWith(Ray ray)
        {
            if (dontRender || isInvisible)
                return null;

            return DynaSpec.IntersectsWith(ray);
        }
        
        public void CreateTransformMatrix()
        {
            DynaSpec.CreateTransformMatrix();
        }

        public void Draw(SharpRenderer renderer)
        {
            if (!isSelected && (dontRender || isInvisible))
                return;

            DynaSpec.Draw(renderer, isSelected);
        }

        public BoundingBox GetBoundingBox()
        {
            return DynaSpec.GetBoundingBox();
        }

        public float GetDistance(Vector3 cameraPosition)
        {
            return DynaSpec.GetDistance(cameraPosition);
        }

        public BoundingSphere GetObjectCenter()
        {
            return DynaSpec.GetObjectCenter();
        }
    }
}
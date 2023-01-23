using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using BoneLib;
using Random = UnityEngine.Random;
using RageQuit.Effects;
using RageQuit.API;
using System.Linq;
using SLZ.Combat;
using System.Threading.Tasks;
using SLZ.Props;
using SLZ.Bonelab;
using System.Threading;
using SLZ.Marrow.Pool;
using SLZ.Marrow.Data;
using SLZ.Marrow.Warehouse;
using BoneLib.Nullables;

[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonPriority(-1000)]
[assembly: MelonInfo(typeof(RageQuit.RageMain),"RageQuit","1.0.0","joe swan#2228")]

namespace RageQuit
{
    /// <summary>
    /// RageMain class. do NOT modify anything here unless you know exactly what you are doing.
    /// </summary>
    public class RageMain :  MelonMod
    {
        GameObject rootObject;
        /// <summary>
        /// the main effect handler. recommended to use API methods instead.
        /// </summary>
        public static EffectHandler effectHandler;
        /// <summary>
        /// the last effect that was ran. API implementation planned in the future.
        /// </summary>
        public static RageEffect lastEffect;

        private RandomAvatar randomAvatar;

        #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override void OnInitializeMelon()
        #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            //Hooking
            Hooking.OnLevelInitialized += new Action<LevelInfo>(LevelLoaded);
        }
        private void LevelLoaded(LevelInfo i)
        {
            rootObject = new GameObject("RAGE_QUIT_MAIN");
            effectHandler = rootObject.AddComponent<EffectHandler>();

            RageAPI.NewEffect("Turn 180", true, 0, () => { Player.RotatePlayer(180); }, null);
            RageAPI.NewEffect("Ragdoll For 5 Seconds", false, 5, () => { Player.physicsRig.RagdollRig(); }, () => { Player.physicsRig.UnRagdollRig(); });
            RageAPI.NewEffect("Ragdoll For 10 Seconds", false, 10, () => { Player.physicsRig.RagdollRig(); }, () => { Player.physicsRig.UnRagdollRig(); });
            RageAPI.NewSingleRunEffect("SMASH INTO A WALL", () => { Player.physicsRig.torso.rbPelvis.AddForce(Player.playerHead.forward * 25,ForceMode.Impulse); }, null);
            Vector3 oldGravity = Physics.gravity;
            RageAPI.NewEffect("on da MOON", false, 30,() => { Physics.gravity = -0.01f * Vector3.one; }, () => { Physics.gravity = oldGravity; });
            RageAPI.NewEffect("i feel motion sick", false, 10, () => { Player.RotatePlayer(30); }, null);
            RageAPI.NewEffect("misclick i swear", false, 10, () => { Player.GetGunInHand(Player.rightHand)?.Fire(); Player.GetGunInHand(Player.leftHand)?.Fire(); }, null,true);
            RageAPI.NewEffect("i dont feel so good", true, 0, () => { UnityEngine.Object.FindObjectOfType<Player_Health>().TAKEDAMAGE(float.MaxValue); }, null);
            RageAPI.NewEffect("i swear i didnt press mag eject", true, 0, () => { Player.GetGunInHand(Player.rightHand)?.EjectCartridge(); Player.GetGunInHand(Player.leftHand)?.EjectCartridge(); }, null);
            RageAPI.NewEffect("wait hold on a second", true, 5, () => { Player.physicsRig.torso.rbPelvis.isKinematic = true; }, () => { Player.physicsRig.torso.rbPelvis.isKinematic = false; },true);
            RageAPI.NewEffect("youtube.com/watch?v=kKEIVxsrF2E", true, 0, () => { Player.rightHand.enabled = false; Player.leftHand.enabled = false; } ,() => { Player.rightHand.enabled = true; Player.leftHand.enabled = true; });
            RageAPI.NewSingleRunEffect("Fake Crash (5 seconds)", () => { Thread.Sleep(5000); },null);
            RageAPI.NewSingleRunEffect("Fake Crash (10 seconds)", () => { Thread.Sleep(10000); },null);
            RageAPI.NewSingleRunEffect("what up, son!", () => {

                Transform head = Player.playerHead.transform;
                string barcode = "c1534c5a-3fd8-4d50-9eaf-0695466f7264";
                SpawnableCrateReference reference = new SpawnableCrateReference(barcode);

                Spawnable spawnable = new Spawnable()
                {
                    crateRef = reference
                };

                AssetSpawner.Register(spawnable);
                AssetSpawner.Spawn(spawnable, head.position + head.forward, default, new BoxedNullable<Vector3>(Vector3.one), false, new BoxedNullable<int>(null), null, null);
            }, null);
            RageAPI.NewEffect("I SAID WHATS UP SON.", false, 2.5f, () => {

                Transform head = Player.playerHead.transform;
                string barcode = "c1534c5a-3fd8-4d50-9eaf-0695466f7264";
                SpawnableCrateReference reference = new SpawnableCrateReference(barcode);

                Spawnable spawnable = new Spawnable()
                {
                    crateRef = reference
                };

                AssetSpawner.Register(spawnable);
                AssetSpawner.Spawn(spawnable, head.position + head.forward, default, new BoxedNullable<Vector3>(Vector3.one), false, new BoxedNullable<int>(null), null, null);
            }, null, false);
            RageAPI.NewEffect("NullReferenceException", false, 2.5f, () => {

                Transform head = Player.playerHead.transform;
                string barcode = "c1534c5a-2775-4009-9447-22d94e756c6c";
                SpawnableCrateReference reference = new SpawnableCrateReference(barcode);

                Spawnable spawnable = new Spawnable()
                {
                    crateRef = reference
                };

                AssetSpawner.Register(spawnable);
                AssetSpawner.Spawn(spawnable, head.position + head.forward, default, new BoxedNullable<Vector3>(Vector3.one), false, new BoxedNullable<int>(null), null, null);
            }, null, false);
            RageAPI.NewEffect("crates CRATES CRRRAAAATEEEESSSS!!!", false, 5f, () => {

                Transform head = Player.playerHead.transform;
                string barcode = "c1534c5a-5be2-49d6-884e-d35c576f6f64";
                SpawnableCrateReference reference = new SpawnableCrateReference(barcode);

                Spawnable spawnable = new Spawnable()
                {
                    crateRef = reference
                };

                AssetSpawner.Register(spawnable);
                AssetSpawner.Spawn(spawnable, head.position + head.forward, default, new BoxedNullable<Vector3>(Vector3.one), false, new BoxedNullable<int>(null), null, null);
            }, null, false);
            RageAPI.NewEffect("invert velocity", true, 0f, () => {
                Player.physicsRig.torso.rbPelvis.velocity *= -1;
            }, null, false);

            //tell rageapi mods that ragequit has loaded.
            //RageAPI.rageload();
        }
    }
    /// <summary>
    /// The MonoBehaviour in charge of effects.
    /// </summary>
    [RegisterTypeInIl2Cpp]

    public class EffectHandler : MonoBehaviour
    {
        private bool debug = true;
        /// <summary>
        /// When the next effect should be ran.
        /// </summary>
        public float next;
        /// <summary>
        /// Base Rate.
        /// </summary>
        public float delay = 5f;
        /// <summary>
        /// Base Rates Random Delay (base + random[min,max])
        /// </summary>
        public List<float> randDelayRange = new List<float>();
        #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public EffectHandler(IntPtr popcornRepellantTrees) : base(popcornRepellantTrees) { }
        #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        //level start
        void Start()
        {
            randDelayRange.Add(-5f);
            randDelayRange.Add(5f);
            next = Time.unscaledTime + delay;
        }

        void Update()
        {
            if(Time.unscaledTime >= next)
            {
                Dictionary<string, RageEffect> dictionary = RageEffects.effectsList;
                System.Random random = new System.Random();
                int index = random.Next(dictionary.Count);

                string key = dictionary.Keys.ElementAt(index);
                RageEffect value = dictionary.Values.ElementAt(index);

                KeyValuePair<string, RageEffect> effectEntry = dictionary.ElementAt(index);
                if (debug) MelonLogger.Msg("Calling Event: " + effectEntry.Key + " for duration: " + effectEntry.Value.duration);
                effectEntry.Value.fireEvent();

                delay += Random.RandomRange(randDelayRange[0], randDelayRange[1]);
                next = Time.unscaledTime + delay;
            }
        }
    }
}

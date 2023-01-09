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

[assembly: MelonInfo(typeof(RageQuit.RageMain),"RageQuit","dont.even.try","joe swan#2228")]

namespace RageQuit
{
    public class RageMain :  MelonMod
    {
        GameObject rootObject;
        public static EffectHandler effectHandler;
        public override void OnInitializeMelon()
        {
            //Hooking
            Hooking.OnLevelInitialized += new Action<LevelInfo>(LevelLoaded);
        }
        private void LevelLoaded(LevelInfo i)
        {
            rootObject = new GameObject("RAGE_QUIT_MAIN");
            effectHandler = rootObject.AddComponent<EffectHandler>();

            RageAPI.NewEffect("Turn 180", true, 0, new Action(() => { Player.RotatePlayer(180); }), null);
            RageAPI.NewEffect("Ragdoll For 5 Seconds", false, 5, new Action(() => { Player.physicsRig.RagdollRig(); }), new Action(() => { Player.physicsRig.UnRagdollRig(); }));
            RageAPI.NewEffect("Ragdoll For 10 Seconds", false, 10, new Action(() => { Player.physicsRig.RagdollRig(); }), new Action(() => { Player.physicsRig.UnRagdollRig(); }));
            RageAPI.NewSingleRunEffect("SMASH INTO A WALL", new Action(() => { Player.physicsRig.torso.rbPelvis.AddForce(Player.playerHead.forward * 25); }), null);
            Vector3 oldGravity = Physics.gravity;
            RageAPI.NewEffect("on da MOON", false, 30, new Action(() => { Physics.gravity = -0.01f * Vector3.one; }), new Action(() => { Physics.gravity = oldGravity; }));
            RageAPI.NewEffect("i feel motion sick", false, 10, new Action(() => { Player.RotatePlayer(30); }), null);
            RageAPI.NewEffect("misclick i swear", false, 10, new Action(() => { Player.GetGunInHand(Player.rightHand)?.Fire(); Player.GetGunInHand(Player.leftHand)?.Fire(); }), null);
            RageAPI.NewEffect("i dont feel so good", true, 0, new Action(() => { UnityEngine.Object.FindObjectOfType<Player_Health>().TAKEDAMAGE(float.MaxValue); }), null);
            RageAPI.NewEffect("i swear i didnt press mag eject", true, 0, new Action(() => { Player.GetGunInHand(Player.rightHand)?.EjectCartridge(); Player.GetGunInHand(Player.leftHand)?.EjectCartridge(); }), null);
        }
    }

    [RegisterTypeInIl2Cpp]
    public class EffectHandler : MonoBehaviour
    {
        private bool debug = true;

        public float next;
        public float delay = 5f;
        public List<float> randDelayRange = new List<float>();
        public EffectHandler(IntPtr popcornRepellantTrees) : base(popcornRepellantTrees) { }
        //level start
        void Start()
        {
            randDelayRange.Add(-5f);
            randDelayRange.Add(10f);
            next = Time.time + delay;
        }

        void Update()
        {
            if(Time.time >= next)
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
                next = Time.time + delay;
            }
        }
    }
}

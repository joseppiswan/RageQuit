using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using BoneLib;
using Random = UnityEngine.Random;
using RageQuit.Effects;
using RageQuit.API;
using System.Linq;

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
            RageAPI.SetBaseRate(7);
            RageAPI.SetRateRandomRange(0, 0);
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

using System;
using System.Collections.Generic;
using MelonLoader;
using UnityEngine;
using BoneLib;
using Random = UnityEngine.Random;
using RageQuit.Effects;
using RageQuit.API;

[assembly: MelonInfo(typeof(RageQuit.RageMain),"RageQuit","dont.even.try","joe swan#2228")]

namespace RageQuit
{
    public class RageMain :  MelonMod
    {
        GameObject rootObject;
        EffectHandler effectHandler;
        public override void OnInitializeMelon()
        {
            //Hooking
            Hooking.OnLevelInitialized += new Action<LevelInfo>(LevelLoaded);
        }
        private void LevelLoaded(LevelInfo i)
        {
            rootObject = new GameObject("RAGE_QUIT_MAIN");
            effectHandler = rootObject.AddComponent<EffectHandler>();

            RageAPI.NewEffect("Debug Thing", 5, new Action(() => { Player.RotatePlayer(32); }));
        }
    }

    [RegisterTypeInIl2Cpp]
    public class EffectHandler : MonoBehaviour
    {
        private bool debug = true;

        private float next;
        private float delay = 15f;
        private List<float> randDelayRange = new List<float>();
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
                foreach(KeyValuePair<string,RageEffect> effectEntry in RageEffects.effectsList)
                {
                    if (debug) MelonLogger.Msg("Calling Event: " + effectEntry.Key + " for duration: " + effectEntry.Value.duration);
                    effectEntry.Value.fireEvent();
                }

                delay += Random.RandomRange(randDelayRange[0], randDelayRange[1]);
                next = Time.time + delay;
            }
        }
    }
}

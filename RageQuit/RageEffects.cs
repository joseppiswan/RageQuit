using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//todo: i know what this is
namespace RageQuit.Effects
{
    class RageEffects
    {
        public static Dictionary<string, RageEffect> effectsList = new Dictionary<string,RageEffect>();

        public static RageEffect AddEffect(string name, float duration, Action runAction)
        {
            RageEffect newEffect = new RageEffect();
            newEffect.duration = duration;
            newEffect.effectAction = runAction;
            effectsList.Add(name, newEffect);
            return newEffect;
        }
    }

    

    public class RageEffect
    {
        public float duration;
        public Action effectAction;
        private float next;

        public void fireEvent()
        {
            next = Time.time + duration;
            Task h = doLoop();
            //MelonCoroutines.Start(loopRun());
        }
        private async Task doLoop()
        {
            for (int i = 0; i < duration * 10; i++)
            {
                effectAction();
                await Task.Delay(100);
            }
        }
        private IEnumerator loopRun()
        {
            //while (true)
            //{
            //    if (Time.time >= next)
            //    {
            //        MelonCoroutines.Stop(loopRun());
            //        break;
            //    }
            //    //MelonCoroutines.Start(loopRun());
            //}
            yield return null;
        }
    }
}

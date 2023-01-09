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

        public static RageEffect AddEffect(string name, bool single, float duration, Action runAction, Action afterAction)
        {
            RageEffect newEffect = new RageEffect();
            newEffect.single = single;
            newEffect.duration = duration;
            newEffect.effectAction = runAction;
            newEffect.afterAction = afterAction;
            effectsList.Add(name, newEffect);
            return newEffect;
        }
    }

    

    public class RageEffect
    {
        public bool single;
        public float duration;
        public Action effectAction;
        public Action afterAction;
        private float next;

        public void fireEvent()
        {
            next = Time.time + duration;
            if (single) {
                effectAction();
                if (afterAction != null)
                {
                    afterAction();
                }
            } else { 
                Task h = doLoop();
            }
        }
        private async Task doLoop()
        {
            for (int i = 0; i < duration * 10; i++)
            {
                effectAction();
                await Task.Delay(100);
            }
            if (afterAction != null)
            {
                afterAction();
            }
        }
    }
}

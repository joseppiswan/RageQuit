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
        public static RageEffect AddEffect(string name, bool single, float duration, Action runAction, Action afterAction, bool slow)
        {
            RageEffect newEffect = new RageEffect();
            newEffect.single = single;
            newEffect.duration = duration;
            newEffect.slow = true;
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
        public bool slow;
        public Action effectAction;
        public Action afterAction;

        public void fireEvent()
        {
            if (single) {
                effectAction();
                if (afterAction != null)
                {
                    afterAction();
                }
            } else { 
                Task h = !slow ? doLoop() : doSlowLoop();
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
        private async Task doSlowLoop()
        {
            for (int i = 0; i < duration; i++)
            {
                effectAction();
                await Task.Delay(1000);
            }
            if (afterAction != null)
            {
                afterAction();
            }
        }
    }
}

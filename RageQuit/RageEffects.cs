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

    
    /// <summary>
    /// The class that handles the effect stuff.
    /// </summary>
    public class RageEffect
    {
        /// <summary>
        /// run once?
        /// </summary>
        public bool single;
        /// <summary>
        /// how long to run for
        /// </summary>
        public float duration;
        /// <summary>
        /// makes the effect run slower when running repeatedly.
        /// </summary>
        public bool slow;
        /// <summary>
        /// the action called when fireEvent is ran.
        /// </summary>
        public Action effectAction;
        /// <summary>
        /// the action called  after the effect has ran.
        /// </summary>
        public Action afterAction;

        /// <summary>
        /// run the effect.
        /// </summary>
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

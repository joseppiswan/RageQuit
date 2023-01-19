using BoneLib;
using MelonLoader;
using RageQuit.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RageQuit.API
{
    /// <summary>
    /// The Official RageQuit API.
    /// </summary>
    public static class RageAPI
    {
        ///// <summary>
        ///// Fires when RageQuit has loaded.
        ///// </summary>
        //public static event Action onRageLoaded;
        /// <summary>
        /// Fires all events in the RageEffects.effectsList dictionary.
        /// </summary>
        /// <param name="debug">Logs debug message with effect name and duration.</param>
        public static void Chaos(bool debug)
        {
            foreach(KeyValuePair<string,RageEffect> pair in RageEffects.effectsList)
            {
                if (debug) MelonLogger.Msg("Firing Effect: " +pair.Key+", duration: " + pair.Value.duration);
                pair.Value.fireEvent();
            }
        }
        /// <summary>
        /// Creates a new effect to be used by RageQuit. This method will add the effect to the base mod automatically. RageEffects.AddEffect will still do the same thing as this method, this is just in the API class instead.
        /// </summary>
        /// <param name="name">Name of effect.</param>
        /// <param name="single">Decides whether the effect will be fired once or repeated for duration seconds.</param>
        /// <param name="duration">Duration of the effect (in seconds)</param>
        /// <param name="runAction">The action that gets ran when RageEffect.fireEvent() is called.</param>
        /// <param name="afterAction">The action that gets ran after the effect is complete. Useful for undoing things doing in runAction, for example, ragdolling. Set to null to not be ran.</param>
        /// <returns>The new RageEffect created.</returns>
        public static RageEffect NewEffect(string name, bool single, float duration, Action runAction, Action afterAction)
        {
            return RageEffects.AddEffect(name, single, duration, runAction, afterAction);
        }
        /// <summary>
        /// Creates a new effect to be used by RageQuit. This method will add the effect to the base mod automatically. RageEffects.AddEffect will still do the same thing as this method, this is just in the API class instead.
        /// </summary>
        /// <param name="name">Name of effect.</param>
        /// <param name="single">Decides whether the effect will be fired once or repeated for duration seconds.</param>
        /// <param name="duration">Duration of the effect (in seconds)</param>
        /// <param name="runAction">The action that gets ran when RageEffect.fireEvent() is called.</param>
        /// <param name="afterAction">The action that gets ran after the effect is complete. Useful for undoing things doing in runAction, for example, ragdolling. Set to null to not be ran.</param>
        /// <param name="slow">instead of running the action every 100 ms, this does it every second. *better* not perfect for things that shouldnt be called every 1/10 of a second</param>
        /// <returns>The new RageEffect created.</returns>
        public static RageEffect NewEffect(string name, bool single, float duration, Action runAction, Action afterAction,bool slow)
        {
            return RageEffects.AddEffect(name, single, duration, runAction, afterAction,slow);
        }

        /// <summary>
        /// Sets the base rate for effects to happen. Default=15. (1 effect/15s). This WILL be affected by the rate range
        /// </summary>
        /// <param name="rate">The base rate at which effects happen per x seconds (plus random)</param>
        public static void SetBaseRate(float rate) {
            RageMain.effectHandler.delay = rate;
        }

        /// <summary>
        /// Sets the effect rates random range (1 effect/[x + random]s)
        /// </summary>
        /// <param name="smallest">The minimum number</param>
        /// <param name="biggest">The highest number</param>
        public static void SetRateRandomRange(float smallest, float biggest)
        {
            List<float> newRange = new List<float>();
            newRange.Add(smallest);
            newRange.Add(biggest);
            RageMain.effectHandler.randDelayRange = newRange;
        }
        /// <summary>
        /// Creates a new effect thats ran once instead of a duration.
        /// </summary>
        /// <param name="name">Name of effect.</param>
        /// <param name="runAction">The action that gets ran when RageEffect.fireEvent() is called.</param>
        /// <param name="afterAction">The action that gets ran after the effect is complete. Useful for undoing things doing in runAction, for example, ragdolling. Set to null to not be ran.</param>
        /// <returns>The new RageEffect created.</returns>
        public static RageEffect NewSingleRunEffect(string name, Action runAction, Action afterAction)
        {
            return RageEffects.AddEffect(name, true, 0, runAction, afterAction);
        }
        /// <summary>
        /// Creates a new effect thats ran for duration seconds instead of once.
        /// </summary>
        /// <param name="name">Name of effect.</param>
        /// <param name="duration">Duration of the effect (in seconds)</param>
        /// <param name="runAction">The action that gets ran when RageEffect.fireEvent() is called.</param>
        /// <param name="afterAction">The action that gets ran after the effect is complete. Useful for undoing things doing in runAction, for example, ragdolling. Set to null to not be ran.</param>
        /// <returns>The new RageEffect created.</returns>
        public static RageEffect NewDurationEffect(string name, float duration, Action runAction, Action afterAction)
        {
            return RageEffects.AddEffect(name, false, duration, runAction, afterAction);
        }

        /// <summary>
        /// Gets the main EffectHandler.
        /// </summary>
        /// <returns>The effectHandler of RageMain. This is the main EffectHandler used by the mod.</returns>
        public static EffectHandler GetEffectHandler()
        {
            return RageMain.effectHandler;
        }
        /// <summary>
        /// Gets every EffectHandler in the scene. Uses FindObjectsOfType each call.
        /// </summary>
        /// <returns>an EffectHandler[] with every EffectHandler found in the scene.</returns>
        public static EffectHandler[] GetEffectHandlers()
        {
            return UnityEngine.Object.FindObjectsOfType<EffectHandler>();
        }
        /// <summary>
        /// Calls the next random effect.
        /// </summary>
        /// <param name="affectTimer">Decides whether this timer should be refreshed. For exapmle, timer is at 2 seconds left before an effect, setting this to true would reset it to the random delay value.</param>
        public static void CallNextEffect(bool affectTimer)
        {
            EffectHandler handler = GetEffectHandler();
            handler.next = Time.unscaledTime;
            if (affectTimer)
                handler.next = Time.unscaledTime + handler.delay;
        }
        /// <summary>
        /// Sets the main EffectHandler to enabled or disabled
        /// </summary>
        /// <param name="enabled">Enabled or disabled</param>
        public static void SetEnabled(bool enabled)
        {
            GetEffectHandler().enabled = enabled;
        }
        /// <summary>
        /// Sets the given EffectHandler to enabled or disabled
        /// </summary>
        /// <param name="handler">The handler to be affected</param>
        /// <param name="enabled">Enabled or disabled</param>
        public static void SetEnabled(EffectHandler handler, bool enabled)
        {
            handler.enabled = enabled;
        }

        //internal static void rageload()
        //{
        //    SafeActions.InvokeActionSafe(onRageLoaded);
        //    //onRageLoaded();
        //}
    }
}

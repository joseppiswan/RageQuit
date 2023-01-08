using MelonLoader;
using RageQuit.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageQuit.API
{
    class RageAPI
    {
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
        /// <param name="name">Name of effet.</param>
        /// <param name="duration">Duration of the effect (in seconds)</param>
        /// <param name="runAction">The action that gets ran when RageEffect.fireEvent() is called.</param>
        public static RageEffect NewEffect(string name, float duration, Action runAction)
        {
            return RageEffects.AddEffect(name, duration, runAction);
        }
    }
}

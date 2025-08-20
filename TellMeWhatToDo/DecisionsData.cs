using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsData(
            IList<DecisionsData.Option> options,
            IList<DecisionsData.Context> contexts ) {
    public partial class Option(
                string info,
                float weight,
                int repeatMinimumAmount,
                int repeatMaximumAmount,
                float repeatWeightScale,
                float contiguousRepeatWeightScale,
                int afterRepeatMinimumDelay,
                int afterRepeatMaximumDelay,
                //IDictionary<string, float> contextWeightAdditive,
                IDictionary<string, float> contextPreference,
                IDictionary<string[], float> contextSetPreference,
                float unmatchedContextSetPreference ) {
        public string Info { get; set; } = info;
        public float Weight { get; set; } = weight;
        public int RepeatMinimumAmount { get; set; } = repeatMinimumAmount;
        public int RepeatMaximumAmount { get; set; } = repeatMaximumAmount;
        public float IsRepeatingWeightScale { get; set; } = repeatWeightScale;
        public float IsRepeatingContiguouslyWeightScale { get; set; } = contiguousRepeatWeightScale;
        public int AfterRepeatMinimumSteps { get; set; } = afterRepeatMinimumDelay;
        public int AfterRepeatMaximumSteps { get; set; } = afterRepeatMaximumDelay;
        //public IDictionary<string, float> ContextWeightAdditive { get; set; } = contextWeightAdditive;
        public IDictionary<string, float> ContextPreference { get; set; } = contextPreference;
        public IDictionary<string[], float> ContextSetPreference { get; set; } = contextSetPreference;
        public float UnmatchedContextSetPreference { get; set; } = unmatchedContextSetPreference;
    }
    
    public partial class Context(
                string name,
                //float activeWeightAdditive,
                //float inactiveWeightAdditive,
                float density,
                int minDuration,
                int maxDuration,
                int minCooldown,
                IDictionary<string, float> coContextWeightScales ) {
        public string Name { get; set; } = name;
        //public float ActiveWeightAdditive { get; set; } = activeWeightAdditive;
        //public float InactiveWeightAdditive { get; set; } = inactiveWeightAdditive;
        public float Density { get; set; } = density;
        public int MinDuration { get; set; } = minDuration;
        public int MaxDuration { get; set; } = maxDuration;
        public int MinCooldown { get; set; } = minCooldown;
        public IDictionary<string, float> CoContextWeightScales { get; set; } = coContextWeightScales;
    }



    public IList<Option> Options { get; set; } = options;

    public IList<Context> Contexts { get; set; } = contexts;


    private IDictionary<string, Context> _ContextMap = new Dictionary<string, Context>(
        contexts.Select( c => new KeyValuePair<string, Context>(c.Name, c) )
    );



    public Context? GetContext( string name ) {
        if( this._ContextMap.TryGetValue(name, out Context? ctx) ) {
            return ctx;
        }
        return null;
    }
}

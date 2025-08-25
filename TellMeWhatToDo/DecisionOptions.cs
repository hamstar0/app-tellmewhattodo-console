using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionOptions(
            IList<DecisionOptions.OptionDef> options,
            IList<DecisionOptions.ContextDef> contexts ) {
    public partial class OptionDef(
                string[] isOfContext,
                string info,
                float weight,
                int repeatMinimumAmount,
                int repeatMaximumAmount,
                float repeatWeightScale,
                float contiguousRepeatWeightScale,
                int afterRepeatMinimumDelay,
                int afterRepeatMaximumDelay,
                //IDictionary<string, float> contextWeightAdditive,
                IDictionary<string, float> associatedContextPreference,
                IDictionary<string[], float> associatedContextSetPreference,
                float unmatchedAssociatedContextSetPreference ) {
        public string[] IsOfContext { get; set; } = isOfContext;
        public string Info { get; set; } = info;
        public float Weight { get; set; } = weight;
        public int RepeatMinimumAmount { get; set; } = repeatMinimumAmount;
        public int RepeatMaximumAmount { get; set; } = repeatMaximumAmount;
        public float IsRepeatingWeightScale { get; set; } = repeatWeightScale;
        public float IsRepeatingContiguouslyWeightScale { get; set; } = contiguousRepeatWeightScale;
        public int AfterRepeatMinimumSteps { get; set; } = afterRepeatMinimumDelay;
        public int AfterRepeatMaximumSteps { get; set; } = afterRepeatMaximumDelay;
        //public IDictionary<string, float> ContextWeightAdditive { get; set; } = contextWeightAdditive;
        public IDictionary<string, float> AssociatedContextPreference { get; set; } = associatedContextPreference;
        public IDictionary<string[], float> AssociatedContextSetPreference { get; set; } = associatedContextSetPreference;
        public float UnmatchedAssociatedContextSetPreference { get; set; } = unmatchedAssociatedContextSetPreference;
    }
    
    public partial class ContextDef(
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



    public IList<OptionDef> Options { get; set; } = options;

    public IList<ContextDef> Contexts { get; set; } = contexts;


    private IDictionary<string, ContextDef> _ContextMap = new Dictionary<string, ContextDef>(
        contexts.Select( c => new KeyValuePair<string, ContextDef>(c.Name, c) )
    );



    public ContextDef? GetContextData( string name ) {
        if( this._ContextMap.TryGetValue(name, out ContextDef? ctx) ) {
            return ctx;
        }
        return null;
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


/// <summary>
/// Defines decision making template data for all options of the given contexts that pertain to
/// another, unspecified (parent) context.
/// </summary>
/// <param name="connectionName">Name for connector.</param>
/// <param name="subOptionContextsPreferences">How preferred any of the given context sets (of the
/// parent) may be to the given (sub) Option.</param>
///// <param name="unmatchedSubContextsPreference">How preferred not having any of the given context
/////sets may not be to the given (sub) Option.</param>
/// <param name="maxDepthByContext">How much depth is allowed by this given sub Option. Defaults to
/// 0: Immediate depth only.</param>
public class DecisionSubOption(
            string connectionName,
            IDictionary<string[], float> subOptionContextsPreferences,
            //float? unmatchedSubContextsPreference,
            IDictionary<string[], float>? maxDepthByContext ) {
    public string? ConnectionName { get; set; } = connectionName;
    public IDictionary<string[], float> SubOptionContextsPreferences { get; set; } = subOptionContextsPreferences;
    //public float? UnmatchedSubContextsPreference { get; set; } = unmatchedSubContextsPreference;
    public IDictionary<string[], float>? MaxDepthByContext { get; set; } = maxDepthByContext;



    public float ComputeWeight( DecisionOption option ) {
        float? weight = null;

        foreach( (string[] ctxs, float pref) in this.SubOptionContextsPreferences ) {
            if( option.HasAllContexts(ctxs) ) {
                weight = weight is not null
                    ? weight * pref
                    : pref;
            }
        }

        return weight ?? 0f;
    }
}

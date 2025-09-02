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
/// <param name="parentContextsPreferences">How preferred any of the given context sets (of the
/// parent) may be to the given (sub) Option.</param>
///// <param name="unmatchedSubContextsPreference">How preferred not having any of the given context
/////sets may not be to the given (sub) Option.</param>
public class DecisionSubOptionSlotDef(
            string connectionName,
            IDictionary<string[], float> parentContextsPreferences
            //float? unmatchedSubContextsPreference,
            ) {
    public string? ConnectionName { get; set; } = connectionName;
    public IDictionary<string[], float> ParentContextsPreferences { get; set; } = parentContextsPreferences;
    //public float? UnmatchedSubContextsPreference { get; set; } = unmatchedSubContextsPreference;



    public float ComputeWeight( DecisionOption option ) {
        float? weight = null;

        foreach( (string[] ctxs, float pref) in this.ParentContextsPreferences ) {
            if( option.HasAllContexts(ctxs) ) {
                weight = weight is not null
                    ? weight * pref
                    : pref;
            }
        }

        return weight ?? 0f;
    }
}

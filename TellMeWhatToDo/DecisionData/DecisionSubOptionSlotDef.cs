using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


/// <summary>
/// Defines decision making template data for all options of the given contexts that pertain to
/// another, unspecified (parent) context. JSON-able.
/// </summary>
public class DecisionSubOptionSlotDef {
            //string connectionName,
            //IDictionary<string[], float> childContextsPreferences
            ////float? unfilledSlotPreference
            //) {
    /// <summary>Name for connector.</summary>
    public string ConnectionName { get; set; } = null!;
    
    /// <summary>Preferences for children by context sets.</summary>
    public IDictionary<string[], float> ChildContextsPreferences { get; set; } = null!;

    ///// <summary>How much preference (weight) exists to simply not give
    ///// the current slot any fillings.</summary>
    //public float? UnfilledSlotPreference { get; set; } = unfilledSlotPreference;



    public float ComputeWeightAsChildCandidate( DecisionOption option ) {
        float? weight = null;

        foreach( (string[] ctxs, float pref) in this.ChildContextsPreferences ) {
            if( option.HasAllContexts(ctxs) ) {
                weight = weight is not null
                    ? weight * pref
                    : pref;
            }
        }

        return weight ?? 0f;
    }
}

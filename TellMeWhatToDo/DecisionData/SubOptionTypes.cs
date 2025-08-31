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
/// <param name="subOptionContextsPreferences">How preferred any given choice of sub-Option by
/// the given context sets may be.</param>
///// <param name="unmatchedSubContextsPreference">How preferred all choices of sub-Option not of
///// the given context sets may be.</param>
/// <param name="maxDepthByContext">How preferred any given choice of sub-Option by
/// the given context sets may be.</param>
public class SubOptionTypes(
            string connectionName,
            IDictionary<string[], float> subOptionContextsPreferences,
            //float? unmatchedSubContextsPreference,
            IDictionary<string[], float> maxDepthByContext ) {
    public string? ConnectionName { get; set; } = connectionName;
    public IDictionary<string[], float> SubOptionContextsPreferences { get; set; } = subOptionContextsPreferences;
    //public float? UnmatchedSubContextsPreference { get; set; } = unmatchedSubContextsPreference;


    public float ComputeWeight( DecisionOption option ) {

    }
}

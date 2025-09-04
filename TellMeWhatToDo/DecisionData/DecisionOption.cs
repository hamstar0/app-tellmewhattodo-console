using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


/// <summary>
/// Defines a given available decision option. JSON-able.
/// </summary>
public partial class DecisionOption {
            //string[] contexts,
            //string name,
            //string? description,
            //float weight,
            //int? repeatMinimumAmount,
            //int? repeatMaximumAmount,
            //float? isRepeatingWeightScale,
            //float? isRepeatingContiguouslyWeightScale,
            //int? repeatIntermissionMinimumDelay,
            //float? repeatIntermissionWeight,
            ////IDictionary<string[], float> associatedContextsPreference,
            ////float? unmatchedAssociatedContextsPreference,
            //IList<DecisionOption.SubOptionSlotPreference>? subOptionsSlots ) {
    public class SubOptionSlotPreference {
        public string Name { get; set; } = null!;
        public float NonEmptyPreference { get; set; } = 0f;
    }



    /// <summary>
    /// List of contexts this Option pertains to. Sub-Options evaluate
    /// their weights against these contexts and their given weights.
    /// </summary>
    public string[] Contexts { get; set; } = null!;   // = contexts;

    /// <summary>Name.</summary>
    public string Name { get; set; } = null!;   // = name;

    /// <summary>Optional description.</summary>
    public string? Description { get; set; } = null;    // = description;

    /// <summary>Weight factor for RNG selection against other available Options.</summary>
    public float Weight { get; set; } = 0f; // = weight;

    /// <summary>Minimum number of successive repeats, once selected.
    /// Relevant when `isRepeatingContiguouslyWeightScale` or `afterRepeatMinimumDelay` are
    /// specified.</summary>
    public int? RepeatMinimumAmount { get; set; } = null;   // = repeatMinimumAmount;

    /// <summary>Maximum number of successive repeats. Randomized between
    /// `repeatMinimumAmount` (or 0, if none).</summary>
    public int? RepeatMaximumAmount { get; set; } = null;   // = repeatMaximumAmount;

    /// <summary>Weight factor for re-selecting the current Option,
    /// while repeating. Overrides `weight`.</summary>
    public float? IsRepeatingWeightScale { get; set; } = null;  // = isRepeatingWeightScale;

    /// <summary>Weight factor for re-selecting the current
    /// Option, while repeating in contiguous succession. Overrides `isRepeatingWeightScale` and
    /// `weight`.</summary>
    public float? IsRepeatingContiguouslyWeightScale { get; set; } = null;  // = isRepeatingContiguouslyWeightScale;

    /// <summary>Minimum number of Option selections within the
    /// current layer before a repeat of the current option resumes using its normal `weight` instead
    /// of `afterRepeatDowntimeWeight`.</summary>
    public int? RepeatIntermissionMinimumDelay { get; set; } = null;    // = repeatIntermissionMinimumDelay;

    /// <summary>Weight value to use instead of `weight` while in
    /// intermission from previous block of repeats. Defaults to 0.</summary>
    public float? RepeatIntermissionWeight { get; set; } = null;    // = repeatIntermissionWeight;

    ///// <summary>Applies additional weight scaling (multiplier)
    ///// for each given set of matched parent contexts.</summary>
    //public IDictionary<string[], float> AssociatedContextsPreference { get; set; } = null;    // = associatedContextsPreference;

    ///// <summary>Applies additional weight scaling
    ///// (multiplier) when no matched parent sets exist. If `associatedContextsPreference` has a
    ///// value, this defaults to 0. Otherwise, it is skipped.</summary>
    //public float? UnmatchedAssociatedContextSetPreference { get; set; } = null;   // = unmatchedAssociatedContextsPreference;

    /// <paramsummary>All available Options that can be generated to attach to the current
    /// (parent) Option.</summary>
    public IList<SubOptionSlotPreference>? SubOptionsSlots { get; set; } = null;    // = subOptionsSlots;



    public bool HasAllContexts( IList<string> contexts ) {
        return contexts.All( c => this.Contexts.Contains(c) );
    }

    public List<string> Render( string indent = "" ) {
        string line1 = indent + "Idea: " + this.Name;

        if( this.Description is not null ) {
            return new List<string>( [line1, indent + "Info: " + this.Description] );
        } else {
            return new List<string>( [line1] );
        }
    }
}

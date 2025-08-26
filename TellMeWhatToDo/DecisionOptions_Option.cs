using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionOptions {
    /// <summary>
    /// Defines a given available decision option. JSON-able.
    /// </summary>
    /// <param name="currentContexts">List of contexts this Option pertains to. Sub-Options evaluate
    /// their weights against these contexts and their given weights.</param>
    /// <param name="name">Name.</param>
    /// <param name="description">Optional description.</param>
    /// <param name="weight">Weight factor for RNG selection against other available Options.</param>
    /// <param name="repeatMinimumAmount">Minimum number of successive repeats, once selected.
    /// Relevant when `isRepeatingContiguouslyWeightScale` or `afterRepeatMinimumDelay` are
    /// specified.</param>
    /// <param name="repeatMaximumAmount">Maximum number of successive repeats. Randomized between
    /// `repeatMinimumAmount` (or 0, if none).</param>
    /// <param name="isRepeatingWeightScale">Weight factor for re-selecting the current Option,
    /// while repeating. Overrides `weight`.</param>
    /// <param name="isRepeatingContiguouslyWeightScale">Weight factor for re-selecting the current
    /// Option, while repeating in contiguous succession. Overrides `isRepeatingWeightScale` and
    /// `weight`.</param>
    /// <param name="repeatIntermissionMinimumDelay">Minimum number of Option selections within the
    /// current layer before a repeat of the current option resumes using its normal `weight` instead
    /// of `afterRepeatDowntimeWeight`.</param>
    /// <param name="repeatIntermissionWeight">Weight value to use instead of `weight` while in
    /// intermission from previous block of repeats. Defaults to 0.</param>
    /// <param name="associatedContextsPreference">Applies additional weight scaling (multiplier)
    /// for each given set of matched parent contexts.</param>
    /// <param name="unmatchedAssociatedContextsPreference">Applies additional weight scaling
    /// (multiplier) when no matched parent sets exist. If `associatedContextsPreference` has a
    /// value, this defaults to 0. Otherwise, it is skipped.</param>
    public partial class OptionDef(
                IDictionary<string, float> currentContexts,
                string name,
                string? description,
                float weight,
                int? repeatMinimumAmount,
                int? repeatMaximumAmount,
                float? isRepeatingWeightScale,
                float? isRepeatingContiguouslyWeightScale,
                int? repeatIntermissionMinimumDelay,
                float? repeatIntermissionWeight,
                IDictionary<string[], float> associatedContextsPreference,
                float? unmatchedAssociatedContextsPreference ) {
        public IDictionary<string, float> CurrentContexts { get; set; } = currentContexts;
        public string Name { get; set; } = name;
        public string? Description { get; set; } = description;
        public float Weight { get; set; } = weight;
        public int? RepeatMinimumAmount { get; set; } = repeatMinimumAmount;
        public int? RepeatMaximumAmount { get; set; } = repeatMaximumAmount;
        public float? IsRepeatingWeightScale { get; set; } = isRepeatingWeightScale;
        public float? IsRepeatingContiguouslyWeightScale { get; set; } = isRepeatingContiguouslyWeightScale;
        public int? RepeatIntermissionMinimumDelay { get; set; } = repeatIntermissionMinimumDelay;
        public float? RepeatIntermissionWeight { get; set; } = repeatIntermissionWeight;
        public IDictionary<string[], float> AssociatedContextsPreference { get; set; } = associatedContextsPreference;
        public float? UnmatchedAssociatedContextSetPreference { get; set; } = unmatchedAssociatedContextsPreference;



        public bool MatchesContexts( IList<string> contexts ) {
            return this.CurrentContexts.Keys.All( c => contexts.Contains(c) );
        }
    }
}

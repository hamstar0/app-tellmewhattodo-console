using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionOption {
    /// <summary>
    /// Defines decision making template data for all options of the given contexts that pertain to
    /// another, unspecified (parent) context.
    /// </summary>
    /// <param name="subOptionContextsPreferences">How preferred any given choice of sub-Option by
    /// the given context sets may be.</param>
    /// <param name="unmatchedSubContextsPreference">How preferred all choices of sub-Option not of
    /// the given context sets may be.</param>
    /// <param name="comboChance"></param>
    /// <param name="comboingWeight"></param>
    /// <param name="comboIntermissionDelay"></param>
    /// <param name="comboIntermissionWeight"></param>
    public class SubOption(
            IDictionary<string[], float> subOptionContextsPreferences,
            float? unmatchedSubContextsPreference,
            float? comboChance,
            float? comboingWeight,
            int? comboIntermissionDelay,
            float? comboIntermissionWeight ) {
        public IDictionary<string[], float> SubOptionContextsPreferences { get; set; } = subOptionContextsPreferences;
        public float? UnmatchedSubContextsPreference { get; set; } = unmatchedSubContextsPreference;
        public float? ComboChance { get; set; } = comboChance;
        public float? ComboingWeight { get; set; } = comboingWeight;
        public int? ComboIntermissionDelay { get; set; } = comboIntermissionDelay;
        public float? ComboIntermissionWeight { get; set; } = comboIntermissionWeight;
    }
}

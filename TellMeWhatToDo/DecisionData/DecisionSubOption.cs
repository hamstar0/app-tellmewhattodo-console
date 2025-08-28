using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionOption {
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public class DecisionSubOption {
	public IDictionary<string[], float> SubOptionContextsPreferences { get; set; }
    float? UnmatchedSubContextsPreference { get; set; }
    public float? ComboChance { get; set; }
    public float? ComboingWeight { get; set; }
    public int? ComboMinRepeats { get; set; }
    public int? ComboMaxRepeats { get; set; }
    public int? ComboIntermissionDelay { get; set; }
    public float? ComboIntermissionWeight { get; set; }
}

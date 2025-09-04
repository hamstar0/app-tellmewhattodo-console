using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


/// <summary>
/// JSON-able.
/// </summary>
public partial class DecisionsData {
            //IDictionary<string, DecisionOption> options,
            //IDictionary<string, DecisionSubOptionSlotDef> subOptionTypes ) {
    public IDictionary<string, DecisionOption> Options { get; set; } = null!;
    public IDictionary<string, DecisionSubOptionSlotDef> SubOptionTypes { get; set; } = null!;
}

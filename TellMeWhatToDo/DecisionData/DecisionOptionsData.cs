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
/// <param name="options"></param>
/// <param name="subOptionTypes"></param>
public partial class DecisionOptionsData(
            IDictionary<string, DecisionOption> options,
            IDictionary<string, SubOptionTypes> subOptionTypes ) {
    public IDictionary<string, DecisionOption> Options { get; set; } = options;
    public IDictionary<string, SubOptionTypes> SubOptionTypes { get; set; } = subOptionTypes;
}

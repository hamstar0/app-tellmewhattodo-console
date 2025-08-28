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
public partial class DecisionOptionChoices( IList<DecisionOption> options ) {
    public IList<DecisionOption> Options { get; set; } = options;
}

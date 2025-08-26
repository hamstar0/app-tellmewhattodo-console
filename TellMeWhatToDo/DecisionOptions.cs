using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


/// <summary>
/// Provides all available decision options for the relevent decision making circumstance. JSON-able.
/// </summary>
/// <param name="options">Each available option.</param>
public partial class DecisionOptions( IList<DecisionOptions.OptionDef> options ) {
    public IList<OptionDef> Options { get; set; } = options;
}

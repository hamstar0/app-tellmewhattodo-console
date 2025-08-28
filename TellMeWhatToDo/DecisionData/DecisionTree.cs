using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionTree( DecisionOption head ) {
    public DecisionOption Head { get; } = head;
    public IList<(DecisionOption, DecisionTree)> Tree { get; } = new List<(DecisionOption, DecisionTree)>();
}

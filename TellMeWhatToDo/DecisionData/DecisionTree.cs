using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionTree( DecisionOption head ) {
    public static DecisionTree Generate( DecisionOption head, IList<DecisionOption> options ) {
        var tree = new DecisionTree( head );

        foreach( DecisionOption.SubOption sub in head.SubOptions ) {
            f
        }

        return tree;
    }



    public DecisionOption Head { get; } = head;
    public IList<(DecisionOption, DecisionTree)> Tree { get; } = new List<(DecisionOption, DecisionTree)>();



    public IList<string> Render() {
        var output = new List<string>( ["Idea: " + this.Head.Name] );

        if( this.Head.Description is not null ) {
            output.Add( this.Head.Description );
        }

        f

        return output;
    }
}

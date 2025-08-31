using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionTree( DecisionOption head ) {
    public static DecisionTree Generate(
                DecisionsMaker decider,
                DecisionOption head,
                int depth ) {
        DecisionTree headTree = new DecisionTree( head );

        foreach( DecisionOption.SubOption sub in head.SubOptionsSlots ?? [] ) {
            headTree.FillSlot( decider, sub, depth );
        }

        return headTree;
    }



    public DecisionOption Head { get; } = head;
    public IList<(DecisionOption, DecisionTree?)> Tree { get; } = new List<(DecisionOption, DecisionTree?)>();



    public void FillSlot(
                DecisionsMaker decider,
                DecisionOption.SubOption slotDef,
                int currentDepth ) {
        foreach( DecisionOption option in decider.Options ) {
            bool isBreadthRepeating, isBreadthContiguous, canBreadthRepeatAgain;

            float optionWeight = option.ComputeWeight(
                isBreadthRepeating,
                isBreadthContiguous,
                canBreadthRepeatAgain
            );
            float subWeight = slotDef.ComputeWeight( option );

            pool[ option ] = optionWeight * subWeight;
        }

        f

        var supOptionTree = DecisionTree.Generate( decider, option, currentDepth++ );

        this.Tree.Add( (option, supOptionTree) );
    }

    public List<string> Render( string indent = "" ) {
        List<string> output = this.Head.Render( indent );

        if( this.Tree is not null ) {
            foreach( (DecisionOption o, DecisionTree? t) in this.Tree ) {
                output.AddRange( o.Render( indent + "  " ) );
                if( t is not null ) {
                    output.AddRange( t.Render( indent + "  " ) );
                }
            }
        }

        return output;
    }
}

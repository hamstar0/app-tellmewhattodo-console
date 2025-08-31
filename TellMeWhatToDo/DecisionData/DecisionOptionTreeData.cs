using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionOptionTreeData( DecisionOption head ) {
    public static DecisionOptionTreeData Generate(
                DecisionsMaker decider,
                DecisionOption head,
                int depth ) {
        DecisionOptionTreeData headTree = new DecisionOptionTreeData( head );

        foreach( SubOptionSlot sub in head.SubOptionsSlots ?? [] ) {
            headTree.FillSlot( decider, sub, depth );
        }

        return headTree;
    }



    public DecisionOption Head { get; } = head;
    public IList<(DecisionOption, DecisionOptionTreeData?)> Tree { get; } = new List<(DecisionOption, DecisionOptionTreeData?)>();



    public void FillSlot(
                DecisionsMaker decider,
                SubOptionSlot slotDef,
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

        var supOptionTree = DecisionOptionTreeData.Generate( decider, option, currentDepth++ );

        this.Tree.Add( (option, supOptionTree) );
    }

    public List<string> Render( string indent = "" ) {
        List<string> output = this.Head.Render( indent );

        if( this.Tree is not null ) {
            foreach( (DecisionOption o, DecisionOptionTreeData? t) in this.Tree ) {
                output.AddRange( o.Render( indent + "  " ) );
                if( t is not null ) {
                    output.AddRange( t.Render( indent + "  " ) );
                }
            }
        }

        return output;
    }
}

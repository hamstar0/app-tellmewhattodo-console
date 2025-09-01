using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionOptionTreeData( DecisionOption head ) {
    public class Node(
                DecisionOption nodeOption,
                DecisionOptionTreeData? nodeTree ) {
        public DecisionOption Option = nodeOption;
        public DecisionOptionTreeData? NodeTree = nodeTree;
    }



    public static DecisionOptionTreeData Generate(
                Random random,
                DecisionsMaker decider,
                DecisionOption head,
                int depth ) {
        DecisionOptionTreeData headTree = new DecisionOptionTreeData( head );

        int slot = 0;

        foreach( string subName in head.SubOptionsSlotsByName ?? [] ) {
            headTree.FillSlot( random, decider, decider.Data.SubOptionTypes[subName], slot++, depth );
        }

        return headTree;
    }



    public DecisionOption Head { get; } = head;
    public IList<Node> Tree { get; } = new List<Node>();



    public void FillSlot(
                Random random,
                DecisionsMaker decider,
                DecisionSubOption slotDef,
                int currentSlot,
                int currentDepth ) {
        var pool = new List<(DecisionOption option, float pref)>();

        foreach( (string name, DecisionOption option) in decider.Data.Options ) {
            float subWeight = slotDef.ComputeWeight( option );
            if( subWeight <= 0f ) {
                continue;
            }

            float optionWeight = option.ComputeWeight( false, false, true );

            if( subWeight > 0f ) {
                pool.Add( (option, optionWeight * subWeight) );
            }
        }

        float r = random.NextSingle() * pool.Sum(o => o.pref);
        float t = 0f;
        DecisionOption? choice = null;

        foreach( (DecisionOption o, float p) in pool ) {
            choice = o;
            t += p;
            if( t > r ) {
                break;
            }
        }
        if( choice is null ) {
            throw new Exception( "No sub-Option selected" );
        }

        var choiceTree = DecisionOptionTreeData.Generate( random, decider, choice, currentDepth++ );

        this.Tree.Add( new Node(choice, choiceTree ) );
    }

    public List<string> Render( string indent = "" ) {
        List<string> output = this.Head.Render( indent );

        if( this.Tree is not null ) {
            foreach( Node node in this.Tree ) {
                output.AddRange( node.Option.Render( indent + "  " ) );
                if( node.NodeTree is not null ) {
                    output.AddRange( node.NodeTree.Render( indent + "  " ) );
                }
            }
        }

        return output;
    }
}

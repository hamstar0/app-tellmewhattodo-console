using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionOptionConnector( DecisionOption head ) {
    public class Child(
                DecisionOption nodeOption,
                DecisionOptionConnector? nodeTree ) {
        public DecisionOption Option = nodeOption;
        public DecisionOptionConnector? NodeTree = nodeTree;
    }



    public static DecisionOptionConnector Generate(
                Random random,
                DecisionsMaker decider,
                DecisionOption head,
                int depth ) {
        DecisionOptionConnector headTree = new DecisionOptionConnector( head );

        int slot = 0;

        foreach( string subName in head.SubOptionsSlotsByName ?? [] ) {
            headTree.FillSlot(
                random: random,
                decider: decider,
                slotDef: decider.Data.SubOptionTypes[subName],
                currentSlot: slot++,
                currentDepth: depth
            );
        }

        return headTree;
    }



    public DecisionOption Head { get; } = head;
    public IList<Child> Tree { get; } = new List<Child>();



    public void FillSlot(
                Random random,
                DecisionsMaker decider,
                DecisionSubOptionSlotDef slotDef,
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

        DecisionOption pick = this.PickSlotOption( random, pool );

        var choiceTree = DecisionOptionConnector.Generate( random, decider, pick, currentDepth++ );

        this.Tree.Add( new Child(pick, choiceTree) );
    }


    public DecisionOption PickSlotOption(
                Random random,
                IList<(DecisionOption option, float pref)> pool ) {
        float r = random.NextSingle() * pool.Sum( o => o.pref );
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
            throw new Exception( "No slot Option selected" );
        }

        return choice;
    }


    public List<string> Render( string indent = "" ) {
        List<string> output = this.Head.Render( indent );

        if( this.Tree is not null ) {
            foreach( Child node in this.Tree ) {
                output.AddRange( node.Option.Render( indent + "  " ) );
                if( node.NodeTree is not null ) {
                    output.AddRange( node.NodeTree.Render( indent + "  " ) );
                }
            }
        }

        return output;
    }
}

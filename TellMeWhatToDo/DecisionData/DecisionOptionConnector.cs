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
                DecisionsData decisionsData,
                DecisionOption head,
                int depth ) {
        DecisionOptionConnector headTree = new DecisionOptionConnector( head );

        int slot = 0;

        foreach( DecisionOption.SubOptionSlotPreference sub in head.SubOptionsSlots ?? [] ) {
            headTree.FillSlot(
                random: random,
                decisionsData: decisionsData,
                slotInfo: sub,
                currentSlot: slot++,
                currentDepth: depth
            );
        }

        return headTree;
    }



    public DecisionOption Head { get; } = head;
    public IList<Child> Tree { get; } = new List<Child>();



    public bool FillSlot(
                Random random,
                DecisionsData decisionsData,
                DecisionOption.SubOptionSlotPreference slotInfo,
                int currentSlot,
                int currentDepth ) {
        if( random.NextSingle() > slotInfo.NonEmptyPreference ) {
            return false;
        }

        var pool = new List<(DecisionOption option, float pref)>();

        foreach( (string name, DecisionOption option) in decisionsData.Options ) {
            DecisionSubOptionSlotDef slotDef = decisionsData.SubOptionTypes[ slotInfo.Name ];

            float childCandidateWeight = slotDef.ComputeWeightAsChildCandidate( option );
            if( childCandidateWeight <= 0f ) {
                continue;
            }

            if( childCandidateWeight > 0f ) {
                pool.Add( (option, childCandidateWeight) );
            }
        }

        DecisionOption pick = this.PickSlotOption( random, pool );

        var choiceTree = DecisionOptionConnector.Generate( random, decisionsData, pick, currentDepth++ );

        this.Tree.Add( new Child( pick, choiceTree ) );

        return true;
    }


    public DecisionOption PickSlotOption(
                Random random,
                IList<(DecisionOption option, float pref)> pool ) {
        float r = random.NextSingle() * pool.Sum(o => o.pref);
        float t = 0f;
        DecisionOption? choice = null;

        foreach( (DecisionOption o, float p) in pool ) {
            t += p;

            if( t > r ) {
                choice = o;
                break;
            }
        }

        if( choice is null ) {
            throw new Exception( "Could not pick Option for slot." );
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

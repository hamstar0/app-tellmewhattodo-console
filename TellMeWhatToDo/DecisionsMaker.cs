using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker( DecisionOption head ) {
    public readonly DecisionOption Head = head;


    private Random Random = new Random();

    private IList<(bool isLastRepeat, DecisionOption choice)> History
            = new List<(bool, DecisionOption)>();

    private IList<string> CurrentContexts = new List<string>();


    public DecisionOption? PendingDecision;



    public void AddInitialContext( string context ) {
        this.CurrentContexts.Add( context );
    }

    public IList<string> PickContexts() {
        return this.CurrentContexts
            .Where( c => this.Random.NextSingle() >= 0.5f )
            .ToList();
    }

    public IDictionary<DecisionOption, float> GetWeights( IList<string> contexts ) {
        int count = this.Head.Options.Count;
        var weights = new Dictionary<DecisionOption, float>( count );

        for( int i=0; i<count; i++ ) {
            DecisionOption o = this.Head.Options[i];
            if( !o.MatchesContexts(contexts) ) {
                continue;
            }

            bool isRepeating = this.IsRepeating( o, out bool isContiguous );
            bool canRepeatAgain = isRepeating && this.CanRepeatAgain( o );

            weights[o] = o.ComputeWeight(
                //this.Data,
                isRepeating,
                isContiguous,
                canRepeatAgain,
                contexts
            );
        }

        return weights;
    }
}

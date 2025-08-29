using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker( IList<DecisionOption> options ) {
    public IList<DecisionOption> Options { get; } = options;


    private Random Random = new Random();

    private IList<(bool isLastRepeat, DecisionTree choice)> History
            = new List<(bool, DecisionTree)>();

    private IList<string> CurrentContexts = new List<string>();


    public DecisionTree? PendingDecision;



    public void AddInitialContext( string context ) {
        this.CurrentContexts.Add( context );
    }

    public IDictionary<DecisionOption, float> GetWeights( IList<string> contexts ) {
        int count = this.Options.Count;
        var weights = new Dictionary<DecisionOption, float>( count );

        for( int i=0; i<count; i++ ) {
            DecisionOption o = this.Options[i];
            if( !o.HasAllContexts(contexts) ) {
                continue;
            }

            bool isRepeating = this.IsRepeating( o, out bool isContiguous );
            bool canRepeatAgain = isRepeating && this.CanRepeatAgain( o );

            weights[o] = o.ComputeWeight(
                null,
                contexts,
                isRepeating,
                isContiguous,
                canRepeatAgain
            );
        }

        return weights;
    }
}

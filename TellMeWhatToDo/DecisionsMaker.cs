using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker( DecisionsData optionsData ) {
    public DecisionsData Data { get; } = optionsData;

    public readonly Random Random = new Random();


    private IList<(bool isLastRepeat, DecisionOptionConnector choice)> History
            = new List<(bool, DecisionOptionConnector)>();

    private IList<string> CurrentContexts = new List<string>();


    public DecisionOptionConnector? PendingDecision;



    public void AddInitialContext( string context ) {
        this.CurrentContexts.Add( context );
    }

    public IDictionary<DecisionOption, float> GetWeights( IList<string> contexts ) {
        int count = this.Data.Options.Count;
        var weights = new Dictionary<DecisionOption, float>( count );

        foreach( DecisionOption o in this.Data.Options.Values ) {
            if( !o.HasAllContexts(contexts) ) {
                continue;
            }

            bool isRepeating = this.IsRepeating( o, out bool isContiguous );
            bool canRepeatAgain = isRepeating && this.CanRepeatAgain( o );

            weights[o] = o.ComputeWeight(
                isRepeating,
                isContiguous,
                canRepeatAgain
            );
        }

        return weights;
    }
}

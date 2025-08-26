using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker( DecisionOptions data ) {
    public readonly DecisionOptions Options = data;


    private Random Random = new Random();

    private IList<(bool isLastRepeat, DecisionOptions.OptionDef choice)> History
            = new List<(bool, DecisionOptions.OptionDef)>();

    private IList<string> CurrentContexts = new List<string>();


    public DecisionOptions.OptionDef? PendingDecision;



    public void AddInitialContext( string context ) {
        this.CurrentContexts.Add( context );
    }

    public IList<string> PickContexts() {
        return this.CurrentContexts
            .Where( c => this.Random.NextSingle() >= 0.5f )
            .ToList();
    }

    public IDictionary<DecisionOptions.OptionDef, float> GetWeights( IList<string> contexts ) {
        int count = this.Options.Options.Count;
        var weights = new Dictionary<DecisionOptions.OptionDef, float>( count );

        for( int i=0; i<count; i++ ) {
            DecisionOptions.OptionDef o = this.Options.Options[i];
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

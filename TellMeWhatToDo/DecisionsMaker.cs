using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker( DecisionOptions data ) {
    public readonly DecisionOptions Data = data;


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

    public IList<float> GetWeights( IList<string> contexts ) {
        int count = this.Data.Options.Count;
        IList<float> weights = new List<float>( count );

        for( int i=0; i<count; i++ ) {
            DecisionOptions.OptionDef o = this.Data.Options[i];

            bool isRepeating = this.IsRepeating( o, out bool isContiguous );
            bool canRepeatAgain = isRepeating && this.CanRepeatAgain( o );

            weights[i] = o.ComputeWeight(
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

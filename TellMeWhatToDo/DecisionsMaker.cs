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

    private IList<DecisionOptions.ContextDef> CurrentContexts = new List<DecisionOptions.ContextDef>();


    public DecisionOptions.OptionDef? PendingDecision;



    public void AddInitialContext( DecisionOptions.ContextDef context ) {
        this.CurrentContexts.Add( context );
    }

    public IList<DecisionOptions.ContextDef> PickContexts() {
        return this.CurrentContexts
            .Where( c => this.Random.NextSingle() < c.Density )
            .ToList();
    }

    public IList<float> GetWeights( IList<DecisionOptions.ContextDef> contexts ) {
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

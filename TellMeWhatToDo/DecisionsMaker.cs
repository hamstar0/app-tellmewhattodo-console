using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker( DecisionsData data ) {
    public readonly DecisionsData Data = data;


    private Random Random = new Random();

    private IList<(bool isLastRepeat, DecisionsData.Option choice)> History
            = new List<(bool, DecisionsData.Option)>();

    private IList<DecisionsData.Context> CurrentContexts = new List<DecisionsData.Context>();


    public DecisionsData.Option? PendingDecision;



    public void AddInitialContext( DecisionsData.Context context ) {
        this.CurrentContexts.Add( context );
    }

    public IList<DecisionsData.Context> PickContexts() {
        return this.CurrentContexts
            .Where( c => this.Random.NextSingle() < c.Density )
            .ToList();
    }

    public IList<float> GetWeights( IList<DecisionsData.Context> contexts ) {
        int count = this.Data.Options.Count;
        IList<float> weights = new List<float>( count );

        for( int i=0; i<count; i++ ) {
            DecisionsData.Option o = this.Data.Options[i];

            bool isRepeating = this.IsRepeating( o, out bool isContiguous );
            bool canRepeatAgain = isRepeating && this.CanRepeatAgain( o );

            weights[i] = o.ComputeWeight(
                this.Data,
                isRepeating,
                isContiguous,
                canRepeatAgain,
                contexts
            );
        }

        return weights;
    }
}

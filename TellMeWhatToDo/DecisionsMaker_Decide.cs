using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker {
    public DecisionTree ProposeDecision() {
        DecisionOption choice = null!;

        IDictionary<DecisionOption, float> weights = this.GetWeights( this.CurrentContexts );

        float r = this.Random.NextSingle() * weights.Sum(o => o.Value);
        float climb = 0f;

        int optionCount = this.Options.Count;

        foreach( (DecisionOption o, float w) in weights ) {
            climb += w;
            if( r < climb ) {
                choice = o;
            }
        }
        if( choice is null ) {
            choice = this.Options[optionCount - 1];
        }

        return DecisionTree.Generate( choice, this.Options );
    }


    public void MakeDecision( DecisionTree choice ) {
        bool isRepeating = this.IsRepeating( choice.Head, out _ );
        bool canRepeat = isRepeating
            ? this.CanRepeatAnew( choice.Head, out _ )
            : this.CanRepeatAgain( choice.Head );

        if( !this._RemainingRepeats.ContainsKey(choice.Head) ) {
            int range = choice.Head.RepeatMaximumAmount ?? Int32.MaxValue;
            range -= choice.Head.RepeatMinimumAmount ?? 0;

            this._RemainingRepeats[choice.Head] = this.Random.Next(range) - 1;
        } else {
            this._RemainingRepeats[choice.Head]--;
            if( !canRepeat ) {
                this._RemainingRepeats.Remove( choice.Head );
            }
        }

        this.History.Add( (
            isLastRepeat: isRepeating && !canRepeat,
            choice: choice
        ) );
    }
}

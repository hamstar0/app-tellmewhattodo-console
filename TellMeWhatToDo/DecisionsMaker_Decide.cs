using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker {
    public DecisionOptionTreeData? ProposeDecision() {
        IDictionary<DecisionOption, float> weights = this.GetWeights( this.CurrentContexts );

        float r = this.Random.NextSingle() * weights.Sum(o => o.Value);
        float climb = 0f;

        int optionCount = this.Data.Options.Count;

        DecisionOption? choice = null;

        foreach( (DecisionOption o, float w) in weights ) {
            climb += w;
            choice = o;

            if( r >= climb ) {
                break;
            }
        }

        return choice is not null
            ? DecisionOptionTreeData.Generate( this, choice, 0 )
            : null;
    }


    public void MakeDecision( DecisionOptionTreeData choice ) {
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker {
    public DecisionsData.Option ProposeDecision() {
        DecisionsData.Option choice = null!;

        IList<DecisionsData.Context> contexts = this.PickContexts();
        IList<float> weights = this.GetWeights( contexts );

        float r = this.Random.NextSingle() * weights.Sum(o => o);
        float climb = 0f;

        int optionCount = this.Data.Options.Count;

        for( int i = 0; i < optionCount; i++ ) {
            climb += weights[i];
            if( r < climb ) {
                choice = this.Data.Options[i];
            }
        }
        if( choice is null ) {
            choice = this.Data.Options[optionCount - 1];
        }

        return choice;
    }


    public void MakeDecision( DecisionsData.Option choice ) {
        bool isRepeating = this.IsRepeating( choice, out _ );
        bool canRepeat = isRepeating
            ? this.CanRepeatAnew( choice )
            : this.CanRepeatAgain( choice );

        if( !this._RemainingRepeats.ContainsKey(choice) ) {
            this._RemainingRepeats[choice] = this.Random.Next(choice.RepeatMaximumAmount - choice.RepeatMinimumAmount) - 1;
        } else {
            this._RemainingRepeats[choice]--;
            if( !canRepeat ) {
                this._RemainingRepeats.Remove( choice );
            }
        }

        this.History.Add( (
            isLastRepeat: isRepeating && !canRepeat,
            choice: choice
        ) );
    }
}

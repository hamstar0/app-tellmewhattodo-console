using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker {
    public DecisionOptions.OptionDef ProposeDecision() {
        DecisionOptions.OptionDef choice = null!;

        IList<string> contexts = this.PickContexts();
        IDictionary<DecisionOptions.OptionDef, float> weights = this.GetWeights( contexts );

        float r = this.Random.NextSingle() * weights.Sum(o => o.Value);
        float climb = 0f;

        int optionCount = this.Options.Options.Count;

        foreach( (DecisionOptions.OptionDef o, float w) in weights ) {
            climb += w;
            if( r < climb ) {
                choice = o;
            }
        }
        if( choice is null ) {
            choice = this.Options.Options[optionCount - 1];
        }

        return choice;
    }


    public void MakeDecision( DecisionOptions.OptionDef choice ) {
        bool isRepeating = this.IsRepeating( choice, out _ );
        bool canRepeat = isRepeating
            ? this.CanRepeatAnew( choice, out _ )
            : this.CanRepeatAgain( choice );

        if( !this._RemainingRepeats.ContainsKey(choice) ) {
            int range = choice.RepeatMaximumAmount ?? Int32.MaxValue;
            range -= choice.RepeatMinimumAmount ?? 0;

            this._RemainingRepeats[choice] = this.Random.Next(range) - 1;
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

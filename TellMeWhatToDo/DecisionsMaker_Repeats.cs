using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker {
    private IDictionary<DecisionOption, int> _RemainingRepeats = new Dictionary<DecisionOption, int>();



    public bool IsRepeating( DecisionOption option, out bool isContiguous ) {
        bool isNonContiguous = false;

        for( int i = this.History.Count - 1; i >= 0; i-- ) {
            if( this.History[i].choice.Head == option ) {
                isContiguous = !isNonContiguous;

                return !this.History[i].isLastRepeat;
            }

            isNonContiguous = true;
        }

        isContiguous = false;
        return false;
    }

    public int CountRepeatsSince( DecisionOption option, int index, out int contiguousRepeats ) {
        contiguousRepeats = 0;
        int repeats = 0;
        bool contiguityBroken = false;

        for( int i = index; i >= 0; i-- ) {
            if( this.History[i].choice.Head == option ) {
                if( this.History[i].isLastRepeat ) {
                    break;
                }

                repeats++;
                if( !contiguityBroken ) {
                    contiguousRepeats++;
                }
            } else {
                contiguityBroken = true;
            }
        }

        return repeats;
    }

    public bool CanRepeatAnew( DecisionOption option, out float weight ) {
        if( option.RepeatIntermissionMinimumDelay is null ) {
            weight = option.Weight;
            return true;
        }

        int traveled = 0;

        for( int i = this.Data.Options.Count - 1; i >= 0; i-- ) {
            if( traveled >= option.RepeatIntermissionMinimumDelay ) {
                weight = option.Weight;
                return true;
            }

            traveled++;
        }

        weight = option.RepeatIntermissionWeight ?? 0f;
        return false;
    }

    public bool CanRepeatAgain( DecisionOption option ) {
        int repeats = this.CountRepeatsSince( option, this.History.Count - 1, out _ );

        if( this._RemainingRepeats[option] <= 0 ) {
            return false;
        }

        return true;
    }
}

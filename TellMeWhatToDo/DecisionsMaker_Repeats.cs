using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public partial class DecisionsMaker {
    private IDictionary<DecisionsData.Option, int> _RemainingRepeats = new Dictionary<DecisionsData.Option, int>();



    public bool IsRepeating( DecisionsData.Option option, out bool isContiguous ) {
        bool isNonContiguous = false;

        for( int i = this.History.Count - 1; i >= 0; i-- ) {
            if( this.History[i].choice == option ) {
                isContiguous = !isNonContiguous;

                return !this.History[i].isLastRepeat;
            }

            isNonContiguous = true;
        }

        isContiguous = false;
        return false;
    }

    public int CountRepeatsSince( DecisionsData.Option option, int index, out int contiguousRepeats ) {
        contiguousRepeats = 0;
        int repeats = 0;
        bool contiguityBroken = false;

        for( int i = index; i >= 0; i-- ) {
            if( this.History[i].choice == option ) {
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

    public bool CanRepeatAnew( DecisionsData.Option option ) {
        int traveled = 0;

        for( int i = this.Data.Options.Count - 1; i >= 0; i-- ) {
            if( option.AfterRepeatMaximumSteps > -1 ) {
                if( traveled >= option.AfterRepeatMaximumSteps ) {
                    return false;
                }
            }
            if( traveled >= option.AfterRepeatMinimumSteps ) {
                return true;
            }

            traveled++;
        }

        return true;
    }

    public bool CanRepeatAgain( DecisionsData.Option option ) {
        int repeats = this.CountRepeatsSince( option, this.History.Count - 1, out _ );

        if( this._RemainingRepeats[option] <= 0 ) {
            return false;
        }

        return true;
    }
}

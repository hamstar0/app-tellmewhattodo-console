using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


public static class LinqExtensions {
    public static IEnumerable<TResult> SelectWhere<TResult, TSource>(
                this IEnumerable<TSource> input,
                Func<TSource, (object ret, bool pass)> selector ) {
        foreach( TSource i in input ) {
            (object ret, bool pass) = selector( i );
            if( pass ) {
                if( ret is TResult ) {
                    yield return (TResult)ret;
                } else {
                    throw new Exception( $"Output object is not a {nameof(TResult)}." );
                }
            }
        }
    }
}

public partial class DecisionsData {
    public partial class Option {
        public float ComputeWeight(
                    DecisionsData data,
                    bool isRepeating,
                    bool isContiguous,
                    bool canRepeatAgain,
                    IList<DecisionsData.Context> nowContexts ) {
            float weight = this.Weight;

            if( isRepeating ) {
                if( canRepeatAgain ) {
                    weight *= this.IsRepeatingWeightScale;

                    if( isContiguous ) {
                        weight *= this.IsRepeatingContiguouslyWeightScale;
                    }
                }
            }

            var nowContextSet = new HashSet<string>( nowContexts.Select( c => c.Name ) );

            foreach( (string ctxPrefName, float mul) in this.ContextPreference ) {
                if( nowContextSet.Contains(ctxPrefName) ) {
                    weight *= mul;
                }
            }

            bool hasSet = false;

            foreach( (string[] ctxSetNames, float mul) in this.ContextSetPreference ) {
                if( ctxSetNames.All( c => nowContextSet.Contains(c) ) ) {
                    weight *= mul;
                    hasSet = true;
                }
            }

            if( !hasSet ) {
                weight *= this.UnmatchedContextSetPreference;
            }

            // Let the contexts also decide if they go together or not
            foreach( Context nowContext1 in nowContexts ) {
                foreach( Context nowContext2 in nowContexts ) {
                    if( nowContext1 == nowContext2 ) {
                        continue;
                    }
                    if( nowContext1.CoContextWeightScales.ContainsKey(nowContext2.Name) ) {
                        weight *= nowContext1.CoContextWeightScales[nowContext2.Name];
                    }
                }
            }

            return weight;
        }
    }
}

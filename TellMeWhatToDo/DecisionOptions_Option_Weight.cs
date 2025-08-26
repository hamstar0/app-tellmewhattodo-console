using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TellMeWhatToDo;


//public static class LinqExtensions {
//    public static IEnumerable<TResult> SelectWhere<TResult, TSource>(
//                this IEnumerable<TSource> input,
//                Func<TSource, (object ret, bool pass)> selector ) {
//        foreach( TSource i in input ) {
//            (object ret, bool pass) = selector( i );
//            if( pass ) {
//                if( ret is TResult ) {
//                    yield return (TResult)ret;
//                } else {
//                    throw new Exception( $"Output object is not a {nameof(TResult)}." );
//                }
//            }
//        }
//    }
//}



public partial class DecisionOptions {
    public partial class OptionDef {
        public float ComputeWeight(
                    //DecisionsOption data,
                    bool isRepeating,
                    bool isContiguous,
                    bool canRepeatAgain,
                    IList<string> nowContexts ) {
            float weight = this.Weight;

            if( isRepeating ) {
                if( canRepeatAgain && this.IsRepeatingWeightScale is not null ) {
                    weight *= this.IsRepeatingWeightScale.Value;

                    if( isContiguous && this.IsRepeatingContiguouslyWeightScale is not null ) {
                        weight *= this.IsRepeatingContiguouslyWeightScale.Value;
                    }
                }
            }

            bool hasSet = false;

            foreach( (string[] ctxSetNames, float mul) in this.AssociatedContextsPreference ) {
                if( ctxSetNames.All( c => nowContexts.Contains(c) ) ) {
                    weight *= mul;
                    hasSet = true;
                }
            }

            if( !hasSet && this.AssociatedContextsPreference.Count > 0 ) {
                weight *= this.UnmatchedAssociatedContextSetPreference ?? 0f;
            }

            //// Let the contexts also decide if they go together or not
            //foreach( ContextDef nowContext1 in nowContexts ) {
            //    foreach( ContextDef nowContext2 in nowContexts ) {
            //        if( nowContext1 == nowContext2 ) {
            //            continue;
            //        }
            //        if( nowContext1.CoContextWeightScales.ContainsKey(nowContext2.Name) ) {
            //            weight *= nowContext1.CoContextWeightScales[nowContext2.Name];
            //        }
            //    }
            //}

            return weight;
        }
    }
}

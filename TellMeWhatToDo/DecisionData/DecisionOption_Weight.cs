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



public partial class DecisionOption {
    public float ComputeWeight(
                DecisionOption? parent,
                IList<string> nowContexts,
                bool isRepeating,
                bool isContiguous,
                bool canRepeatAgain ) {
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

        foreach( SubOption subOption in parent?.SubOptions ?? [] ) {
            foreach( (string[] ctxSetNames, float mul) in subOption.SubOptionContextsPreferences ) {
                if( !this.HasAllContexts( ctxSetNames) ) {
                    continue;
                }

                if( ctxSetNames.All( c => nowContexts.Contains(c) ) ) {
                    weight *= mul;
                    hasSet = true;
                }
            }

            if( !hasSet && subOption.SubOptionContextsPreferences.Count > 0 ) {
                weight *= subOption.UnmatchedSubContextsPreference ?? 0f;
            }
        }

        return weight;
    }
}

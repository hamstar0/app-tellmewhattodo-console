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

        return weight;
    }
}

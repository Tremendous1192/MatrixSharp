using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class Matrix
    {
        /// <summary>
        /// 絶対値の降順に並び替える。絶対値が等しい場合、正の数を前に並べる。
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        internal static double[] OrderByAbs(in double[] array)
        {
            double threshold = Math.Pow(10, -13); // 閾値(doubleの有効数字は15～17桁のため)
            double[] results = (double[])array.Clone(); // 戻り値
            double temp = 0;
            fixed (double* presults = results, endpresults = &results[results.Length - 1])
            {
                for (double* p1 = presults; p1 != endpresults; ++p1)
                {
                    for (double* p2 = p1 + 1; p2 != endpresults + 1; ++p2)
                    {
                        if (Math.Abs(Math.Abs(*p1) - Math.Abs(*p2)) > threshold) // 2つの絶対値に差がある場合
                        {
                            if (Math.Abs(*p1) < Math.Abs(*p2)) // 後ろ側のデータの絶対値が大きい場合、入れ替える
                            {
                                temp = *p1;
                                *p1 = *p2;
                                *p2 = temp;
                            }
                        }
                        else // 2つの値に差がない場合
                        {
                            if (*p1 < *p2) // 絶対値が等しい場合、正の数を前に並べる。
                            {
                                temp = *p1;
                                *p1 = *p2;
                                *p2 = temp;
                            }
                        }
                    }
                }
            }

            return results;
        }
    }
}

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
        /// 小さいサイズの行列の固有値を計算する
        /// </summary>
        /// <param name="array">正方行列</param>
        /// <returns></returns>
        internal static double[] EigenValuesSmallSize(double[,] array)
        {
            // 1x1の場合、単なるスカラー倍なので、成分をそのまま返す
            if (array.Length == 1) { return new double[1] { array[0, 0] }; }

            // 2x2の場合、行列式の公式で済ませる
            double a = array[0, 0], b = array[0, 1];
            double c = array[1, 0], d = array[1, 1];
            double sq = (a + d) * (a + d) / 4 - a * d + b * c;

            List<double> result2Dim = new List<double>(2);
            if (sq < 0)
            {
                result2Dim.Add(0);
            }
            else if (double.IsNaN(1.0 / sq))
            {
                result2Dim.Add((a + d) / 2);
            }
            else
            {
                sq = Math.Sqrt(sq);
                double plus = (a + d) / 2.0 + sq;
                double minus = (a + d) / 2.0 - sq;
                if (Math.Abs(plus) > Math.Abs(minus))
                {
                    result2Dim.Add(plus);
                    result2Dim.Add(minus);
                }
                else
                {
                    result2Dim.Add(minus);
                    result2Dim.Add(plus);
                }
            }
            return result2Dim.ToArray();
        }
    }
}

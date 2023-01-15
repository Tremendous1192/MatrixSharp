using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tremendous1192.SelfEmployed.MatrixSharp;

namespace MatrixSharp.Source.DesignMatrix
{
    /// <summary>
    /// 計画行列のクラス
    /// </summary>
    public static unsafe partial class DesignMatrix
    {
        /// <summary>
        /// 計画行列の平均ベクトルを計算する
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <returns></returns>
        public static RowVector Mean(Matrix designMatrix)
        {
            double[] mean = new double[designMatrix.Column];
            fixed (double* pdesignMatrix = designMatrix._array, pmean = mean)
            {
                int count = 0;
                for (double* pd = pdesignMatrix, endpd = pdesignMatrix + designMatrix._array.Length, pm = pmean;
                    pd != endpd; ++pd, ++count, pm = pmean + count % designMatrix.Column)
                {
                    *pm += *pd;
                }
            }

            return new RowVector(mean.Select(x => x / designMatrix.Column).ToArray(), false);
        }

        /// <summary>
        /// 測定値(列ベクトル)の平均値を計算する
        /// </summary>
        /// <param name="measuredVariables">測定値</param>
        /// <returns></returns>
        public static double Mean(ColumnVector measuredVariables)
        {
            return measuredVariables._array.Sum() / measuredVariables.Dimension;
        }

    }
}

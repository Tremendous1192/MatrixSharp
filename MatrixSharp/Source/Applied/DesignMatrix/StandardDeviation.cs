using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tremendous1192.SelfEmployed.MatrixSharp;

namespace MatrixSharp.Source.DesignMatrix
{
    public static unsafe partial class DesignMatrix
    {
        /// <summary>
        /// 各成分の標準偏差を計算する。
        /// </summary>
        /// <param name="designMatrix"></param>
        /// <returns></returns>
        public static RowVector StandardDeviation(Matrix designMatrix)
        {
            double[] mean = new double[designMatrix.Column];
            double[] meanSquare = new double[designMatrix.Column];

            fixed (double* pdesignMatrix = designMatrix._array, pmean = mean, pmeanSquare = meanSquare)
            {
                int count = 0;
                for (double* pd = pdesignMatrix, endpd = pdesignMatrix + designMatrix._array.Length, pm = pmean, pms = pmeanSquare;
                    pd != endpd; ++pd, ++count, pm = pmean + count % designMatrix.Column, pms = pmeanSquare + count % designMatrix.Column)
                {
                    *pm += *pd;
                    *pms += *pd * *pd;
                }
            }

            int row = designMatrix.Row;
            return new RowVector(meanSquare.Zip(mean, (xx, x) => xx / row - x * x / (row * row)).ToArray(), false);
        }

        /// <summary>
        /// 測定値の標準偏差を計算する
        /// </summary>
        /// <param name="measuredVariables"></param>
        /// <returns></returns>
        public static double StandardDeviation(ColumnVector measuredVariables)
        {
            double sum = 0, sumSquare = 0;
            double y = 0;
            int row = measuredVariables.Dimension;
            for (int i = 0; i < measuredVariables.Dimension; i++)
            {
                y = measuredVariables[i];
                sum += y;
                sumSquare += y * y;
            }
            return Math.Sqrt(sumSquare / row - (sum * sum) / (row * row));
        }
    }
}

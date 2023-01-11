using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tremendous1192.SelfEmployed.MatrixSharp;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public static unsafe partial class Preprocessing
    {
        /// <summary>
        /// 標準化した説明変数の行ベクトルを元に戻す.
        /// </summary>
        /// <param name="rowVectorNormarized">標準化した行ベクトル</param>
        /// <param name="average">平均</param>
        /// <param name="standardDeviation">標準偏差</param>
        /// <returns></returns>
        public static RowVector ZScoreNormarizationInverse(RowVector rowVectorNormarized, RowVector average, RowVector standardDeviation)
        {
            if (rowVectorNormarized.Dimension != average.Dimension || rowVectorNormarized.Dimension != standardDeviation.Dimension)
            {
                throw new FormatException("次元が揃っていません");
            }

            double[] result = new double[rowVectorNormarized.Dimension];
            for (int j = 0; j < rowVectorNormarized.Dimension; j++)
            {
                result[j] = rowVectorNormarized[j] * standardDeviation[j] + average[j];
            }

            return new RowVector(result);
        }

        /// <summary>
        /// 標準化した計画行列を元に戻す
        /// </summary>
        /// <param name="designMatrixNormarized">計画行列</param>
        /// <param name="average">平均</param>
        /// <param name="standardDeviation">標準偏差</param>
        /// <returns></returns>
        public static Matrix ZScoreNormarizationInverse(Matrix designMatrixNormarized, RowVector average, RowVector standardDeviation)
        {
            if (designMatrixNormarized.Column != average.Dimension || designMatrixNormarized.Column != standardDeviation.Dimension)
            {
                throw new FormatException("次元が揃っていません");
            }

            double[,] normarized = new double[designMatrixNormarized.Row, designMatrixNormarized.Column];

            unsafe
            {
                fixed (double* pdesignMatrix = designMatrixNormarized._array, pcentroid = average._array, pstandardDeviation = standardDeviation._array, pnormarized = normarized)
                {
                    int count = 0;
                    for (double* pdes = pdesignMatrix, endpdes = pdesignMatrix + designMatrixNormarized._array.Length, pshi = pnormarized;
                        pdes != pdesignMatrix; ++pdes, ++pshi, ++count)
                    {
                        *pshi = *pdes * *(pstandardDeviation + count % designMatrixNormarized.Column) + *(pcentroid + count % designMatrixNormarized.Column);
                    }
                }
            }

            return new Matrix(normarized, false);
        }

        /// <summary>
        /// 標準化した説明変数を元に戻す
        /// </summary>
        /// <param name="measuredVariablesNormarized"></param>
        /// <param name="average"></param>
        /// <param name="standardDeviation"></param>
        /// <returns></returns>
        public static ColumnVector ZScoreNormarizationInverse(ColumnVector measuredVariablesNormarized, double average, double standardDeviation)
        {
            double[] result = new double[measuredVariablesNormarized.Dimension];
            for (int i = 0; i < measuredVariablesNormarized.Dimension; i++)
            {
                result[i] = measuredVariablesNormarized[i] * standardDeviation + average;
            }

            return new ColumnVector(result);
        }

        /// <summary>
        /// 標準化した説明変数を元に戻す
        /// </summary>
        /// <param name="measuredVariableNormarized"></param>
        /// <param name="average"></param>
        /// <param name="standardDeviation"></param>
        /// <returns></returns>
        public static double ZScoreNormarizationInverse(double measuredVariableNormarized, double average, double standardDeviation)
        {
            return measuredVariableNormarized * standardDeviation + average;
        }
    }
}

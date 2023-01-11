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
        /// 説明変数の行ベクトルを標準化する
        /// </summary>
        /// <param name="rowVector">説明変数</param>
        /// <param name="average">平均</param>
        /// <param name="standardDeviation">標準偏差</param>
        /// <returns></returns>
        public static RowVector ZScoreNormarization(RowVector rowVector, RowVector average, RowVector standardDeviation)
        {
            if (rowVector.Dimension != average.Dimension || rowVector.Dimension != standardDeviation.Dimension)
            {
                throw new FormatException("次元が揃っていません");
            }

            double[] result = new double[rowVector.Dimension];
            for (int j = 0; j < rowVector.Dimension; j++)
            {
                result[j] = (rowVector[j] - average[j]) / standardDeviation[j];
            }

            return new RowVector(result);
        }

        /// <summary>
        /// 計画行列を標準化する.
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <param name="average">平均</param>
        /// <param name="standardDeviation">標準偏差</param>
        /// <returns></returns>
        public static Matrix ZScoreNormarization(Matrix designMatrix, RowVector average, RowVector standardDeviation)
        {
            if (designMatrix.Column != average.Dimension || designMatrix.Column != standardDeviation.Dimension)
            {
                throw new FormatException("次元が揃っていません");
            }

            double[,] normarized = new double[designMatrix.Row, designMatrix.Column];

            unsafe
            {
                fixed (double* pdesignMatrix = designMatrix._array, pcentroid = average._array, pstandardDeviation = standardDeviation._array, pnormarized = normarized)
                {
                    int count = 0;
                    for (double* pdes = pdesignMatrix, endpdes = pdesignMatrix + designMatrix._array.Length, pshi = pnormarized;
                        pdes != pdesignMatrix; ++pdes, ++pshi, ++count)
                    {
                        *pshi = (*pdes - *(pcentroid + count % designMatrix.Column)) / *(pstandardDeviation + count % designMatrix.Column);
                    }
                }
            }

            return new Matrix(normarized, false);
        }

        /// <summary>
        /// 目的変数の列ベクトルを標準化する.
        /// </summary>
        /// <param name="measuredVariables">目的変数</param>
        /// <param name="average">平均</param>
        /// <param name="standardDeviation">標準偏差</param>
        /// <returns></returns>
        public static ColumnVector ZScoreNormarization(ColumnVector measuredVariables, double average, double standardDeviation)
        {
            double[] result = new double[measuredVariables.Dimension];
            for (int i = 0; i < measuredVariables.Dimension; i++)
            {
                result[i] = (measuredVariables[i] - average) / standardDeviation;
            }

            return new ColumnVector(result);
        }

        /// <summary>
        /// 目的変数を標準化する.
        /// </summary>
        /// <param name="measuredVariable">測定値</param>
        /// <param name="average">平均</param>
        /// <param name="standardDeviation">標準偏差</param>
        /// <returns></returns>
        public static double ZScoreNormarization(double measuredVariable, double average, double standardDeviation)
        {
            return (measuredVariable - average) / standardDeviation;
        }
    }
}

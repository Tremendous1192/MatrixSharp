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
        /// Centroidを原点とする相対位置行ベクトルを計算する
        /// </summary>
        /// <param name="rowVector">説明変数</param>
        /// <param name="centroid">平行移動後の原点</param>
        /// <returns></returns>
        public static RowVector RelativePosition(RowVector rowVector, RowVector centroid)
        {
            if (rowVector.Dimension != centroid.Dimension)
            {
                throw new FormatException("次元が揃っていません");
            }
            return rowVector - centroid;
        }

        /// <summary>
        /// Centroidを原点とする相対位置計画行列を計算する
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <param name="centroid">平行移動後の原点</param>
        /// <returns></returns>
        public static Matrix RelativePosition(Matrix designMatrix, RowVector centroid)
        {
            if (designMatrix.Column != centroid.Dimension)
            {
                throw new FormatException("次元が揃っていません");
            }

            double[,] shifted = new double[designMatrix.Row, designMatrix.Column];

            unsafe
            {
                fixed (double* pdesignMatrix = designMatrix._array, pcentroid = centroid._array, pshifted = shifted)
                {
                    int count = 0;
                    for (double* pdes = pdesignMatrix, endpdes = pdesignMatrix + designMatrix._array.Length, pshi = pshifted;
                        pdes != pdesignMatrix; ++pdes, ++pshi, ++count)
                    {
                        *pshi = *pdes - *(pcentroid + count % designMatrix.Column);
                    }
                }
            }

            return new Matrix(shifted, false);
        }

        /// <summary>
        /// Centroidを原点とする相対位置列ベクトルを計算する
        /// </summary>
        /// <param name="measuredVariables">目的変数</param>
        /// <param name="centroid">平行移動後の原点</param>
        /// <returns></returns>
        public static ColumnVector RelativePosition(ColumnVector measuredVariables, double centroid)
        {
            return new ColumnVector(measuredVariables._array.Select(y => y - centroid).ToArray());
        }

        /// <summary>
        /// Centroidを原点とする相対位置を計算する
        /// </summary>
        /// <param name="measuredVariable">測定値</param>
        /// <param name="centroid">平行移動後の原点</param>
        /// <returns></returns>
        public static double RelativePosition(double measuredVariable, double centroid)
        {
            return measuredVariable - centroid;
        }
    }
}

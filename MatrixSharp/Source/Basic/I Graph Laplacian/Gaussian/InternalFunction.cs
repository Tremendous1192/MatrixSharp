using MatrixSharp.Source.DesignMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class GaussianGraphLaplacian : IGraphLaplacian
    {
        /// <summary>
        /// 隣接行列を返す。
        /// </summary>
        /// <param name="matrix">計画行列の配列</param>
        /// <returns>double[,]</returns>
        internal double[,] InternalFunction(double[,] matrix)
        {
            double[,] adjacency = new double[matrix.GetLength(0), matrix.GetLength(0)];
            int i = 0, j = 0;
            double distance = 0;

            // ハイパーパラメータの設定済み
            if (setHyperParameter) 
            {
                fixed (double* pdesignMatrix = matrix, padjacency = adjacency)
                {
                    double* pad = padjacency;
                    // データ点 1
                    for (double* pdes1 = pdesignMatrix, endpdes1 = pdesignMatrix + matrix.Length; pdes1 != endpdes1; pdes1 += matrix.GetLength(1))
                    {
                        i = (int)(pdes1 - pdesignMatrix) / matrix.GetLength(1);
                        // データ点 2
                        for (double* pdes2 = pdesignMatrix; pdes2 != pdes1; pdes2 += matrix.GetLength(1))
                        {
                            j = (int)(pdes2 - pdesignMatrix) / matrix.GetLength(1);
                            distance = 0;
                            // 距離計算
                            for (double* p1 = pdes1, endp1 = pdes1 + matrix.GetLength(1), p2 = pdes2; p1 != endp1; ++p1, ++p2)
                            {
                                distance += (*p1 - *p2) * (*p1 - *p2);
                            }
                            adjacency[i, j] = Math.Exp(-Math.Sqrt(distance) / hyperParameterArray[0]);
                            adjacency[j, i] = adjacency[i, j];
                        }
                    }
                }
            }
            else
            {
                fixed (double* pdesignMatrix = matrix, padjacency = adjacency)
                {
                    double* pad = padjacency;
                    // データ点 1
                    for (double* pdes1 = pdesignMatrix, endpdes1 = pdesignMatrix + matrix.Length; pdes1 != endpdes1; pdes1 += matrix.GetLength(1))
                    {
                        i = (int)(pdes1 - pdesignMatrix) / matrix.GetLength(1);
                        // データ点 2
                        for (double* pdes2 = pdesignMatrix; pdes2 != pdes1; pdes2 += matrix.GetLength(1))
                        {
                            j = (int)(pdes2 - pdesignMatrix) / matrix.GetLength(1);
                            distance = 0;
                            // 距離計算
                            for (double* p1 = pdes1, endp1 = pdes1 + matrix.GetLength(1), p2 = pdes2; p1 != endp1; ++p1, ++p2)
                            {
                                distance += (*p1 - *p2) * (*p1 - *p2);
                            }
                            adjacency[i, j] = Math.Exp(-Math.Sqrt(distance));
                            adjacency[j, i] = adjacency[i, j];
                        }
                    }
                }
            } 

            return adjacency;
        }

    }
}

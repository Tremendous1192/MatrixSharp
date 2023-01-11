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
        /// ラプラシアン行列を返す
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <returns>Matrix</returns>
        public Matrix LaplacianMatrix(Matrix designMatrix)
        {
            double[,] adjacency = this.InternalFunction(designMatrix._array);
            int i = 0, j = 0;
            double distance = 0;
            double[,] degree = new double[designMatrix.Row, designMatrix.Row];
            double[,] laplacian=new double[designMatrix.Row, designMatrix.Row];
            fixed (double* pdesignMatrix = designMatrix._array, padjacency = adjacency, pdegree = degree, plaplacian = laplacian)
            {
                // 次数行列
                for (double* pdes1 = pdesignMatrix, endpdes1 = pdesignMatrix + designMatrix._array.Length; pdes1 != endpdes1; pdes1 += designMatrix._array.GetLength(1))
                {
                    i = (int)(pdes1 - pdesignMatrix) / designMatrix._array.GetLength(1);
                    distance = 0;
                    for (double* p1 = pdes1, endp1 = pdes1 + designMatrix._array.GetLength(1); p1 != endp1; ++p1)
                    {
                        distance += *p1;
                    }

                    degree[i, i] = distance;
                }

                // ラプラシアン
                for (double* plap = plaplacian, endplap = plaplacian + laplacian.Length, pad = padjacency, pdeg = pdegree;
                    plap != endplap; ++plap, ++pad, ++pdeg)
                { *plap = *pdeg - *pad; }
            }

            return new Matrix(laplacian, false);
        }
    }
}

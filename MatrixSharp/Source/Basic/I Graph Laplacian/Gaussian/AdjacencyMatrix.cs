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
        /// 隣接行列を返す
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <returns></returns>
        public Matrix AdjacencyMatrix(Matrix designMatrix)
        {
            return new Matrix(this.InternalFunction(designMatrix._array), false);
        }
    }
}

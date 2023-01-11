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
        /// 分散・共分散行列の逆行列を返す。
        /// </summary>
        /// <param name="designMatrix"></param>
        /// <returns></returns>
        public static Matrix InverseVarianceCovarianceMatrix(Matrix designMatrix)
        {
            return DesignMatrix.VarianceCovarianceMatrix(designMatrix).Inverse();
        }
    }
}

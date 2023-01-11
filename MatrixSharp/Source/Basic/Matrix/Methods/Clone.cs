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
        /// 行列を複製する
        /// </summary>
        /// <returns>Matrix</returns>
        public Matrix Clone()
        { return new Matrix(_array, false); }
    }
}

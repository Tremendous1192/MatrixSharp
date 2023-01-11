using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class RowVector
    {
        /// <summary>
        /// ベクトルを転置する
        /// </summary>
        /// <returns>ColumnVector</returns>
        public ColumnVector Transpose()
        { return new ColumnVector(_array); }
    }
}

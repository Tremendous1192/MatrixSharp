using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class ColumnVector
    {
        /// <summary>
        /// ベクトルを転置する
        /// </summary>
        /// <returns>RowVector</returns>
        public RowVector Transpose()
        { return new RowVector(_array); }
    }
}

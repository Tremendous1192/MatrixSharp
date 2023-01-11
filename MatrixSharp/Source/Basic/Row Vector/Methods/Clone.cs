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
        /// ベクトルを複製する
        /// </summary>
        /// <returns>ColumnVector</returns>
        public RowVector Clone()
        { return new RowVector(_array); }
    }
}

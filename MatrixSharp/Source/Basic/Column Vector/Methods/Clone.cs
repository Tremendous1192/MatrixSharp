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
        /// ベクトルを複製する
        /// </summary>
        /// <returns>ColumnVector</returns>
        public ColumnVector Clone()
        { return new ColumnVector(_array); }
    }
}

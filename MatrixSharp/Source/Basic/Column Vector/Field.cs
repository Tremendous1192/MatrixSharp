using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// 列ベクトル
    /// </summary>
    public unsafe partial class ColumnVector
    {
        internal double[] _array;
        internal int _dim;
    }
}

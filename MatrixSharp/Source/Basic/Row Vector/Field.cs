using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// 行ベクトル
    /// </summary>
    public unsafe partial class RowVector
    {
        internal double[] _array;
        internal int _dim;
    }
}

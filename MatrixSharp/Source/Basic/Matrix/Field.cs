using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// 行列のクラス
    /// </summary>
    public unsafe partial class Matrix
    {
        internal double[,] _array; // 2次元配列
        internal int _row; // 行数
        internal int _column; // 列数
    }
}

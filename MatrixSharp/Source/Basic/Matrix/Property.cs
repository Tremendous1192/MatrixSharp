using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class Matrix
    {
        /// <summary>
        /// 行列の要素
        /// </summary>
        /// <param name="i">行番号</param>
        /// <param name="j">列番号</param>
        /// <returns></returns>
        public double this[int i, int j]
        {
            get { return _array[i, j]; }
            set { _array[i, j] = value; }
        }

        /// <summary>
        /// 行数
        /// </summary>
        public int Row
        {
            get { return _row; }
        }

        /// <summary>
        /// 列数
        /// </summary>
        public int Column
        {
            get { return _column; }
        }
    }
}

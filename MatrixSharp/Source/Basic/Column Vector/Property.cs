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
        /// ベクトルの要素
        /// </summary>
        /// <param name="i">要素番号</param>
        /// <returns></returns>
        public double this[int i]
        {
            get { return _array[i]; }
            set { _array[i] = value; }
        }

        /// <summary>
        /// ベクトルの次元
        /// </summary>
        public int Dimension
        {
            get { return _dim; }
        }
    }
}

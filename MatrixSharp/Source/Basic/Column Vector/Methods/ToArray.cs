using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class ColumnVector
    {
        /// <summary>
        /// 配列の複製を返す
        /// </summary>
        /// <returns></returns>
        public double[] ToArray()
        {
            return (double[])_array.Clone();
        }
    }
}

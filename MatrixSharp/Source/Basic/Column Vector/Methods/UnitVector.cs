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
        /// 単位ベクトルに変換する
        /// </summary>
        /// <returns></returns>
        public ColumnVector UnitVector()
        {
            double norm = Math.Sqrt(_array.Select(x => x * x).Sum());
            return new ColumnVector(_array.Select(x => x / norm).ToArray(), false);
        }
    }
}

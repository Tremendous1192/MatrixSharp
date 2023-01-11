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
        /// 単位ベクトルに変換する
        /// </summary>
        /// <returns></returns>
        public RowVector UnitVector()
        {
            double norm = Math.Sqrt(_array.Select(x => x * x).Sum());
            return new RowVector(_array.Select(x => x / norm).ToArray(), false);
        }
    }
}

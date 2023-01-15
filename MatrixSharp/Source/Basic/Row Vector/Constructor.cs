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
        /// 列ベクトルインスタンスを生成する
        /// </summary>
        /// <param name="dimension">次元</param>
        public RowVector(int dimension)
        {
            _dim = dimension;
            _array = new double[dimension];
        }

        /// <summary>
        /// 列ベクトルインスタンスを生成する
        /// </summary>
        /// <param name="array">配列</param>
        public RowVector(double[] array)
        {
            _array = (double[])array.Clone();
            _dim = array.Length;
        }

        /// <summary>
        /// 列ベクトルインスタンスを生成する
        /// </summary>
        /// <param name="array">配列</param>
        /// <param name="dummy">ダミー変数</param>
        internal RowVector(double[] array, bool dummy)
        {
            _array = array;
            _dim = array.Length;
        }
    }
}

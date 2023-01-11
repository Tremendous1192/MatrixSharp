using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public unsafe partial class Matrix
    {

        /// <summary>
        /// 行数・列数を指定して行列インスタンスを生成する
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="column">列数</param>
        public Matrix(int row, int column)
        {
            _array = new double[row, column];
            _row = row;
            _column = column;
        }

        /// <summary>
        /// 正方行列の行列インスタンスを生成する
        /// </summary>
        /// <param name="rowandColumn">行数・列数</param>
        public Matrix(int rowandColumn)
        {
            _array = new double[rowandColumn, rowandColumn];
            _row = rowandColumn;
            _column = rowandColumn;
        }

        /// <summary>
        /// 2次元配列を行列として行列インスタンスを生成する
        /// </summary>
        /// <param name="array">2次元配列</param>
        public Matrix(in double[,] array)
        {
            _array = (double[,])array.Clone();
            _row = array.GetLength(0);
            _column = array.GetLength(1);
        }

        /// <summary>
        /// 2次元配列を行列として行列インスタンスを生成する。
        /// 内部処理で new の回数を減らした
        /// </summary>
        /// <param name="array">2次元配列</param>
        /// <param name="dummy">ダミー変数</param>
        internal Matrix(in double[,] array, bool dummy)
        {
            _array = array;
            _row = array.GetLength(0);
            _column = array.GetLength(1);
        }
    }
}

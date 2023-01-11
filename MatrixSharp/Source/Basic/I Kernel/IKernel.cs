using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// カーネル
    /// </summary>
    public partial interface IKernel
    {
        // ハイパーパラメータ関係
        /// <summary>
        /// カーネル関数のハイパーパラメータをセットする
        /// </summary>
        /// <param name="hyperParameter">ハイパーパラメータ (1次元)</param>
        void SetHyperParameters(double[] hyperParameter);
        /// <summary>
        /// ハイパーパラメータの要素数を返す
        /// </summary>
        /// <returns>int</returns>
        int HyperParameterDimension();


        //基本のメソッド
        /// <summary>
        /// カーネル関数を計算する。
        /// </summary>
        /// <param name="vector01">行ベクトル</param>
        /// <param name="vector02">行ベクトル</param>
        /// <returns>double</returns>
        double Kernel(RowVector vector01, RowVector vector02);


        // 自分自身とのカーネル
        /// <summary>
        /// 2つのベクトル変数が等しい場合のカーネル関数を計算する。
        /// </summary>
        /// <remarks>
        /// (計算量削減のため作成した)
        /// </remarks>
        /// <param name="vector01">行ベクトル</param>
        /// <returns>double</returns>
        double KernelOneself(RowVector vector01);
        /// <summary>
        /// 2つのベクトル変数が等しい場合のカーネル関数を計算する。
        /// </summary>
        /// <remarks>
        /// (計算量削減のため作成した)
        /// </remarks>
        /// <param name="designMatrixTest">計画行列</param>
        /// <returns>ColumnVector</returns>
        ColumnVector KernelOneself(Matrix designMatrixTest);


        // 訓練データのグラム行列
        /// <summary>
        /// 訓練データのグラム行列を計算する。
        /// </summary>
        /// <param name="designMatrixTrain">訓練データの計画行列</param>
        /// <returns>Matrix</returns>
        Matrix GramMatrixTrain(Matrix designMatrixTrain);


        // テストデータのグラム行列
        /// <summary>
        /// テストデータのグラム行列を計算する
        /// </summary>
        /// <param name="designMatrixTest">テストデータの計画行列</param>
        /// <param name="designMatrixTrain">訓練データの計画行列</param>
        /// <returns>Matrix</returns>
        Matrix GramMatrixTest(Matrix designMatrixTest, Matrix designMatrixTrain);
        /// <summary>
        /// テストデータのグラム行ベクトルを計算する
        /// </summary>
        /// <param name="rowVectorTest">テストデータの行ベクトル</param>
        /// <param name="designMatrixTrain">訓練データの計画行列</param>
        /// <returns>RowVector</returns>
        RowVector GramVectorTest(RowVector rowVectorTest, Matrix designMatrixTrain);
    }
}

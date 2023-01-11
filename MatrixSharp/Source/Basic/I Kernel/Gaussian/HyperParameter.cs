using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// ガウシアンカーネル
    /// </summary>
    public unsafe partial class GaussianKernel : IKernel
    {
        bool setHyperParameter; // ハイパーパラメータを設定したかどうかの確認
        double[] hyperParameterArray; // ハイパーパラメータ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GaussianKernel()
        {
            setHyperParameter = false;
        }

        /// <summary>
        /// カーネル関数のハイパーパラメータをセットする
        /// </summary>
        /// <param name="hyperParameter"></param>
        public void SetHyperParameters(double[] hyperParameter)
        {
            if (hyperParameter.Length != 1)
            {
                throw new FormatException("ガウスカーネルのハイパーパラメータの次元は1次元です");
            }

            foreach (double d in hyperParameter)
            {
                if (d < 0)
                {
                    throw new FormatException("ガウスカーネルのハイパーパラメータの要素は非負の実数のみです");
                }
            }

            setHyperParameter = true;
            hyperParameterArray = hyperParameter;
        }

        /// <summary>
        /// ハイパーパラメータの要素数を返す
        /// </summary>
        /// <returns></returns>
        public int HyperParameterDimension()
        {
            return 1;
        }
    }
}

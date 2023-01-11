using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tremendous1192.SelfEmployed.MatrixSharp;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    /// <summary>
    /// 主成分分析
    /// </summary>
    public static unsafe partial class PrincipalComponentAnalysis
    {
        /// <summary>
        /// 計画行列の積和行列から、主成分を計算する
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <returns></returns>
        public static List<ColumnVector> PrincipalComponents(Matrix designMatrix)
        {
            List<ColumnVector> result = (designMatrix.Transpose() * designMatrix).EigenVectors(designMatrix.Column, true);
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = result[i].UnitVector();
            }
            return result;
        }

        /// <summary>
        /// 計画行列の積和行列から、主成分を計算する
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <param name="numberLatentVariables">潜在変数の数</param>
        /// <returns></returns>
        public static List<ColumnVector> PrincipalComponents(Matrix designMatrix, int numberLatentVariables)
        {
            List<ColumnVector> result = (designMatrix.Transpose() * designMatrix).EigenVectors(numberLatentVariables, true);
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = result[i].UnitVector();
            }
            return result;
        }
    }
}

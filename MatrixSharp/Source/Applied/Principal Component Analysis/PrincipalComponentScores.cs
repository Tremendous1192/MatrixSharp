using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tremendous1192.SelfEmployed.MatrixSharp;

namespace Tremendous1192.SelfEmployed.MatrixSharp
{
    public static unsafe partial class PrincipalComponentAnalysis
    {
        /// <summary>
        /// 計画行列の積和行列から、主成分得点を計算する
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <returns></returns>
        public static List<ColumnVector> PrincipalComponentScores(Matrix designMatrix)
        {
            List<ColumnVector> result = PrincipalComponentAnalysis
                .PrincipalComponents(designMatrix.Transpose() * designMatrix);

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = designMatrix * result[i];
            }

            return result;
        }

        /// <summary>
        /// 計画行列の積和行列から、主成分得点を計算する
        /// </summary>
        /// <param name="designMatrix">計画行列</param>
        /// <param name="numberLatentVariables">主成分数</param>
        /// <returns></returns>
        public static List<ColumnVector> PrincipalComponentScores(Matrix designMatrix, int numberLatentVariables)
        {
            List<ColumnVector> result = PrincipalComponentAnalysis
                .PrincipalComponents(designMatrix.Transpose() * designMatrix, numberLatentVariables);

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = designMatrix * result[i];
            }

            return result;
        }
    }
}

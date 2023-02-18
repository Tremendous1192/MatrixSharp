# MatrixSharp
It is simple matrix calculation package.
It provides matrix and vector and more like hand calculation.

# Constructor
```cs
using System;
// My package
using Tremendous1192.SelfEmployed.MatrixSharp;
namespace YourApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix m = new Matrix(in double[,] array);
            ColumnVector c = new ColumnVector(double[] array);
            RowVector r = new RowVector(double[] array);
        }
    }
}
```

[Japanese read me is in Zenn](https://zenn.dev/tremendous1192/articles/824b2d32381173)

[GitHub](https://github.com/Tremendous1192/MatrixSharp)

[Nuget](https://www.nuget.org/packages/MatrixSharp/)


# ver 0.0.7
Append internal function of faster matrix subtract.  
If your matrix element number (row x column) is multiples of 2, 3, or 4, 5, your calculation will be faster than usual.

# ver 0.0.6  
3/Feb./2023  
Modify Clone Method of Matrix and Vector classes.

# ver.0.0.5
Append internal function faster matrix addition.  
If your matrix element number (row x column) is multiples of 2, 3, or 4, 5, you are able to calculate matrix addition quickly.
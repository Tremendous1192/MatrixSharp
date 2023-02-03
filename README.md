# MatrixSharp
Provides matrix and vector and more like hand calculation.

```cs
namespace Tremendous1192.SelfEmployed.MatrixSharp
{
	public unsafe partial class ColumnVector{}
	public unsafe partial class Matrix{}
	public unsafe partial class RowVector{}
}
```

[Japanese read me is in Zenn](https://zenn.dev/tremendous1192/articles/824b2d32381173)

[GitHub](https://github.com/Tremendous1192/MatrixSharp)

[Nuget](https://www.nuget.org/packages/MatrixSharp/)


# ver 0.0.6  
Modify Clone Method of Matrix and Vector classes.

# ver.0.0.5
Append internal function faster matrix addition.  
If your matrix element number (row x column) is multiples of 2, 3, or 4, 5, you are able to calculate matrix addition quickly.
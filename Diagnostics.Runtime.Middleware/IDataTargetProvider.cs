using System;
using Microsoft.Diagnostics.Runtime;

namespace Diagnostics.Runtime.Middleware
{
    internal interface IDataTargetProvider : IDisposable
    {
        DataTarget GetDataTarget();
    }
}

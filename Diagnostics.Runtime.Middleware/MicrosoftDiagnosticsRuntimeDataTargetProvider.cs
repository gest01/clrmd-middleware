using System.Diagnostics;
using Microsoft.Diagnostics.Runtime;

namespace Diagnostics.Runtime.Middleware
{
    internal class MicrosoftDiagnosticsRuntimeDataTargetProvider : IDataTargetProvider
    {
        private DataTarget _target;

        public DataTarget GetDataTarget()
        {
            if (_target == null)
            {
                int pid = Process.GetCurrentProcess().Id;
                _target = DataTarget.CreateSnapshotAndAttach(pid);
            }

            return _target;
        }

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _target?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}

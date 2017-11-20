using System;
using System.Threading;

internal struct SynchronizationContextSwitcher : IDisposable
{
    private SynchronizationContext originalSyncContext;

    public static SynchronizationContextSwitcher Capture()
    {
        return new SynchronizationContextSwitcher
        {
            originalSyncContext = SynchronizationContext.Current
        };
    }

    public void Dispose()
    {
        SynchronizationContext.SetSynchronizationContext(originalSyncContext);
    }
}

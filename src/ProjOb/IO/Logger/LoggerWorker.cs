using System.Collections.Concurrent;

namespace ProjOb.IO
{
    public class LoggerWorker
    {
        private Task? _writingTask;
        private BlockingCollection<(ILogProvider provider, LogMessage message)> _queue = [];

        public ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();
        public CancellationTokenSource TokenSource;

        public LoggerWorker()
        {
            TokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            if (Logger.Enabled) return;

            _writingTask = Task.Run(() =>
            {
                try
                {
                    foreach (var pair in _queue.GetConsumingEnumerable(TokenSource.Token))
                    {
                        SendToProvider(pair.message, pair.provider);
                    }
                }
                catch (OperationCanceledException)
                {
                    while (_queue.TryTake(out var pair))
                    {
                        SendToProvider(pair.message, pair.provider);
                    }
                }
            });
        }

        public void Stop()
        {
            if (!Logger.Enabled) return;

            TokenSource.Cancel();
            _writingTask?.Wait();
        }

        public void ProcessAsync(LogMessage message, ILogProvider provider)
        {
            if (!Logger.Enabled) return;

            _queue.Add((provider, message));
        }

        public void Process(LogMessage message, ILogProvider provider)
        {
            if (!Logger.Enabled) return;

            SendToProvider(message, provider);
        }

        private void SendToProvider(LogMessage message, ILogProvider provider)
        {
            try
            {
                Lock.EnterWriteLock();
                provider.Log(message);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }
    }
}

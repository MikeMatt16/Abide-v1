using System;
using System.Threading;

namespace Abide.Wpf.Modules.ViewModel
{
    public abstract class BackgroundOperation : BaseViewModel
    {
        private bool isRunning = false;
        private bool successful = false;
        private WaitCallback completedCallback;
        private object state = null;
        private bool cancelPending = false;
        private string status = string.Empty;

        protected IProgress<int> ProgressReporter { get; private set; }
        protected bool IsCancelPending
        {
            get => cancelPending;
            private set
            {
                if (cancelPending != value)
                {
                    cancelPending = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Status
        {
            get => status;
            protected set
            {
                if (status != value)
                {
                    status = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool IsRunning
        {
            get => isRunning;
            private set
            {
                isRunning = value;
                NotifyPropertyChanged();
            }
        }
        public bool Successful
        {
            get => successful;
            protected set
            {
                if (successful != value)
                {
                    successful = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void Start(WaitCallback completedCallback)
        {
            if (completedCallback == null) throw new ArgumentNullException(nameof(completedCallback));
            if (isRunning) return;

            this.completedCallback = completedCallback;
            isRunning = true;
            state = OnStart();

            if (!ThreadPool.QueueUserWorkItem(OnBackground, state))
            {
                successful = false;
            }
        }
        public void Start(WaitCallback completedCallback, IProgress<int> progressReporter)
        {
            if (completedCallback == null) throw new ArgumentNullException(nameof(completedCallback));
            if (progressReporter == null) throw new ArgumentNullException(nameof(progressReporter));
            if (isRunning) return;

            this.completedCallback = completedCallback;
            ProgressReporter = progressReporter;
            isRunning = true;
            state = OnStart();

            if (!ThreadPool.QueueUserWorkItem(OnBackground, state))
            {
                successful = false;
            }
        }
        public void Cancel()
        {
            if (IsCancelPending) return;
            IsCancelPending = true;
        }

        protected virtual object OnStart()
        {
            return null;
        }
        protected virtual void OnBackground(object state) { }
        protected virtual void OnCancel() { }
        protected void Complete()
        {
            Dispatcher.Invoke(() => { completedCallback?.Invoke(state); });
        }
    }
}

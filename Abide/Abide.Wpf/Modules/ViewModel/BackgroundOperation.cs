using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Abide.Wpf.Modules.ViewModel
{
    public abstract class BackgroundOperation : BaseViewModel
    {
        private static readonly DependencyPropertyKey IsRunningPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(IsRunning), typeof(bool), typeof(BackgroundOperation), new PropertyMetadata());
        private static readonly DependencyPropertyKey StatusPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Status), typeof(string), typeof(BackgroundOperation), new PropertyMetadata(string.Empty));
        private static readonly DependencyPropertyKey ProgressPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Progress), typeof(int), typeof(BackgroundOperation), new PropertyMetadata(0));
        private static readonly DependencyPropertyKey MaximumPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(Maximum), typeof(int), typeof(BackgroundOperation), new PropertyMetadata(0));
        private static readonly DependencyPropertyKey ProgressVisibilityPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(ProgressVisibility), typeof(Visibility), typeof(BackgroundOperation), new PropertyMetadata(Visibility.Hidden));
        public static readonly DependencyProperty StatusProperty = StatusPropertyKey.DependencyProperty;
        public static readonly DependencyProperty ProgressProperty = ProgressPropertyKey.DependencyProperty;
        public static readonly DependencyProperty MaximumProperty = MaximumPropertyKey.DependencyProperty;
        public static readonly DependencyProperty ProgressVisibilityProperty = ProgressVisibilityPropertyKey.DependencyProperty;
        public static readonly DependencyProperty IsRunningProperty = IsRunningPropertyKey.DependencyProperty;

        private IProgressReporter ProgressReporter { get; set; } = new DefaultReporter();
        public Task BackgroundTask { get; private set; }
        public TimeSpan ElapsedTime { get; private set; }
        public bool IsRunning
        {
            get => (bool)GetValue(IsRunningProperty);
            private set => SetValue(IsRunningPropertyKey, value);
        }
        public string Status
        {
            get => (string)GetValue(StatusProperty);
            private set => SetValue(StatusPropertyKey, value);
        }
        public int Progress
        {
            get => (int)GetValue(ProgressProperty);
            private set => SetValue(ProgressPropertyKey, value);
        }
        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            private set => SetValue(MaximumPropertyKey, value);
        }
        public Visibility ProgressVisibility
        {
            get => (Visibility)GetValue(ProgressVisibilityProperty);
            private set => SetValue(ProgressVisibilityPropertyKey, value);
        }

        public void Start(WaitCallback completedCallback)
        {
            if (completedCallback == null)
            {
                throw new ArgumentNullException(nameof(completedCallback));
            }

            if (!IsRunning)
            {
                var start = DateTime.Now;
                IsRunning = true;
                var state = OnStart();
                BackgroundTask = Task.Run(() =>
                {
                    OnBackground(state);
                    ElapsedTime = DateTime.Now - start;
                    if (Dispatcher.CheckAccess())
                    {
                        Dispatcher.VerifyAccess();
                        completedCallback?.Invoke(state);
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            Dispatcher.VerifyAccess();
                            completedCallback?.Invoke(state);
                        });
                    }
                });
            }
        }
        public void Start(WaitCallback completedCallback, IProgressReporter progressReporter)
        {
            if (completedCallback == null)
            {
                throw new ArgumentNullException(nameof(completedCallback));
            }

            if (progressReporter == null)
            {
                throw new ArgumentNullException(nameof(progressReporter));
            }

            if (!IsRunning)
            {
                var start = DateTime.Now;
                IsRunning = true;
                var state = OnStart();
                ProgressReporter = progressReporter;
                BackgroundTask = Task.Run(() =>
                {
                    OnBackground(state);
                    ElapsedTime = DateTime.Now - start;
                    if (Dispatcher.CheckAccess())
                    {
                        Dispatcher.VerifyAccess();
                        completedCallback?.Invoke(state);
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            Dispatcher.VerifyAccess();
                            completedCallback?.Invoke(state);
                        });
                    }
                });
            }
        }
        public void Cancel()
        {
            // todo implement
        }
        protected void ReportStatus(string status)
        {
            if (Dispatcher.CheckAccess())
            {
                Dispatcher.VerifyAccess();
                Status = status ?? string.Empty;
                ProgressReporter?.ReportStatus(status);
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    Dispatcher.VerifyAccess();
                    Status = status ?? string.Empty;
                    ProgressReporter?.ReportStatus(status);
                });
            }
        }
        protected void ReportProgress(int progress)
        {
            if (Dispatcher.CheckAccess())
            {
                Dispatcher.VerifyAccess();
                Progress = progress;
                ProgressReporter?.Report(progress);
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    Dispatcher.VerifyAccess();
                    Progress = progress;
                    ProgressReporter?.Report(progress);
                });
            }
        }
        protected void ResetProgress(int maximum)
        {
            if (Dispatcher.CheckAccess())
            {
                Dispatcher.VerifyAccess();
                Maximum = maximum;
                ProgressReporter?.Reset(maximum);
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    Dispatcher.VerifyAccess();
                    Maximum = maximum;
                    ProgressReporter?.Reset(maximum);
                });
            }
        }
        protected void SetProgressVisibility(bool visible)
        {
            if (Dispatcher.CheckAccess())
            {
                Dispatcher.VerifyAccess();
                ProgressVisibility = visible ? Visibility.Visible : Visibility.Collapsed;
                ProgressReporter?.SetVisibility(visible);
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    Dispatcher.VerifyAccess();
                    ProgressVisibility = visible ? Visibility.Visible : Visibility.Collapsed;
                    ProgressReporter?.SetVisibility(visible);
                });
            }
        }

        protected virtual object OnStart()
        {
            return null;
        }
        protected virtual void OnBackground(object state) { }
        protected virtual void OnCancel() { }

        private class DefaultReporter : IProgressReporter
        {
            public string Status { get; }
            public int Maximum { get; }

            public bool Visible { get; set; } = true;
            public void ReportStatus(string status)
            {
            }
            public void Report(int value)
            {
            }
            public void Reset(int max)
            {
            }
            public void SetVisibility(bool visible)
            {
            }
        }
    }

    public interface IProgressReporter : IProgress<int>
    {
        string Status { get; }
        int Maximum { get; }
        bool Visible { get;}
        void Reset(int max);
        void ReportStatus(string status);
        void SetVisibility(bool visible);
    }
}

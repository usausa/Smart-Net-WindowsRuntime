﻿namespace Smart.UI.Xaml.ViewModels
{
    using System;
    using System.Reactive.Disposables;

    using Smart.UI.Xaml.Messaging;

    /// <summary>
    ///
    /// </summary>
    public abstract class DisposableViewModelBase : ViewModelBase, IDisposable
    {
        private CompositeDisposable disposables;

        /// <summary>
        ///
        /// </summary>
        protected CompositeDisposable Disposables
        {
            get { return disposables ?? (disposables = new CompositeDisposable()); }
        }

        /// <summary>
        ///
        /// </summary>
        protected DisposableViewModelBase()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="messenger"></param>
        protected DisposableViewModelBase(Messenger messenger)
            : base(messenger)
        {
        }

        /// <summary>
        ///
        /// </summary>
        ~DisposableViewModelBase()
        {
            Dispose(false);
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                disposables?.Dispose();
            }
        }
    }
}

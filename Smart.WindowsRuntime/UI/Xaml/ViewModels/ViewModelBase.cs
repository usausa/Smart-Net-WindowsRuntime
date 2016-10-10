﻿namespace Smart.UI.Xaml.ViewModels
{
    using Smart.ComponentModel;
    using Smart.UI.Xaml.Messaging;

    /// <summary>
    ///
    /// </summary>
    public abstract class ViewModelBase : NotificationObject
    {
        /// <summary>
        ///
        /// </summary>
        private Messenger messenger;

        /// <summary>
        ///
        /// </summary>
        public Messenger Messenger
        {
            get { return messenger ?? (messenger = new Messenger()); }
        }

        /// <summary>
        ///
        /// </summary>
        protected ViewModelBase()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="messenger"></param>
        protected ViewModelBase(Messenger messenger)
        {
            this.messenger = messenger;
        }
    }
}

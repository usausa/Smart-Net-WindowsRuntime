namespace Smart.UI.Xaml.Interactivity
{
    using System.Collections.Generic;
    using System.Reflection;

    using Microsoft.Xaml.Interactivity;

    using Smart.UI.Xaml.Messaging;

    using Windows.UI.Xaml;

    /// <summary>
    ///
    /// </summary>
    public class MessageBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "BindableProperty")]
        public static readonly DependencyProperty MessengerProperty =
            DependencyProperty.Register("MessengerProperty", typeof(Messenger), typeof(MessageBehavior), new PropertyMetadata(null, MessengerChanged));

        /// <summary>
        ///
        /// </summary>
        public Messenger Messenger
        {
            get { return (Messenger)GetValue(MessengerProperty); }
            set { SetValue(MessengerProperty, value); }
        }

        /// <summary>
        ///
        /// </summary>
        public object Message { get; set; }

        /// <summary>
        ///
        /// </summary>
        public IList<IMessageAction> Actions { get; } = new List<IMessageAction>();

        /// <summary>
        ///
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Unloaded += OnUnloaded;
        }

        /// <summary>
        ///
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.Unloaded -= OnUnloaded;

            base.OnDetaching();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (Messenger != null)
            {
                Messenger.Recieved -= MessengerOnRecieved;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private static void MessengerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
            {
                return;
            }

            var behavior = (MessageBehavior)obj;

            if ((e.OldValue != null) && (behavior.Messenger != null))
            {
                behavior.Messenger.Recieved -= behavior.MessengerOnRecieved;
            }

            if ((e.NewValue != null) && (behavior.Messenger != null))
            {
                behavior.Messenger.Recieved += behavior.MessengerOnRecieved;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessengerOnRecieved(object sender, MessengerEventArgs e)
        {
            if ((Message == null) || Message.Equals(e.Message))
            {
                foreach (var action in Actions)
                {
                    if ((action.ParameterType == null) ||
                        ((e.Parameter != null) && e.Parameter.GetType().GetTypeInfo().IsAssignableFrom(action.ParameterType)))
                    {
                        action.Invoke(AssociatedObject, e.Parameter);
                    }
                }
            }
        }
    }
}

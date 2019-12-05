using Abide.Tag;
using System.Windows;
using System.Windows.Controls;

namespace Abide.Guerilla.Wpf.Controls
{
    public abstract class BaseControl : UserControl
    {
        /// <summary>
        /// Identifies the <see cref="FieldChanged"/> event.
        /// </summary>
        public static readonly RoutedEvent FieldChangedEvent = 
            EventManager.RegisterRoutedEvent("FieldChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BaseControl));
        /// <summary>
        /// Identifies the <see cref="Field"/> property.
        /// </summary>
        public static readonly DependencyProperty FieldProperty =
            DependencyProperty.Register("Field", typeof(Field), typeof(BaseControl), new PropertyMetadata(FieldPropertyChanged));
        /// <summary>
        /// Occurs when the <see cref="Field"/> property is changed.
        /// </summary>
        public event RoutedEventHandler FieldChanged
        {
            add { AddHandler(FieldChangedEvent, value); }
            remove { RemoveHandler(FieldChangedEvent, value); }
        }
        /// <summary>
        /// Gets or sets the control's referenced field.
        /// </summary>
        public Field Field
        {
            get { return (Field)GetValue(FieldProperty); }
            set { SetValue(FieldProperty, value); }
        }
        /// <summary>
        /// Occurs when the <see cref="Field"/> property is changed.
        /// </summary>
        /// <param name="e">The <see cref="RoutedEventArgs"/> that contain event data.</param>
        protected virtual void OnFieldChanged(RoutedEventArgs e) { }
        
        private static void FieldPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Check
            if (d is BaseControl control)
            {
                //Create arguments
                RoutedEventArgs args = new RoutedEventArgs(FieldChangedEvent, control);

                //Call RaiseFieldChangedEvent(RoutedEventArgs)
                control.RaiseFieldChangedEvent(args);
            }
        }
        private void RaiseFieldChangedEvent(RoutedEventArgs e)
        {
            //Call OnFieldChanged
            OnFieldChanged(e);

            //Raise event
            RaiseEvent(e);
        }
    }
}

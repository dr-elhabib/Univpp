using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Univ
{
    /// <summary>
    /// Creates a clipping region from the parent <see cref="Border"/> <see cref="CornerRadius"/>
    /// </summary>
    public class brderProperty : BaseAttachedProperty<brderProperty, bool>
    {
        #region Private Members

        /// <summary>
        /// Called when the parent border first loads
        /// </summary>
        private RoutedEventHandler mBorder_Loaded;

        /// <summary>
        /// Called when the border size changes
        /// </summary>
        private SizeChangedEventHandler mBorder_SizeChanged;

        #endregion

        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

            // Get border
            var border = (Button)sender;
            if ((Boolean)e.NewValue)
                border.BorderThickness = new Thickness(2);
            else
                border.BorderThickness = new Thickness(0);
        }

        /// <summary>
        /// Called when the border is loaded and changed size
        /// </summary>
        /// <param name="sender">The border</param>
        /// <param name="e"></param>
        /// <param name="child">The child element (our selves)</param>
        private void Border_OnChange(object sender, RoutedEventArgs e, FrameworkElement child, Boolean value)
        {
            // Get border
            var border = (Border)sender;
            if (value)            
                border.BorderThickness = new Thickness(1);           
            else
                border.BorderThickness = new Thickness(0);

        }
    }
}
namespace Cinema.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    public partial class RatingBar : UserControl
    {
        public static readonly DependencyProperty RatingValueProperty = 
            DependencyProperty.Register("RatingValue", typeof(int), typeof(RatingBar), 
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                new PropertyChangedCallback(RatingValueChanged)));

        private int _maxValue = 10;

        public event EventHandler ButtonClick;

        public RatingBar()
        {
            this.InitializeComponent();
        }

        public int RatingValue
        {
            get
            {
                return (int)GetValue(RatingValueProperty) * 2;
            }

            set
            {
                if (value < 0)
                {
                    this.SetValue(RatingValueProperty, 0);
                }
                else if (value > this._maxValue)
                {
                    this.SetValue(RatingValueProperty, this._maxValue);
                }
                else
                {
                    this.SetValue(RatingValueProperty, value);
                }
            }
        }

        private static void RatingValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RatingBar parent = sender as RatingBar;
            int ratingValue = (int)e.NewValue;
            UIElementCollection children = ((Grid)(parent.Content)).Children;
            ToggleButton button = null;

            for (int i = 0; i < ratingValue; i++)
            {
                button = children[i] as ToggleButton;
                if (button != null)
                {
                    button.IsChecked = true;
                }
            }

            for (int i = ratingValue; i < children.Count; i++)
            {
                button = children[i] as ToggleButton;
                if (button != null)
                {
                    button.IsChecked = false;
                }
            }
        }

        private void RatingButtonClickEventHandler(object sender, RoutedEventArgs e)
        {
            ToggleButton button = sender as ToggleButton;

            int newRating = int.Parse((string)button.Tag);

            if ((bool)button.IsChecked || newRating < this.RatingValue)
            {
                this.RatingValue = newRating;
            }
            else
            {
                this.RatingValue = newRating - 1;
            }

            e.Handled = true;

            if (this.ButtonClick != null)
            {
                this.ButtonClick(this, e);
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace AnimationSample
{
    /// <summary>
    /// Interaction logic for MultiSampleViewer.xaml
    /// </summary>
    public partial class MultiSampleViewer : UserControl
    {
        public MultiSampleViewer()
        {
            InitializeComponent();
        }



        public int ViewIndex
        {
            get { return (int)GetValue(ViewIndexProperty); }
            set { SetValue(ViewIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewIndexProperty =
            DependencyProperty.Register("ViewIndex", typeof(int), typeof(MultiSampleViewer), new PropertyMetadata(0));


    }
}

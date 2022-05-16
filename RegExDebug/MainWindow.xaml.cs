using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegExDebug
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            sv_source_lbl.ScrollToVerticalOffset(e.VerticalOffset);
        }

        private void ScrollViewer_ScrollChanged_1(object sender, ScrollChangedEventArgs e)
        {
            sv_regex_lbl.ScrollToVerticalOffset(e.VerticalOffset);
        }

        private void ScrollViewer_ScrollChanged_2(object sender, ScrollChangedEventArgs e)
        {
            sv_result_lbl.ScrollToVerticalOffset(e.VerticalOffset);
        }
    }
}

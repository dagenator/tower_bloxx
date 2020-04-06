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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    

    public partial class MainWindow : Window
    {
        private BlockPhysic block = new BlockPhysic(200, 50, 70, 70);

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var rectangle = MakeRectangle(block.Position, block.Size, Brushes.Black);
            
            CanvasField.Children.Insert(0, rectangle);

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                block.ChangeBlockStatement();

            }
        }

        private Rectangle MakeRectangle(Vector position, Vector size, Brush brush)
        {
            
            var rect = new Rectangle();
            rect.Width = size.X;
            rect.Height = size.Y;
            rect.Fill = brush;
            Canvas.SetLeft(rect, position.X);
            Canvas.SetTop(rect, position.Y);
            return rect;
        }
    }
}

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
using System.Windows.Media.Animation;
using System.Timers;
using System.Windows.Threading;

namespace WpfApp1
{
    

    public partial class MainWindow : Window
    {
        MainLogic logic;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new MainLogic(this);
        }

        public void SetCanvasPosition(Image im, double x, double y)
        {
            Canvas.SetLeft(im, x);
            Canvas.SetTop(im, y);
        } 

        public void AddOnCanvas(Image im)
        {
            CanvasField.Children.Add(im);
        }

        public void DeleteBlock(Image im)
        {
            CanvasField.Children.Remove(im);
        }

        private void CanvasField_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            logic.MainButton();
        }
    }
}

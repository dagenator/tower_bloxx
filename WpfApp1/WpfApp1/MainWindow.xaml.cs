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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private BlockPhysic block = new BlockPhysic(200, 50, 150, 70);
        private BlockPhysic block1 = new BlockPhysic(400, 50, 70, 70);

        Image image = new Image();



        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //rectangle = MakeRectangle(block.Position, block.Size, Brushes.Black);
            
            //CanvasField.Children.Insert(0, rectangle);


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

       
       

        private Image CreateNewBlock(BlockPhysic block)
        {
            var image = new Image();
            image = new Image();
            image.Source = BitmapFrame.Create(new Uri(@"D:\Git\TowerBloxxGame\tower_bloxx\WpfApp1\WpfApp1\pictures\HouseTest.bmp"));// орефакторить
            image.Stretch = Stretch.Fill;
            image.Width = block.Size.X;
            image.Height = block.Size.Y;

            Canvas.SetLeft(image, block.Position.X);
            Canvas.SetTop(image, block.Position.Y);
            
            return image;
        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            image = CreateNewBlock(block);
            CanvasField.Children.Add(image);

            var timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            timer.Start();

        }

        private void StraightMoveAnimation(BlockPhysic block, Image im, double stepX, double stepY)
        {
            Canvas.SetTop(im, block.Position.Y);
            Canvas.SetLeft(im, block.Position.X);
            block.SetNewPosition(block.Position.X + stepX, block.Position.Y + stepY);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            StraightMoveAnimation(block, image, 0, 3);
        }

    }
}

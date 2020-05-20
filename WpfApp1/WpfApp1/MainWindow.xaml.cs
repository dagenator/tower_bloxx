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
        // 
        private BlockPhysic block = new BlockPhysic(200, 50, 150, 70);
        List<Tuple<Image, BlockPhysic>> Blocks = new List<Tuple<Image, BlockPhysic>>();
       
        double DeleteBorder = 1000;
        double fallSpeed = 5;

        ParabollAnimations parabola = new ParabollAnimations( 200, 100, 300, 15, 5);
        Image hook = new Image();

        DispatcherTimer SpavnTimer = new DispatcherTimer();
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var AnimationTimer = new DispatcherTimer();
            AnimationTimer.Tick += new EventHandler(AnimationTimer_Tick);
            AnimationTimer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            AnimationTimer.Start();

           
            SpavnTimer.Tick += new EventHandler(SpavnTimer_Tick);
            SpavnTimer.Interval = new TimeSpan(0, 0, 3);
            SpavnTimer.Start();


            hook = CreateNewImage(new BlockPhysic(parabola.DynamicPosition.X + parabola.Size.X/2, 
                parabola.DynamicPosition.Y+ parabola.Size.Y, 
                50, 
                50),
                BitmapFrame.Create(new Uri(@"D:\Git\TowerBloxxGame\tower_bloxx\WpfApp1\WpfApp1\pictures\23.png")));
            CanvasField.Children.Add(hook);


            BlockSpavn(new BlockPhysic(100, 650, 300, 100, State.InTower));
        }

        private void SpavnTimer_Tick(object sender, EventArgs e)
        {
            BlockSpavn(new BlockPhysic(parabola.DynamicPosition.X -50, parabola.DynamicPosition.Y +100, 150, 70, State.Swing));
            SpavnTimer.IsEnabled = false;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            AnimationProcessing();
            HookAnimation();
            //if (Blocks.Count >= 5)
            //    label.Content = Blocks[4].Item2.Position.Y.ToString();
        }

        private void BlockSpavn(BlockPhysic block)
        {
            Image im = CreateNewImage(block, BitmapFrame.Create(new Uri(@"D:\Git\TowerBloxxGame\tower_bloxx\WpfApp1\WpfApp1\pictures\HouseTest.bmp")));
            Blocks.Add( new Tuple<Image, BlockPhysic>(im, block));
            CanvasField.Children.Add(im);
        }

        private void HookAnimation()
        {
            Canvas.SetLeft(hook, parabola.DynamicPosition.X);
            Canvas.SetTop(hook, parabola.DynamicPosition.Y);
            parabola.MadeStep();
        }

        
       

        private void DeleteBlock(int index)
        {
            CanvasField.Children.Remove(Blocks[index].Item1);
            Blocks.RemoveAt(index);
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


        private Image CreateNewImage(BlockPhysic block, BitmapFrame bitmape)
        {
            var image = new Image();
            image = new Image();
            image.Source = bitmape;// BitmapFrame.Create(new Uri(@"D:\Git\TowerBloxxGame\tower_bloxx\WpfApp1\WpfApp1\pictures\HouseTest.bmp"));// орефакторить
            image.Stretch = Stretch.Fill;
            image.Width = block.Size.X;
            image.Height = block.Size.Y;

            Canvas.SetLeft(image, block.Position.X);
            Canvas.SetTop(image, block.Position.Y);
            return image;
        }

        private void SwingMoveAnimation(BlockPhysic block, Image im)
        {
            block.SetNewPosition(parabola.DynamicPosition.X -block.Size.X/2+ hook.Width/2, parabola.DynamicPosition.Y +block.Size.Y/2);
            Canvas.SetTop(im, block.Position.Y);
            Canvas.SetLeft(im, block.Position.X);
            
        }

        private void StraightMoveAnimation(BlockPhysic block, Image im, double stepX, double stepY)
        {
            Canvas.SetTop(im, block.Position.Y);
            Canvas.SetLeft(im, block.Position.X);
            block.SetNewPosition(block.Position.X + stepX, block.Position.Y + stepY);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            Blocks[Blocks.Count - 1].Item2.StartFall();
            SpavnTimer.IsEnabled = true;


        }
    }
}

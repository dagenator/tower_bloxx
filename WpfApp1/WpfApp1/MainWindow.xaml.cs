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
        double TowerHeight = 700;
        double DeleteBorder = 750;


        Image image = new Image();

        double fallSpeed = 7;



        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 18);
            timer.Start();

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            AnimationProcessing();
        }

        private void BlockSpavn(BlockPhysic block)
        {
            Image im = CreateNewImage(block);
            Blocks.Add( new Tuple<Image, BlockPhysic>(im, block));
            CanvasField.Children.Add(im);
        }

        private void AnimationProcessing()
        {
            for(int i = 0; i < Blocks.Count; i++)
            {
                if (Blocks[i].Item2.Position.Y > DeleteBorder) DeleteBlock(i);

                if (Blocks[i].Item2.State == State.Null
                    || Blocks[i].Item2.State == State.InTower) continue;
                else if(Blocks[i].Item2.State == State.Fall)
                {
                    if (Blocks[i].Item2.Position.Y+ Blocks[i].Item2.Size.Y >= TowerHeight)
                    {
                        Blocks[i].Item2.InTower();
                        TowerHeight = Blocks[i].Item2.Position.Y;
                    }
                        

                    StraightMoveAnimation(Blocks[i].Item2, Blocks[i].Item1, 0, fallSpeed);
                }
                

            }
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

       
       

        private Image CreateNewImage(BlockPhysic block)
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
            BlockSpavn(new BlockPhysic(200, 50, 150, 75));
            Blocks[Blocks.Count-1].Item2.StartFall();

        }
       

        private void StraightMoveAnimation(BlockPhysic block, Image im, double stepX, double stepY)
        {
            Canvas.SetTop(im, block.Position.Y);
            Canvas.SetLeft(im, block.Position.X);
            block.SetNewPosition(block.Position.X + stepX, block.Position.Y + stepY);
        }

       
    }
}

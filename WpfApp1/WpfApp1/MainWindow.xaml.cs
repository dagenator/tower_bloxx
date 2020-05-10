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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private BlockPhysic block = new BlockPhysic(200, 50, 70, 70);
        private BlockPhysic block1 = new BlockPhysic(400, 50, 70, 70);
        Rectangle rectangle = new Rectangle();

         Storyboard ellipseStoryboard = new Storyboard();


        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rectangle = MakeRectangle(block.Position, block.Size, Brushes.Black);
            
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

        private RectAnimation FallAnimation1(BlockPhysic fallBolock)
        {
            var animation1 = new RectAnimation();
            RectangleGeometry myRectangleGeometry = new RectangleGeometry();
            myRectangleGeometry.Rect = new Rect(fallBolock.Position.X, fallBolock.Position.Y, fallBolock.Size.X, fallBolock.Size.Y);
            animation1.To = new Rect(fallBolock.Position.X, fallBolock.Position.Y + 100, fallBolock.Size.X, fallBolock.Size.Y);
            animation1.Duration = TimeSpan.FromSeconds(5);
            Storyboard.SetTarget(animation1, rectangle);
            Storyboard.SetTargetProperty(animation1, new PropertyPath(MarginProperty));

            return animation1;
        }

        private void FallAnimation(BlockPhysic fallBolock)
        {

            // Create a NameScope for this page so that
            // Storyboards can be used.
            //NameScope.SetNameScope(this, new NameScope());
            //NameScope.GetNameScope(this);

            RectangleGeometry myRectangleGeometry = new RectangleGeometry();
           
            myRectangleGeometry.Rect = new Rect(fallBolock.Position.X, fallBolock.Position.Y, fallBolock.Size.X, fallBolock.Size.Y);

            // Assign the geometry a name so that
            // it can be targeted by a Storyboard.
            this.RegisterName(
                "MyAnimatedRectangleGeometry", myRectangleGeometry);

            Path myPath = new Path();
            myPath.Fill = Brushes.LemonChiffon;
            myPath.StrokeThickness = 1;
            myPath.Stroke = Brushes.Black;
            myPath.Data = myRectangleGeometry;

            RectAnimation myRectAnimation = new RectAnimation();
            myRectAnimation.Duration = TimeSpan.FromSeconds(0.9);
            myRectAnimation.FillBehavior = FillBehavior.HoldEnd;

            // Set the animation to repeat forever.
            //myRectAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Set the From and To properties of the animation.
            //myRectAnimation.From = new Rect(block1.Position.X, block1.Position.Y, block1.Size.X, block1.Size.Y);
            myRectAnimation.To = new Rect(fallBolock.Position.X, fallBolock.Position.Y + 250, fallBolock.Size.X, fallBolock.Size.Y);

            // Set the animation to target the Rect property
            // of the object named "MyAnimatedRectangleGeometry."
            Storyboard.SetTargetName(myRectAnimation, "MyAnimatedRectangleGeometry");
            Storyboard.SetTargetProperty(
                myRectAnimation, new PropertyPath(RectangleGeometry.RectProperty));

            // Create a storyboard to apply the animation.
           
            ellipseStoryboard.Children.Add(myRectAnimation);

            // Start the storyboard when the Path loads.
            //myPath.Loaded += delegate (object sender, RoutedEventArgs e)
            //{
            //    ellipseStoryboard.Begin(this);
            //};

            //Canvas containerCanvas = new Canvas();
            CanvasField.Children.Add(myPath);

            //Content = CanvasField;
        }

        private Image CreateNewBlock(BlockPhysic block)
        {
            var image = new Image();
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("HouseTest.bmp", UriKind.Relative);
            bi3.EndInit();
            image.Source = bi3;
            image.Width = block.Size.X;
            image.Stretch = Stretch.Fill;
            Canvas.SetLeft(image, block.Position.X);
            Canvas.SetTop(image, block.Position.Y);
            return image;
        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CanvasField.Children.Insert(1, CreateNewBlock(block));


            ////FallAnimation1(block1);
            //FallAnimation(block);
            ////FallAnimation(block1);
            //ellipseStoryboard.Begin(this);


        }
    }
}

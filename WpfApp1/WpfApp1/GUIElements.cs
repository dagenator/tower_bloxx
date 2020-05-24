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
    public class GUIElements
    {
        //MainWindow window = new MainWindow();
        //private Rectangle MakeRectangle(Vector position, Vector size, Brush brush)
        //{
        //    var rect = new Rectangle();
        //    rect.Width = size.X;
        //    rect.Height = size.Y;
        //    rect.Fill = brush;
        //    Canvas.SetLeft(rect, position.X);
        //    Canvas.SetTop(rect, position.Y);
        //    return rect;
        //}

        

        public Image CreateNewImage(BlockPhysic block, string path)
        {
            var image = new Image
            {
                Source = BitmapFrame.Create(new Uri(path)),// орефакторить
                Stretch = Stretch.Fill,
                Width = block.Size.X,
                Height = block.Size.Y
            };
            //window.SetCanvasPosition(image, block.Position.X, block.Position.Y);
            return image;
        }
    }
}

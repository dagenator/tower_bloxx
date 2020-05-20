using System.Windows;
using System;
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
    public class ParabollAnimations
    {
        //public BlockPhysic block { get; private set; }
        public Vector Size { get; private set; }
        public Vector DynamicPosition { get; private set; }
        public double Step { get; private set; }

        public double ParabolaK;
        public Vector FirstPosition { get; private set; }
        public Vector DynamicLocalPosition { get; private set; }
        private bool direction = true;

        public ParabollAnimations(double posX, double posY, double sizeX, double k, double step)
        {
            Size = new Vector(sizeX, 0);
            DynamicPosition = new Vector(posX, posY);
            Step = step;
            DynamicLocalPosition = new Vector(0, 0);
            FirstPosition = DynamicPosition;
            ParabolaK = k;
        }


        public void MadeStep()
        {
            double x = DynamicLocalPosition.X;
            if (direction) x += Step;
            else x -= Step;

            if (Math.Abs(x) >= Size.X / 2) direction = !direction;

            DynamicLocalPosition = new Vector(x, ParabolaCalculate(x));

            DynamicPosition = new Vector( FirstPosition.X + DynamicLocalPosition.X, FirstPosition.Y - DynamicLocalPosition.Y);

        }

        private double ParabolaCalculate(double x)
        {
            double y = (x/ParabolaK)*(x / ParabolaK);
            return y;
        }
    }
    /// <summary>
    /// /////       //
    /// </summary>
    public partial class MainWindow : Window
    {
        CalculateOfCollision calculator = new CalculateOfCollision();

        private bool GoingDown = false;
        private double goingDownDistance = 10;
        //double procent = 0;
        private void AnimationProcessing()
        {

            for (int i = 0; i < Blocks.Count; i++)
            {
                if(Blocks[i].Item2.Position.Y > )////
                if (Blocks[i].Item2.Position.Y > DeleteBorder) DeleteBlock(i);

                if (Blocks[i].Item2.State == State.InTower && GoingDown)
                {
                    GoingDownAnimation(Blocks[i].Item2, Blocks[i].Item1, 1, 70);
                }
                else if (Blocks[i].Item2.State == State.Fall)
                {
                    if (calculator.CollisionDetect( Blocks[i].Item2, Blocks[i - 1].Item2))
                    {
                        Blocks[i].Item2.InTower();
                        GoingDown = true;
                        label.Content = calculator.GetProcentOfTouching(Blocks[i].Item2, Blocks[i - 1].Item2);
                    }else
                        StraightMoveAnimation(Blocks[i].Item2, Blocks[i].Item1, 0, fallSpeed);
                }
                else if (Blocks[i].Item2.State == State.Swing)
                {
                    SwingMoveAnimation(Blocks[i].Item2, Blocks[i].Item1);
                }


            }
        }

        private void GoingDownAnimation(BlockPhysic block, Image im,double stepY, double distance)
        {
            
            StraightMoveAnimation(block, im, 0, stepY);
            if(Blocks[Blocks.Count-1].Item2 == block)
                goingDownDistance -= stepY;

            if (goingDownDistance < 0)
            {
                GoingDown = false;
                goingDownDistance = distance;
            }

        }

    }
}

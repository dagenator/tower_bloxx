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
using System.Linq;
using System.Windows.Threading;
using System.Collections.Generic;

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


    public class Animations
    {
        private GUIElements gui = new GUIElements();
        private MainLogic logic;

        private CalculateOfCollision calculator = new CalculateOfCollision();

        public ParabollAnimations parabola = new ParabollAnimations(200, 100, 300, 15, 3);

        private MainWindow window;

        private double fallSpeed = 5;
        private double DeleteBorder = 1000;
        private bool GoingDown = false;
        private double goingDownDistance = 10;


        public Animations(MainLogic l, MainWindow w)
        {
            logic = l;
            window = w;
        }

        public void AnimationProcessing(List<Tuple<Image, BlockPhysic>> Blocks)
        {

            for (int i = 0; i < Blocks.Count; i++)
            {
                //if(Blocks[i].Item2.Position.Y > )////
                if (Blocks[i].Item2.Position.Y > DeleteBorder) logic.DeleteBlock(i);

                if (Blocks[i].Item2.State == State.InTower && GoingDown)
                {
                    GoingDownAnimation(Blocks[i].Item2, Blocks[i].Item1, 1, 70, i == IndexOfLastBlockInTower(Blocks)); // logic.Blocks[logic.Blocks.Count-1]
                }
                else if (Blocks[i].Item2.State == State.Fall)
                {
                    if (calculator.CollisionDetect(Blocks[i].Item2, Blocks[i - 1].Item2))
                    {
                        Blocks[i].Item2.InTower();
                        GoingDown = true;
                        //window.label.Content = GoingDown + " " + goingDownDistance;// calculator.GetProcentOfTouching(Blocks[i].Item2, Blocks[i - 1].Item2);
                    }
                    else
                        StraightMoveAnimation(Blocks[i].Item2, Blocks[i].Item1, 0, fallSpeed);
                }
                else if (Blocks[i].Item2.State == State.Swing)
                {
                    SwingMoveAnimation(Blocks[i].Item2, Blocks[i].Item1);
                }
            }
        }

        private int IndexOfLastBlockInTower(List<Tuple<Image, BlockPhysic>> Blocks)
        {
            for (int i = Blocks.Count-1; i > 0; i--)
            {
                if (Blocks[i].Item2.State == State.InTower) 
                    return i;
            }
            throw new Exception();
        }

        public void HookAnimation(Image hook)
        {
            window.SetCanvasPosition(hook, parabola.DynamicPosition.X, parabola.DynamicPosition.Y);
            parabola.MadeStep();
        }


        public void StraightMoveAnimation(BlockPhysic block, Image im, double stepX, double stepY)
        {
            window.SetCanvasPosition(im, block.Position.X, block.Position.Y);
            block.SetNewPosition(block.Position.X + stepX, block.Position.Y + stepY);
        }

        public void SwingMoveAnimation(BlockPhysic block, Image im)
        {
            block.SetNewPosition(parabola.DynamicPosition.X - block.Size.X / 2 + block.Size.X / 2, parabola.DynamicPosition.Y + block.Size.Y / 2);
            window.SetCanvasPosition(im, block.Position.X, block.Position.Y);
        }

       

        public void GoingDownAnimation(BlockPhysic block, Image im, double stepY, double distance, bool isLastBlock)
        {

            StraightMoveAnimation(block, im, 0, stepY);
            if (isLastBlock)
                goingDownDistance -= stepY;

            if (goingDownDistance < 0)
            {
                GoingDown = false;
                goingDownDistance = distance;
            }

        }

    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Dynamic;
using System.Windows.Documents;

namespace WpfApp1
{
    public enum Level
    {
        Easy, 
        Hormal, 
        Hard
    }

    public class MainLogic
    {
        
        MainWindow window;
        Animations animation;
        GUIElements gui = new GUIElements();

        public List<Tuple<Image, BlockPhysic>> Blocks = new List<Tuple<Image, BlockPhysic>>();

        DispatcherTimer AnimationTimer = new DispatcherTimer();
        DispatcherTimer SpavnTimer = new DispatcherTimer();

        Image hook = new Image();
        int Scores = 0;

       
        public Level level = Level.Easy;


        public MainLogic(MainWindow w)
        {
            window = w;
            animation = new Animations(this, window);

            AnimationTimer.Tick += new EventHandler(AnimationTimer_Tick);
            AnimationTimer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            AnimationTimer.Start();

            SpavnTimer.Tick += new EventHandler(SpavnTimer_Tick);
            SpavnTimer.Interval = new TimeSpan(0, 0, 0, 0, 1500);
            SpavnTimer.Start();

            BlockSpavn(new BlockPhysic(100, 650, 300, 100, State.InTower));
            AddHook();
        }



        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animation.AnimationProcessing(Blocks);
            animation.HookAnimation(hook);
             
        }

        private void SpavnTimer_Tick(object sender, EventArgs e)
        {
            BlockSpavn(new BlockPhysic(animation.parabola.DynamicPosition.X - 50, animation.parabola.DynamicPosition.Y + 100, 150, 70, State.Swing));
            SpavnTimer.IsEnabled = false;
        }

        public void SpavnTimerIs(bool b)
        {
            SpavnTimer.IsEnabled = b;
        }

        public void DeleteBlock(int index)
        {
            window.DeleteBlock(Blocks[index].Item1);
            Blocks.RemoveAt(index);
        }

        public void LastBlockStartFall()
        {
            Blocks[^1].Item2.StartFall(); ;
        }

        private void BlockSpavn(BlockPhysic block)
        {
            Image im = gui.CreateNewImage(block, @"D:\Git\TowerBloxxGame\tower_bloxx\WpfApp1\WpfApp1\pictures\HouseTest.bmp");
            Blocks.Add(new Tuple<Image, BlockPhysic>(im, block));
            window.SetCanvasPosition(im, block.Position.X, block.Position.Y);
            window.AddOnCanvas(im);
        }

        public void MainButton()
        {
            LastBlockStartFall();
            SpavnTimerIs(true);
        }

        private void AddHook()
        {
            hook = gui.CreateNewImage(new BlockPhysic(
                animation.parabola.DynamicPosition.X + animation.parabola.Size.X / 2,
                animation.parabola.DynamicPosition.Y + animation.parabola.Size.Y,
                50, 50),
                @"D:\Git\TowerBloxxGame\tower_bloxx\WpfApp1\WpfApp1\pictures\23.png");
            window.SetCanvasPosition(hook, animation.parabola.DynamicPosition.X + animation.parabola.Size.X / 2, animation.parabola.DynamicPosition.Y + animation.parabola.Size.Y);
            window.AddOnCanvas(hook);
        }

        public void ScoreCount(double procent)
        {
            if(Blocks.Count > 2)
            {
                double half = Math.Abs(procent) / 2;
                if (half >= 45) Scores += 30;
                if (half >= 30 && half < 45) Scores += 20;
                if (half >= 20 && half < 30) Scores += 10;
                if (half > 10 && half < 20) Scores += 5;
                if (half <= 10) GameOver();

                window.label.Content = Scores.ToString();
            }
           
        }

        public void GameOver()
        {
            AnimationTimer.Stop();
            SpavnTimer.Stop();

        }

    }
}

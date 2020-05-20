using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public enum State
    {
        Null,
        Swing,
        Fall, 
        InTower
    }

    public class Vector
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public Vector(double inputX, double inputY)
        {
            X = inputX;
            Y = inputY;
        }
        public void SetX(double x)
        {
            X = x;
        }
        public void SetY(double y)
        {
            Y = y;
        }
    }

    public  class BlockPhysic
    {

        // public const float gravity = 9.8f;
        public Vector Position { get; private set; }
        public Vector Size { get; private set; }
        //public bool isFalling { get; private set; }

        public State State  { get; private set;}
      


        public event Action<Vector> BlockFalling;
        public BlockPhysic(double posX, double posY, double sizeX, double sizeY)
        {
            Position = new Vector(posX, posY);
            Size = new Vector(sizeX, sizeY);
            State = State.Null;
        }
        public BlockPhysic(double posX, double posY, double sizeX, double sizeY, State state)
        {
            Position = new Vector(posX, posY);
            Size = new Vector(sizeX, sizeY);
            State = state;
        }

        //public void ChangeBlockStatement()
        //{
        //    this.isFalling = !this.isFalling;
        //    MovingDown();

        //    //if (BlockFalling != null) BlockFalling(Size);
        //}

        public void StartFall()
        {
            if(State == State.Swing)
                State = State.Fall;
        }

        public void Stop()
        {
            State = State.Null;
        }
        public void InTower()
        {
            State = State.InTower;
        }
        public void SetNewPosition(double newX, double newY)
        {
            Position = new Vector(newX, newY);
        }

        private void MovingDown()
        {
            Position = new Vector(Position.X, Position.Y + 20);
        }
    }
}

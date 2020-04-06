using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Vector
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public Vector(double inputX, double inputY)
        {
            X = inputX;
            Y = inputY;
        }
    }

    public class BlockPhysic
    {
        // public const float gravity = 9.8f;
        public Vector Position { get; private set; }
        public Vector Size { get; private set; }
        public bool isFalling { get; private set; }

        public event Action<Vector> BlockFalling;
        public BlockPhysic(int posX, int posY, int sizeX, int sizeY)
        {
            Position = new Vector(posX, posY);
            Size = new Vector(sizeX, sizeY);
        }

        public void ChangeBlockStatement()
        {
            this.isFalling = !this.isFalling;
            MovingDown();

            //if (BlockFalling != null) BlockFalling(Size);
        }

        private void MovingDown()
        {
            Position = new Vector(Position.X, Position.Y + 20);
        }
    }
}

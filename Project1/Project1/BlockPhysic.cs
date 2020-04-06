using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Vector
    {
        readonly int X;
        readonly int Y;
        public Vector(int inputX, int inputY)
        {
            X = inputX;
            Y = inputY;
        }
    }

    class BlockPhysic
    {
        // public const float gravity = 9.8f;
        public Vector Position { get; private set; }
        public Vector Size { get; private set; }
        public bool isFalling  { get; private set; }
        public BlockPhysic(int posX, int posY, int sizeX, int sizeY)
        {
            Position = new Vector(posX, posY);
            Size = new Vector(sizeX, sizeY);
        }

        public void ChangeBlockStatement(BlockPhysic block)
        {
            isFalling = !isFalling;
        }
       
        public event Action<int, int> BlockFalling;
        
        //private void FallingMove(this BlockPhysic block, PaintEventArgs)
        //{

        //}
    }
}

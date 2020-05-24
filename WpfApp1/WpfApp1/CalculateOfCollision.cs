using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1
{
    public class CalculateOfCollision
    {
        public bool CollisionDetect(BlockPhysic b1, BlockPhysic b2)
        {
            double err = -1;
            return (b1.Position.X <= b2.Position.X + b2.Size.X + err
                && b1.Position.X + b1.Size.X + err >= b2.Position.X
                && b1.Position.Y <= b2.Position.Y + b2.Size.Y + err
                && b1.Position.Y + b1.Size.Y + err >= b2.Position.Y);

        }

        /// <summary>
        /// Функция высчитавающая процент касания блоков.
        /// Возвращает значение от -100 до 100.
        /// Если блок касается правой стороны, то от правой точки нижнего блока идет подсчет положительных процентов
        /// касание на 50,30 процентов справа
        /// Если бок касается с левой стороны, то подсчет идет от левого края. Процент отрицательный.
        /// </summary>
        public double GetProcentOfTouching(BlockPhysic above, BlockPhysic under) 
        {
            if (above.Position.X + above.Size.X < under.Position.X)
                return -1;
            else if (above.Position.X >= under.Position.X && above.Position.X < under.Position.X + under.Size.X)
                return (int)((((under.Position.X + under.Size.X) - above.Position.X) / under.Size.X) * 100);
            else if (above.Position.X + above.Size.X > under.Position.X && above.Position.X + above.Size.X < under.Position.X + under.Size.X)
                return -1 * (int)(((above.Position.X + above.Size.X - under.Position.X) / under.Size.X) * 100);
            else 
                return 1;
        }



    }

    
}

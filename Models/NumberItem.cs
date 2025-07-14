using System.Windows.Shapes;

namespace AlgoPaws.Models
{
    public class NumberItem
    {
        public int Value { get; set; }
        public Rectangle Rectangle { get; set; }

        public NumberItem(int value, Rectangle rectangle)
        {
            Value = value;
            Rectangle = rectangle;
        }
    }
}
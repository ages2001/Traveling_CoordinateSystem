namespace Traveling_CoordinateSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CoordinateSystem coordinate1 = new CoordinateSystem(20, 100, 100);
            coordinate1.printPoints();

            CoordinateSystem coordinate2 = new CoordinateSystem(50, 100, 100);
            coordinate2.printPoints();

            coordinate1.createDM();
            coordinate1.traverse();
        }
    }
}

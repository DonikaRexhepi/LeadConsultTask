using System;
using System.Collections.Generic;
using System.Text;

namespace LeadConsultTask
{
    public class Coordinate
    {
        public Coordinate(double x, double y)
        {
            X = x;
            Y = y;
        }
        public double X { get; set; }
        public double Y { get; set; }

        public string QuadrantOfTheCoordinate
        {
            get
            {
                if (X > 0 && Y > 0)
                {
                    return "1st Quadrant";
                }
                if (X < 0 && Y > 0)
                {
                    return "2nd Quadrant";
                }
                if (X < 0 && Y < 0)
                {
                    return "3rd Quadrant";
                }
                return "4th Quadrant";
            }
        }

        public double DistanceFromCenter =>
            //euclidian distance between two points
            //since the inital point of reference is 0,0 the mathematic formula is simplified to the below:
            Math.Sqrt(X * X + Y * Y);
    }
}

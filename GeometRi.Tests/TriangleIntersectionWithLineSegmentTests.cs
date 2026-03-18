using System;
using static System.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeometRi;

namespace GeometRi.Tests
{
    [TestClass]
    public class TriangleIntersectionWithLineSegmentTests
    {


        [TestMethod]
        public void TriangleNone()
        {
            var lineSegment = new Segment3d(new Point3d(2, 2, -1), new Point3d(2, 2, 1));
            var triangle = new Triangle(
                new Point3d(0, 0, 0),
                new Point3d(0, 2, 0),
                new Point3d(2, 0, 0));

            var intersection = triangle.IntersectionWith(lineSegment) as Point3d;

            Assert.AreEqual(null, intersection);
        }

        [TestMethod]
        public void TriangleStraight()
        {
            var lineSegment = new Segment3d(new Point3d(1, 1, -1), new Point3d(1, 1, 1));
            var triangle = new Triangle(
                new Point3d(0, 0, 0),
                new Point3d(0, 2, 0),
                new Point3d(2, 0, 0));

            var intersection = triangle.IntersectionWith(lineSegment) as Point3d;

            Assert.AreEqual(new Point3d(1, 1, 0), intersection);
        }

        [TestMethod]
        public void TriangleSkewedTriangle()
        {
            var lineSegment = new Segment3d(new Point3d(1, 1, -1), new Point3d(1, 1, 1));
            var triangle = new Triangle(
                new Point3d(0, 0, 1),
                new Point3d(0, 2, -1),
                new Point3d(2, 0, 0));

            var intersection = triangle.IntersectionWith(lineSegment) as Point3d;

            Assert.AreEqual(new Point3d(1, 1, -0.5), intersection);
        }

        [TestMethod]
        public void TriangleSkewedLine()
        {
            var lineSegment = new Segment3d(new Point3d(.7, 0.6, -1), new Point3d(1, 1, 1));
            var triangle = new Triangle(
                new Point3d(0, 0, 0),
                new Point3d(0, 2, 0),
                new Point3d(2, 0, 0));

            var intersection = triangle.IntersectionWith(lineSegment) as Point3d;

            Assert.AreEqual(new Point3d(0.85, 0.8, 0), intersection);
        }

        [TestMethod]
        public void TriangleSkewed()
        {
            var lineSegment = new Segment3d(new Point3d(.7, 0.6, -1), new Point3d(1, 1, 1));
            var triangle = new Triangle(
                new Point3d(0, 0, 1),
                new Point3d(0, 2, -1),
                new Point3d(2, 0, 0));

            var intersection = triangle.IntersectionWith(lineSegment) as Point3d;

            var expected = new Point3d(0.82352941176470584, 0.76470588235294112, -0.17647058823529405);
            double dist = expected.DistanceTo(intersection);
            Assert.IsTrue(GeometRi3D.AlmostEqual(dist, 0));
        }

        [TestMethod]
        public void TriangleSegment()
        {
            var lineSegment = new Segment3d(new Point3d(0, 0, 0), new Point3d(1, 1, 0));
            var triangle = new Triangle(
                new Point3d(0, 0, 0),
                new Point3d(0, 2, 0),
                new Point3d(2, 0, 0));

            var intersection = triangle.IntersectionWith(lineSegment) as Segment3d;

            Assert.AreEqual(intersection, new Segment3d(
                new Point3d(0, 0, 0),
                new Point3d(1, 1, 0)));
        }

        [TestMethod]
        public void TriangleSegmentPartial()
        {
            var lineSegment = new Segment3d(new Point3d(-1, 0, 0), new Point3d(1, 3, 0));
            var triangle = new Triangle(
                new Point3d(0, 0, 0),
                new Point3d(0, 2, 0),
                new Point3d(2, 0, 0));

            var intersection = triangle.IntersectionWith(lineSegment) as Segment3d;

            Assert.AreEqual(intersection, new Segment3d(
                new Point3d(0, 1.5, 0),
                new Point3d(0.2, 1.8, 0)));
        }

        [TestMethod]
        public void TriangleSegmentAB()
        {
            var lineSegment = new Segment3d(new Point3d(.5, .5, 0), new Point3d(.5, -.5, 0));
            var triangle = new Triangle(
                new Point3d(0, 0, 0),
                new Point3d(1, 0, 0),
                new Point3d(0, 1, 0));

            var intersectionWith = triangle.IntersectionWith(lineSegment);
            Assert.IsTrue(intersectionWith is Segment3d);

            var intersection = intersectionWith as Segment3d;
            Assert.AreEqual(intersection, new Segment3d(
                new Point3d(.5, .5, 0),
                new Point3d(.5, 0, 0)));
        }

        [TestMethod]
        public void TriangleSegmentCAMiddleOfAB_BC()
        {
            var lineSegment = new Segment3d(new Point3d(0.75, 0.25, 0), new Point3d(0, 1, 0));
            var triangle = new Triangle(
                new Point3d(0, 0, 0),
                new Point3d(1, 0, 0),
                new Point3d(1, 1, 0));

            var intersectionWith = triangle.IntersectionWith(lineSegment);
            Assert.IsTrue(intersectionWith is Segment3d);

            var intersection = intersectionWith as Segment3d;
            Assert.AreEqual(intersection, new Segment3d(
                new Point3d(0.75, 0.25, 0),
                new Point3d(.5, .5, 0)));
        }

        [TestMethod]
        public void TriangleSegmentCA()
        {
            var lineSegment = new Segment3d(new Point3d(1.1, 0, 0), new Point3d(0, 1.1, 0));
            var triangle = new Triangle(
                new Point3d(0.25, -0.25, 0),
                new Point3d(1.25, -0.25, 0),
                new Point3d(1.25, 0.75, 0));

            var intersectionWith = triangle.IntersectionWith(lineSegment);
            Assert.IsTrue(intersectionWith is Segment3d);

            var intersection = intersectionWith as Segment3d;
            Assert.AreEqual(intersection, new Segment3d(
                new Point3d(1.1, 0, 0),
                new Point3d(.8, .3, 0)));
        }
    }
}

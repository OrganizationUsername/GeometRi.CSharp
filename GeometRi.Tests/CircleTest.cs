﻿using System;
using static System.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeometRi;

namespace GeometRi_Tests
{
    [TestClass]
    public class CircleTest
    {
        //===============================================================
        // Circle3d tests
        //===============================================================

        [TestMethod()]
        public void CircleBy3PointsTest()
        {
            Point3d p1 = new Point3d(-3, 0, 4);
            Point3d p2 = new Point3d(4, 0, 5);
            Point3d p3 = new Point3d(1, 0, -4);

            Circle3d c = new Circle3d(p1, p2, p3);

            Assert.IsTrue(c.Center == new Point3d(1, 0, 1));
            Assert.IsTrue(Abs(c.R - 5) <= GeometRi3D.Tolerance);
        }

        [TestMethod()]

        public void CircleIntersectionWithPlaneTest()
        {
            // parallel obecjts
            Circle3d c = new Circle3d(new Point3d(5, 6, 1), 5, new Vector3d(0, 0, 1));
            Plane3d s = new Plane3d(new Point3d(0, 0, 0), new Vector3d(0, 0, 1));
            Assert.AreEqual(c.IntersectionWith(s), null);

            // coplanar objects
            s = new Plane3d(new Point3d(0, 0, 1), new Vector3d(0, 0, 1));
            Assert.AreEqual(c.IntersectionWith(s), c);

            // nonintersecting objects
            c = new Circle3d(new Point3d(5, 6, 10), 5, new Vector3d(0, 0, 1));
            s = new Plane3d(new Point3d(0, 0, 1), new Vector3d(0, 0, 1));
            Assert.AreEqual(c.IntersectionWith(s), null);

            // intersection in one point
            c = new Circle3d(new Point3d(0, 0, 3), 5, new Vector3d(3, 0, 4));
            s = new Plane3d(new Point3d(5, 5, 0), new Vector3d(0, 0, 1));
            Assert.AreEqual(c.IntersectionWith(s), new Point3d(4, 0, 0));

            // intersection in two points
            c = new Circle3d(new Point3d(0, 0, 3), 5, new Vector3d(3, 0, 0));
            s = new Plane3d(new Point3d(5, 5, 0), new Vector3d(0, 0, 1));
            Assert.AreEqual(c.IntersectionWith(s), new Segment3d(new Point3d(0, 4, 0), new Point3d(0, -4, 0)));

        }

        [TestMethod()]
        public void CircleParametricFormTest()
        {
            Circle3d c = new Circle3d(new Point3d(5, 6, 1), 5, new Vector3d(3, 0, 1));
            Assert.IsTrue(c.ParametricForm(0.5).BelongsTo(c));
        }

        [TestMethod()]
        public void CircleToEllipseTest()
        {
            Circle3d c = new Circle3d(new Point3d(5, 6, 1), 5, new Vector3d(3, 0, 1));
            Ellipse e = c.ToEllipse;

            Assert.IsTrue(c.ParametricForm(0.5).BelongsTo(e));
            Assert.IsTrue(c.ParametricForm(0.725).BelongsTo(e));
            Assert.IsTrue(c.ParametricForm(2.7215).BelongsTo(e));

            Assert.IsTrue(e.ParametricForm(0.5).BelongsTo(c));
            Assert.IsTrue(e.ParametricForm(0.725).BelongsTo(c));
            Assert.IsTrue(e.ParametricForm(2.7215).BelongsTo(c));
        }

        [TestMethod()]
        public void CircleProjectionToPlaneTest()
        {
            Vector3d v1 = new Vector3d(3, 5, 1);
            Circle3d c = new Circle3d(new Point3d(5, 6, 1), 5, v1);
            Plane3d s = new Plane3d(5, 2, 3, -3);

            Point3d p = c.ParametricForm(0.5).ProjectionTo(s);
            Assert.IsTrue(p.BelongsTo(c.ProjectionTo(s)));
            p = c.ParametricForm(0.725).ProjectionTo(s);
            Assert.IsTrue(p.BelongsTo(c.ProjectionTo(s)));
            p = c.ParametricForm(2.7122).ProjectionTo(s);
            Assert.IsTrue(p.BelongsTo(c.ProjectionTo(s)));
        }
    }
}
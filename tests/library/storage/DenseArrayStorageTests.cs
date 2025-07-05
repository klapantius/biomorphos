using System;
using NUnit.Framework;
using biomorphos.library.storage;
using biomorphos.tests.library.TestUtils;

namespace biomorphos.tests.library.storage
{
    [TestFixture]
    public class DenseArrayStorageTests
    {
        [Test]
        public void Add_And_Get_Works_For_ValidCoordinates()
        {
            var storage = new DenseArrayStorage<DummyCoordinates>(3, 3);
            var coord = new DummyCoordinates(1, 2);
            var cell = new DummyCell();
            storage.Add(coord, cell);
            Assert.AreSame(cell, storage.Get(coord));
        }

        [Test]
        public void Get_ReturnsNull_If_NotPresent()
        {
            var storage = new DenseArrayStorage<DummyCoordinates>(2, 2);
            var coord = new DummyCoordinates(0, 1);
            Assert.IsNull(storage.Get(coord));
        }

        [Test]
        public void Remove_Sets_Cell_To_Null()
        {
            var storage = new DenseArrayStorage<DummyCoordinates>(2, 2);
            var coord = new DummyCoordinates(1, 1);
            var cell = new DummyCell();
            storage.Add(coord, cell);
            storage.Remove(coord);
            Assert.IsNull(storage.Get(coord));
        }

        [Test]
        public void Throws_On_OutOfBounds_Coordinate()
        {
            var storage = new DenseArrayStorage<DummyCoordinates>(2, 2);
            var coord = new DummyCoordinates(2, 0); // out of bounds
            Assert.Throws<ArgumentOutOfRangeException>(() => storage.Get(coord));
            Assert.Throws<ArgumentOutOfRangeException>(() => storage.Add(coord, new DummyCell()));
            Assert.Throws<ArgumentOutOfRangeException>(() => storage.Remove(coord));
        }

        [Test]
        public void Throws_On_Wrong_Dimension_Coordinate()
        {
            var storage = new DenseArrayStorage<DummyCoordinates>(2, 2);
            var coord = new DummyCoordinates(1, 1, 1); // 3D, but storage is 2D
            Assert.Throws<ArgumentException>(() => storage.Get(coord));
        }
        [Test]
        public void GetIndex_2D_CorrectIndex()
        {
            var storage = new DenseArrayStorage<DummyCoordinates>(4, 5);
            Assert.AreEqual(0, storage.GetIndex(new DummyCoordinates(0, 0)));
            Assert.AreEqual(1, storage.GetIndex(new DummyCoordinates(1, 0)));
            Assert.AreEqual(4, storage.GetIndex(new DummyCoordinates(0, 1)));
            Assert.AreEqual(9, storage.GetIndex(new DummyCoordinates(1, 2)));
            Assert.AreEqual(19, storage.GetIndex(new DummyCoordinates(3, 4)));
        }

        [Test]
        public void GetIndex_Throws_On_OutOfBounds()
        {
            var storage = new DenseArrayStorage<DummyCoordinates>(2, 2);
            Assert.Throws<ArgumentOutOfRangeException>(() => storage.GetIndex(new DummyCoordinates(2, 0)));
            Assert.Throws<ArgumentOutOfRangeException>(() => storage.GetIndex(new DummyCoordinates(0, 2)));
            Assert.Throws<ArgumentOutOfRangeException>(() => storage.GetIndex(new DummyCoordinates(-1, 0)));
        }

        [Test]
        public void GetIndex_Throws_On_WrongDimensions()
        {
            var storage = new DenseArrayStorage<DummyCoordinates>(2, 2);
            Assert.Throws<ArgumentException>(() => storage.GetIndex(new DummyCoordinates(1, 1, 1)));
        }
    }
}

using NUnit.Framework;
using biomorphos.library.storage;
using biomorphos.tests.library.TestUtils;

namespace biomorphos.tests.library.storage
{
    [TestFixture]
    public class DictionaryStorageTests
    {
        [Test]
        public void Add_And_Get_Works_For_ValidCoordinates()
        {
            var storage = new DictionaryStorage<DummyCoordinates>();
            var coord = new DummyCoordinates(1, 2);
            var cell = new DummyCell();
            storage.Add(coord, cell);
            Assert.AreSame(cell, storage.Get(coord));
        }

        [Test]
        public void Get_ReturnsNull_If_NotPresent()
        {
            var storage = new DictionaryStorage<DummyCoordinates>();
            var coord = new DummyCoordinates(0, 1);
            Assert.IsNull(storage.Get(coord));
        }

        [Test]
        public void Remove_Sets_Cell_To_Null()
        {
            var storage = new DictionaryStorage<DummyCoordinates>();
            var coord = new DummyCoordinates(1, 1);
            var cell = new DummyCell();
            storage.Add(coord, cell);
            storage.Remove(coord);
            Assert.IsNull(storage.Get(coord));
        }
    }
}

using Moq;
using NUnit.Framework;

namespace CharacterCopierKata.Tests
{
    [TestFixture]
    public class CopierShould
    {
        #region Fields
        private readonly Mock<IDestination> _destination;
        private readonly Mock<ISource> _source;

        private Copier _sut; //system _unit test
        #endregion

        #region Construction
        public CopierShould() //override class
        {
            _destination = new Mock<IDestination>().SetupAllProperties();
            _source = new Mock<ISource>().SetupAllProperties();

            _sut = new Copier(_source.Object, _destination.Object);
        }
        #endregion

        #region Setup
        [SetUp]
        public void SetupTests()
        {
            _destination.Invocations.Clear();
            _source.Invocations.Clear();

            _sut = new Copier(_source.Object, _destination.Object);
        }
        #endregion

        #region Tests
        [Test]
        public void Source_GetChar_Is_Called_when_Copy_Is_Called()
        {
            _sut.Copy();
            _source.Verify(s => s.GetChar(), Times.Once);
        }

        [Test]
        public void Destination_SetChar_Is_Called_when_Copy_Is_Called()
        {
            _source.Setup(s => s.GetChar()).Returns(It.IsAny<char>());
            _sut.Copy();
            _destination.Verify(d => d.SetChar(It.IsAny<char>()), Times.Once);
        }

        [Test]
        public void Have_Same_Char_In_Source_And_Destination()
        {
            char n = 'n';

            _source.Setup(s => s.GetChar()).Returns(n);
            _sut.Copy();
            _destination.Verify(d => d.SetChar(n));
        }

        [Test]
        public void Destination_Not_Called_When_Source_Returns_NewLine()
        {
            char n = '\n';

            _source.Setup(s => s.GetChar()).Returns(n);
            _sut.Copy();
            _destination.Verify(d => d.SetChar(n), Times.Never); // should not return when new line
        }
        #endregion
    }
}
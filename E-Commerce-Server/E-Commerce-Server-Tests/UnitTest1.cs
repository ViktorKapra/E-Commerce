namespace ECom.Test
{
    public class UnitTest1
    {
        public int PlusFunction(int a, int b)
        { return a + b; }

        [Fact]
        public void Test_PlusFunction_Zero_Zero()
        {
            //Arrange
            int a = 0;
            int b = 0;
            // Act
            int result = PlusFunction(a, b);
            //Asert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Failing_Test_PlusFunction()
        {
            //Arrange
            int a = 0;
            int b = 0;
            // Act
            int result = PlusFunction(a, b);
            //Asert
            Assert.Equal(1, result);
        }
    }
}
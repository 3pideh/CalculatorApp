namespace CalculatorApp.Test
{
    public class MathelperTest
    {
        [Fact] //test without parameters
        public void IsEventTest()
        {
            var calculator = new MathFormulas();

            int x = 1;
            int y = 2;

            calculator.IsEven(x);
            calculator.IsEven(y);

            Assert.False(calculator.IsEven(x));
            Assert.True(calculator.IsEven(y));
        }

        [Theory]
        [InlineData(1,2,1)]
        [InlineData(1, 3, 2)]
        public void DiffTest(int x , int y , int expectedValue)
        {
            var calculator = new MathFormulas();

            var result = calculator.Diff(x, y);

            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 },6)]
        [InlineData(new int[] { -1, -2, -3 }, -6)]
        public void SumTest(int[] values , int expectedValue)
        {
            var calculator = new MathFormulas();

            var result = calculator.Sum(values);

            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(3,4,7)]
        [InlineData(8, 4, 12)]
        public void AddTest(int x , int y , int expectedValue)
        {
            var calculator = new MathFormulas();

            Assert.Equal(calculator.Add(x, y),expectedValue);
        }

        [Theory]
        [InlineData(new int[] { 3, 3, 3 },3)]
        [InlineData(new int[] { 2, 4, 6 }, 4)]
        public void AverageTest(int[] values, double expectedValue)
        {
            var calculator = new MathFormulas();

            Assert.Equal(calculator.Average(values), expectedValue);
        }

        //MathFormulas.Data => Data can be methodname/propname
        [Theory]
        [MemberData(nameof(MathFormulas.Data),MemberType = typeof(MathFormulas))]
        public void Add_MemberData_Test(int x, int y, int expectedValue)
        {
            var calculator = new MathFormulas();

            Assert.Equal(calculator.Add(x, y), expectedValue);
        }

        //MathFormulas.Data => Data can be classname
        [Theory(Skip = "..reason")]
        [ClassData(typeof(MathFormulas))]
        public void Add_ClassData_Test(int x, int y, int expectedValue)
        {
            var calculator = new MathFormulas();

            Assert.Equal(calculator.Add(x, y), expectedValue);
        }
    }
}
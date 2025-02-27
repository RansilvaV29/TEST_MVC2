namespace TEST_MVC2.Models
{
    public class calculadora
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }

        public int Add()
        {
            return FirstNumber + SecondNumber;
        }

    }
}

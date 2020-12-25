using System.Collections.Generic;

namespace Connect_Four
{
    class Program
    {
        private static List<string> myList = new List<string>()
           {
            "A_Red",
            "B_Yellow",
            "A_Red",
            "B_Yellow",
            "A_Red",
            "B_Yellow",
            "G_Red",
            "B_Yellow",
        };

        static void Main(string[] args)
        {
            var _ = ConnectFour.WhoIsWinner(myList);
        }
    }
}

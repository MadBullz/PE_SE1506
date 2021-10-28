using System;

namespace Q1_SE1506
{
    class Student
    {
        private string code { get; set; }
        private string name { get; set; }

        Student(string c)
        {
            this.code = c;
        }

        void InputInfo()
        {
            Console.WriteLine("Input name for student, please. Student name must be less than 15 characters");
            string input = Console.ReadLine();
            if (input.Length > 15) throw new IncorrectNameFormatException();
            else this.name = input;
        }

        static void Main(string[] args)
        {
            try
            {
                Student s = new Student("SE12067");
                s.InputInfo();
                Console.WriteLine(s.ToString());
            }
            catch (IncorrectNameFormatException e)
            {
                Console.WriteLine(e.Message); ;
            }
            Console.ReadLine();
        }

        public override string ToString()
        {
            return $"Student Information: Code: {code} - Name: {name}";
        }
    }
}

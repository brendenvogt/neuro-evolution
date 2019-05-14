using System;

namespace NeatConsole
{
    public class MyInput
    {
        public float[] Pixels;
    }
    public class MyOutput
    {
        public float[] Controls => new[] {L, R, U, D, A, B};
        private float L => 0;

        private float R => 0;

        private float U => 0;

        private float D => 0;

        private float A => 0;

        private float B => 0;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
        }
    }
}

using System;
using System.Collections.Generic;
using SharpNeat.Network;
using SharpNeat.Phenomes.NeuralNets;

namespace NeatConsole
{
    class Master
    {
        public CyclicNetwork _network;

        public static void Main(string[] args)
        {
            int s = int.Parse(args[1]);
            int os = int.Parse(args[2]);
            int d = int.Parse(args[3]);
            int od = int.Parse(args[4]);
            int ms = int.Parse(args[5]);

            var move = new Master().GetMove(s, os, d, od, ms);
            Console.WriteLine(move);
        }

        public Master()
        {
            var nodes = new List<Neuron>
            {
                new Neuron(0, NodeType.Bias, new LogisticFunctionSteep(), null), //added LogisticFunctionSteep and null for all neurons
                new Neuron(1, NodeType.Input, new LogisticFunctionSteep(), null),
                new Neuron(2, NodeType.Input, new LogisticFunctionSteep(), null),
                new Neuron(3, NodeType.Input, new LogisticFunctionSteep(), null),
                new Neuron(4, NodeType.Input, new LogisticFunctionSteep(), null),
                new Neuron(5, NodeType.Input, new LogisticFunctionSteep(), null),
                new Neuron(6, NodeType.Output, new LogisticFunctionSteep(), null),
                new Neuron(7, NodeType.Output, new LogisticFunctionSteep(), null),
                new Neuron(8, NodeType.Output, new LogisticFunctionSteep(), null),
                new Neuron(9, NodeType.Hidden, new LogisticFunctionSteep(), null)
            };
            var connections = new List<Connection>
            {
                new Connection(nodes[1], nodes[6], -1.3921811701131295),
                new Connection(nodes[6], nodes[6], 0.04683387519679514),
                new Connection(nodes[3], nodes[7], -4.746164930591382),
                new Connection(nodes[8], nodes[8], -0.025484025422054933),
                new Connection(nodes[4], nodes[9], -0.02084856381644095),
                new Connection(nodes[9], nodes[6], 4.9614062853759124),
                new Connection(nodes[9], nodes[9], -0.008672587457112968)
            };
            _network = new CyclicNetwork(nodes, connections, 5, 3, 2, false); //added false
        }

        public int GetMove(int snowballs, int opponentBalls, int ducks, int opponentDucks, int maxSnowballs)
        {
            _network.InputSignalArray[0] = snowballs;
            _network.InputSignalArray[1] = opponentBalls;
            _network.InputSignalArray[2] = ducks;
            _network.InputSignalArray[3] = opponentDucks;
            _network.InputSignalArray[4] = maxSnowballs;

            _network.Activate();

            double max = double.MinValue;
            int best = 0;
            for (var i = 0; i < _network.OutputCount; i++)
            {
                var current = _network.OutputSignalArray[i];

                if (current > max)
                {
                    max = current;
                    best = i;
                }
            }

            _network.ResetState();

            return best;
        }
    }
}



//    public class MyInput
//    {
//        public float[] Pixels;
//    }
//    public class MyOutput
//    {
//        public float[] Controls => new[] {L, R, U, D, A, B};
//        private float L => 0;
//        private float R => 0;
//        private float U => 0;
//        private float D => 0;
//        private float A => 0;
//        private float B => 0;
//    }
//    class Program
//    {
//    static void Main(string[] args)
//    {
//        Console.WriteLine("Hello World");
//    }
//}

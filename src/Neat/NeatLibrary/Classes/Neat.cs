using System;
using System.Collections.Generic;

namespace NeatLibrary.Classes
{
    public class Neat
    {
        //replication
        //    copy over best from every species
        //    25% chance of cloning using skewed exponential distribution
        //    75% chance of crossover using aligned innovation number method
        //    perform mutation for each gene
        //        80% chance of a weight mutation
        //            of that 80%, 90% chance of shifting a weight
        //            10% chance of new random weight
        //        80% chance of a structural mutation
        //            5% chance of adding a new gene
        //            3% chance of adding a new node
        
        //Species compatibility
        // formula c1*E/N + c2*D/N + c3*W
        //    take each species
        //        take mascot genome for species and compare its value with the particular genome in question.
        //        if abs(difference) is below a certain threshold then it should be added to the same genome
        //     if no compatible species is found then create new species
        
        //species culling / population control
        //    Population Control
        //        Take population, keep every species and the top fitness genome for each species. 
        //        Kill 50% of low fitness people with some exponential gradient.
        
        //    Stagnation
        //        if fitness for species doesnt advance 
        //    Extinction
        
        //STRUCTURE MUTATIONS
        
        //add node mutation
        //    take connection gene, disable it,
        //        and add a hidden node with incremented id
        //        add a connection from source to new node weighted 1.0 and incremented innovation number,
        //        add a connection from new node to target with weight of old connection weight and incremented innovation number
        
        //add connection mutation
        //    take connection gene, find another random node,
        //        add a connection from source to target with random weight and incremented innovation number
        
        //WEIGHT MUTATIONS
        
        //5% adjust add weight mutation (HyperParameter maxShift)
        //    take connection gene
        //        assign weight = weight + ((random.nextDouble()*maxShift)-(maxShift/2))
        
        // OR
        
        //5% exponential select adjust add weight mutation (HyperParameter maxShift)
        //    take connection gene
        //        assign weight = weight + ((ExpRandom()*maxShift)-(maxShift/2))

        //2% adjust multiply weight mutation (HyperParameter maxScalePercentage)
        //    take connection gene
        //        assign weight = weight * (random.nextDouble()*2*maxScalePercentage) + (1-maxScalePercentage)

        //2% new weight mutation
        //    take connection gene (HyperParameter maxNewWeight)
        //        assign weight = (random.nextDouble()*maxNewWeight)-(maxNewWeight/2)
        
        //1% change sign mutation
        //     take connection gene 
        //        assign weight = weight * -1

        public static double ExpRandom(int power = 2)
        {
            var rand = new Random(DateTime.UtcNow.GetHashCode());
            var val = 0.0;
            for (int i = 0; i < power; i++)
            {
                val *= rand.NextDouble();
            }
            return val;
        }
        
        public static T Pick<T>(List<T> items, Func<T, double> selector, double total) where T : class
        {
            
            //todo centralize random seed
            var random = new Random();
            
            if (items == null || items.Count <= 0) return null;

            if (total <= 0) return items[random.Next() % items.Count];
            
            var r = random.Next() % total;

            var index = 0;
            while (r > 0)
            {
                var item = items[index];
                var fitness = selector.Invoke(item);
                
                r = r - fitness;
                index++;
            }

            index--;
            return items[index];
        }
        
    }

    public class Genome
    {
        private Random _random = new Random();
        private Dictionary<int, NodeGene> Nodes { get; set; }
        private Dictionary<int, ConnectionGene> Genes { get; set; }

        public int Size()
        {
            return Genes.Count;
        }

        public void AddConnection(int sourceId, int targetId)
        {
            var newId = 0;
            var source = Nodes[sourceId];
            var target = Nodes[targetId];
            Genes.Add(newId, new ConnectionGene(source, target, newId));
        }
        
        private double Compatibility(Genome g2)
        {
            var c1 = 1.0;
            var c2 = 1.0;
            var c3 = 0.4;
            var n = Math.Max(Size(), g2.Size());
            var e = 0.0;
            var d = 0.0;
            var w = 0.0;
            var excess = c1*e/n;
            var disjoint = c2*d/n;
            var weightNot = w*c3;
            return excess+disjoint+weightNot;
        }        

        public void Crossover(Genome genome)
        {
            
        }
        
        public void Mutate()
        {
            if (_random.NextDouble() < 0.8)
            {
                MutateWeights();
            }
            MutateStructure();
        }

        #region WeightMutate

        private void MutateWeights()
        {
            if (_random.NextDouble() < 0.9)
            {
                MutateAdjustWeight();
            }
            else
            {
                MutateNewWeight();
            }
        }
        
        private void MutateAdjustWeight()
        {
            
        }
        
        
        private void MutateNewWeight()
        {
            
        }

        #endregion
        
        #region StructureMutate
        
        private void MutateStructure()
        {
            
        }

        private void MutateAddNode()
        {
            // take connection
            // disable connection
            // add node between the source and target
            // source to new node weight is 1.0
            // new node to target weight is whatever the old weight was
        }

        private void MutateAddConnection()
        {
            
        }

        private void MutateToggleConnection()
        {
            
        }

        private void DisableConnection()
        {
            
        }

        private void EnableConnection()
        {
            
        }

        #endregion

    }
    
    public class Gene
    {
        
    }

    public class ConnectionGene : Gene
    {
        public NodeGene Source { get; set; }
        public NodeGene Target { get; set; }
        public double Weight { get; set; }
        public bool Enabled { get; set; }
        public int InnovationNumber { get; set; }

        public ConnectionGene(NodeGene source, NodeGene target, int innovationNumber, double weight = 1.0, bool enabled = true)
        {
            Source = source;
            Target = target;
            InnovationNumber = innovationNumber;
            Weight = weight;
            Enabled = enabled;
        }
    }

    public enum NodeType
    {
        Sensor,
        Hidden,
        Output
    }
    
    public class NodeGene : Gene
    {
        public int Id { get; set; }
        public NodeType Type { get; set; }
        
    }

    public class Host
    {
        public Genome Genome;

        public Host(List<NodeGene> input, List<NodeGene> output)
        {
            
        }
    }
}
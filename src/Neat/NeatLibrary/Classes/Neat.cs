using System;
using System.Collections.Generic;

namespace NeatLibrary.Classes
{
    public class Neat
    {
        

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

        private Dictionary<int, Gene> Genes { get; set; }
        
        public void AddGene<T>(T gene) where T : Gene
        {
            
        }
        
        public void Crossover()
        {
            
        }
        
        public void Mutate()
        {
            
        }

        private void MutateAddNode()
        {
            
        }

        private void MutateAddConnection()
        {
            
        }

        private void MutateShiftWeight()
        {
            
        }

        private void MutateNewWeight()
        {
            
        }

        private void MutateToggleConnection()
        {
            
        }
        
    }
    
    public class Gene
    {
        
    }

    public class ConnectionGene : Gene
    {
        public NodeGene InNode { get; set; }
        public NodeGene OutNode { get; set; }
        public float Weight { get; set; }
        public bool Enabled { get; set; }
        public int InnovationNumber { get; set; }
        
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
    
    
}
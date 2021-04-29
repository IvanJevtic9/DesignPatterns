using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Composite
{
    /*Potrebne su nam dve konekcije - in and out*/
    public class Neuron : IEnumerable<Neuron>
    {
        public float Value;
        public List<Neuron> In, Out;

        public Neuron(float v)
        {
            Value = v;
            In = new List<Neuron>();
            Out = new List<Neuron>();
        }
        
        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return $"Neuron with value {Value}";
        }
    }

    /*Zahtev da iz ove grupe neurona povezmo sa nekim na neki nacin*/
    public class NeuronLayer : Collection<Neuron>
    {

    }

    public class NeuronRing : List<Neuron>
    {

    }

    /*Napravimo Neuron kao IEnumerable<Neuron> i onda mozemo da pravimo extansion metod da Connectujemo stvari (Collection, List su isto IEnumerable)*/

    public static class ExtensionMethod
    {
        // this IEnumerable self -> oznacava da Connect to moze da se pozove iz IEnumerable<Neuron> objekta
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other)) return;

            foreach(var from in self) 
                foreach(var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
        }
    }

    public class NeuronExp
    {
        public static void MainFunc(string[] args)
        {
            var neuron1 = new Neuron(3.2f);
            var neuron2 = new Neuron(1.1f);

            var n = new NeuronLayer();
            n.Add(new Neuron(2.5f));
            n.Add(new Neuron(5.2f));

            n.ConnectTo(neuron1);
            neuron1.ConnectTo(neuron2);

        }
    }
}

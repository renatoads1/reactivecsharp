using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace reactivecsharp
{
    class Exemplo2
    {
        private Random _random;

        public Exemplo2()
        {
            _random = new Random();
        }

        public void Start() {
            //converter
            var obs = Generate().ToObservable();
            var obs1 = obs
                .Where(x => x.Id % 2 == 0)
                .Select(x => x.Name);

            obs1.Subscribe(x => { // on next
                Console.WriteLine($"Observando o evento {x}");
            }, () =>
            {// on completed
                Console.WriteLine($"evento xompleto");
            });


        }

        public IEnumerable<Example02Event> Generate()
        {
            int numberOfEvents = _random.Next(10,21);
            for (int i = 0; i <= numberOfEvents; i++)
            {
                yield return new Example02Event(_random);
            }
        }


    }

    public class Example02Event
    {
        public int Id { get; private set; }
        public string Name { get; set; }

        public Example02Event(Random random)
        {
            Id = random.Next(0,1000);
            Name = $"Event{Id}";
        }
    }
}

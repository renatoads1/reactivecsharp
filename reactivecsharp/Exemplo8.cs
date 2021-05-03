using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace reactivecsharp
{
    class Exemplo8
    {
        public void Start() {
            var myInbox = FakeEmailGeneration().ToObservable();
            myInbox
                .Buffer(TimeSpan.FromSeconds(3))
                .Subscribe(x =>
                {
                    Console.WriteLine($"Recebi uma msg {x.Count}");
                    foreach (var email in x)
                    {
                        Console.WriteLine(" - {0} ",email);
                    }
                    Console.WriteLine();
                });

        }

        private IEnumerable<string> FakeEmailGeneration() {
            var random = new Random();
            var colours = new List<string> {"Email Importante"
            ,"Email da DevLearning"
            ,"nuo"};
            for (; ; ) {
                yield return colours[random.Next(colours.Count)];
                Thread.Sleep(random.Next(1000));
            }
        }

    }
}

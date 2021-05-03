using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace reactivecsharp
{
    class Exemplo7
    {
        public void Start() {
            var obs = GenerateEvents().ToObservable();

            obs.Throttle(TimeSpan.FromMilliseconds(750))
                .Subscribe(x =>
                {
                    Console.WriteLine($"OnNext: {x}");
                });
        }

        public IEnumerable<int> GenerateEvents() {
            int i = 0;
            while (true) {
                if (i > 1000)
                {
                    yield break;
                }

                yield return i;

                Thread.Sleep(i++ % 10 < 5 ? 500 : 1000);
                
            }
        }
    }
}

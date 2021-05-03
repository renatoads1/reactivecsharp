using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace reactivecsharp
{
    class Exemplo6
    {
        public void Start() {
            var obs = Observable.Interval(TimeSpan.FromMilliseconds(500));
            obs.Subscribe(x => {
                Console.WriteLine($"On Next -> {x}");
            });

        }
        
    }
}

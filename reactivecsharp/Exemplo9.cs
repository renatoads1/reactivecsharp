using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace reactivecsharp
{
    class Exemplo9
    {
        //executa as camadas ao mesmo tempo / conectadas
        public void Start() {
            var obs = Observable.Range(1, 4);

            var shared = obs.Publish();

            shared.Subscribe(x => {
                Console.WriteLine($"Rodando{x}");
            });

            Console.WriteLine();
            
            shared.Subscribe(x => {
                Console.WriteLine($"Rodando{x}");
            });

            shared.Connect();
        }

    }
}

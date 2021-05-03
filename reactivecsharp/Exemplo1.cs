using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace reactivecsharp
{
    class Exemplo1
    {
        public void Start() {

            var ob1 = Observable.Return<int>(1);

            ob1.Subscribe(x =>
            {
                Console.WriteLine("obs 1 => " + x);
            });
        
        }

    }
}

using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace reactivecsharp
{
    class Exemplo5
    {
        public void Start()
        {
            var obs = Observable.Create<Exemplo5Result>((x) =>
            {
                Console.WriteLine($"start thread {Thread.CurrentThread.ManagedThreadId}");
                x.OnNext(new Exemplo5Result { Name = "Obj1" });
                x.OnNext(new Exemplo5Result { Name = "Obj2" });
                x.OnNext(new Exemplo5Result { Name = "Obj3" });
                x.OnNext(new Exemplo5Result { Name = "Obj4" });

                Console.WriteLine($"Fim das trheads {Thread.CurrentThread.ManagedThreadId}");
                return Disposable.Empty;

            });

            Observer5 observer5 = new Observer5();
            obs.Subscribe(observer5);

        }

    }

    public class Observer5 : IObserver<Exemplo5Result>
    {
        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Exemplo5Result value)
        {
            Console.WriteLine($"Obs OnNext: {value.Name}");
        }
    }

    public class Exemplo5Result
    {
        public string Name { get; set; }
    }
}

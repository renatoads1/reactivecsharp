using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;

namespace reactivecsharp
{
    class Exemplo3
    {
        public void Start()
        {
            var obs = Observable.Create<Exemplo3Result>((x) =>
            {
                Console.WriteLine($"start thread {Thread.CurrentThread.ManagedThreadId}");
                x.OnNext(new Exemplo3Result { Name = "Obj1" });
                x.OnNext(new Exemplo3Result { Name = "Obj2" });
                x.OnNext(new Exemplo3Result { Name = "Obj3" });
                x.OnNext(new Exemplo3Result { Name = "Obj4" });

                Console.WriteLine($"Fim das trheads {Thread.CurrentThread.ManagedThreadId}");
                return Disposable.Empty;

            });

            obs.SubscribeOn(NewThreadScheduler.Default)
                .Subscribe(x =>
                {
                    Console.WriteLine($"Obs OnNext {x.Name.ToString()} -> on Thread {Thread.CurrentThread.ManagedThreadId}");
                });

        }



    }

    public class Exemplo3Result
    {
        public string Name { get; set; }
    }
}

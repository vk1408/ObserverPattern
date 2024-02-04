namespace ObserverPatternNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var alarm = new Alarm();
            alarm.Subscribe(new FireStation());
            alarm.Notify();
            alarm.Notify();
            alarm.Notify();
            alarm.Notify();
            alarm.Notify();

        }

    }

    public class Alarm : IObservable<int>, IDisposable
    {
        List<IObserver<int>> watchers = new();
        public IDisposable Subscribe(IObserver<int> observer) 
        { 
            watchers.Add(observer);
            return this;
        }
        int i = 0;
        public void Dispose() => throw new NotImplementedException();
        
        public void Notify()
        {
            if (i>3)
            {
                watchers.ForEach(x=>x.OnCompleted());
                return;
            }
            watchers.ForEach(x => x.OnNext(i++));
        }
    }
    public class FireStation : IObserver<int>
    {
        public void Alert(Alarm value)
        {
            Console.WriteLine($"{nameof(FireStation)}: RESPONDING!");
        }

        // Provider has finished sending data
        public void OnCompleted()
        {
            Console.WriteLine($"{nameof(FireStation)}: complete");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"{nameof(FireStation)}: error");
        }

        // value in Provider changed 
        public void OnNext(int value)
        {
            Console.WriteLine($"{nameof(FireStation)}: next: {value}");
        }
    }
}

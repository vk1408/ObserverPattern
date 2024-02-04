namespace ObserverPatternRaw
{
    public class Program
    {
        static void Main(string[] args)
        {
            var alarm = new Alarm("person injured");
            alarm.AddWatcher(new FireStation());
            alarm.AddWatcher(new PoliceStation());
            alarm.AddWatcher(new HospitalStation());
            alarm.Notify();
            alarm.Message = "house burning";
            alarm.Notify();

        }
    }
    public class Alarm
    {
        public string Message { get; set; }
        List<IWatcher<Alarm>> watchers = new();
        public Alarm(string message)
        {
            Message = message;
        }
        public void AddWatcher(IWatcher<Alarm> alarmWatcher)
        {
            watchers.Add(alarmWatcher);
        }
        public void Notify()
        {
            foreach (var w in watchers) 
            {
                w.Alert(this);
            }
        }
    }
    public interface IWatcher<T>
    {
        void Alert(T value);
    }
    public class FireStation : IWatcher<Alarm>
    {
        public void Alert(Alarm value)
        {
            Console.WriteLine($"{nameof(FireStation)} responding to {value.Message}");
        }
    }
    public class HospitalStation : IWatcher<Alarm>
    {
        public void Alert(Alarm value)
        {
            Console.WriteLine($"{nameof(HospitalStation)} responding to {value.Message}");
        }
    }

    public class PoliceStation : IWatcher<Alarm>
    {
        public void Alert(Alarm value)
        {
            Console.WriteLine($"{nameof(PoliceStation)} responding to {value.Message}");
        }
    }

 
}

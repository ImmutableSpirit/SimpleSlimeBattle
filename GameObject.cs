namespace Demo1
{
    public abstract class GameObject
    {
        public virtual int hitPoints { get; set; }
        public abstract void Do(string actionText, GameObject gameObject = null);
        public abstract void Process(Attack attack);

        public enum statusType { Normal, Dead };
        public statusType status { get; set; }
    }
}
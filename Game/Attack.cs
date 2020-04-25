namespace Demo1
{
    public class Attack
    {
        public string displayName { get; set; }
        public int damage { get; set; }
        public enum attackTypes { Normal, Fire };
        public attackTypes attackType { get; set; }
    }
}
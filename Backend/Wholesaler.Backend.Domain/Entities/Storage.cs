namespace Wholesaler.Backend.Domain.Entities
{
    public class Storage
    {
        public Guid Id { get; }
        public string Name { get; }
        public int State { get; private set; }
        public ICollection<Requirement>? Requirements { get; }

        public Storage(string name)
        {
            Id = Guid.NewGuid();
            State = 0;
            Name = name;
            Requirements = new List<Requirement>();
        }

        public Storage(int state, string name)
        {
            Id = Guid.NewGuid();
            State = state;
            Name = name;
            Requirements = new List<Requirement>();
        }

        public Storage(int state, string name, ICollection<Requirement> requirements)
        {
            Id = Guid.NewGuid();
            State = state;
            Name = name;
            Requirements = requirements;
        }
        public Storage(Guid id, int state, string name, ICollection<Requirement> requirements)
        {
            Id = id;
            State = state;
            Name = name;
            Requirements = requirements;
        }

        public void SetState(int state) 
        {
            State = state;
        }
    }
}

namespace PetFamily.Domain.Volunteers
{
    public record SocialNetwork
    {
        public SocialNetwork(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public string Name { get; }
        public string Description { get; }

    }
}

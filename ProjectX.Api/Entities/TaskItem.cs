namespace ProjectX.Api.Entities
{
    public record TaskItem
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Task { get; init; }

        public DateTimeOffset TaskDateTime { get; init; }
    }
}
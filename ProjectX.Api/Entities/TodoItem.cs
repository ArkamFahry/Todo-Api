namespace ProjectX.Api.Entities
{
    public record TodoItem
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Todo { get; init; }

        public DateTimeOffset TodoDateTime { get; init; }
    }
}
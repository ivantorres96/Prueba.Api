namespace Prueba.Api.DTOs.Task
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Priority { get; set; }

        public string? File { get; set; }

        public int StateId { get; set; }
    }
}

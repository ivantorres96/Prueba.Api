namespace Prueba.Api.Models
{
    public class StateModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<TaskModel>? Tasks { get; set; }
    }
}

namespace AttendanceApi.Models;

public class Scheme
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public ICollection<Program> Programs { get; set; } = new List<Program>();
}
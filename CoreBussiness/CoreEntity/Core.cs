namespace CoreBussiness.CoreEntity;

public class Core
{
    public Core()
    {
        CreationTime = DateTime.Now;
    }
    public int Id { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ?ModificationTime { get; set; }
    public bool IsDeleted { get; set; }
}
namespace CRUD_MongoDB;

public class MetroStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string LinesCollectionName { get; set; } = null!;
    public string StationsCollectionName { get; set; } = null!;
    public string DevicesCollectionName { get; set; } = null!;
}
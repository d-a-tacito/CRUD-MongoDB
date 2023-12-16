namespace CRUD_MongoDB;

public class MetroStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = "mongodb://10.42.0.15:27017";
    public string DatabaseName { get; set; } = "metro";
    public string LinesCollectionName { get; set; } = "lines";
    public string StationsCollectionName { get; set; } = "stations";
    public string DevicesCollectionName { get; set; } = "devices";
    
}
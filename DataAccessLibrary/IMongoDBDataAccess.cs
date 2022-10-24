namespace DataAccessLibrary
{
    /// <summary>
    /// Provides a MongoDB data access layer
    /// </summary>
    public interface IMongoDBDataAccess
    {
        T? DeleteRecordById<T>(string table, Guid id);
        void InsertRecord<T>(string table, T record);
        T? LoadRecordById<T>(string table, Guid id);
        List<T>? LoadRecords<T>(string table);
        T? UpsertRecord<T>(string table, T record, Guid id);
    }
}
namespace SKGPortalCore.Model.SourceData
{
    public interface IImportSource
    {
        int Id { get; set; }
        string ImportBatchNo { get; set; }
        string Source { get; set; }
        string Src { get; }
    }
}

using DotnetHomework.Models;
using DotnetHomework.Utility;

namespace DotnetHomework.Data
{
    public interface IDocumentsRepository
    {
        Task<Document> GetDocument(string id);
        Task Add(Document document, string storageType = "hdd");
        Task<Result> GetDocument(string id, string storageType = "hdd", string fileType = "xml");
    }
}

using DotnetHomework.Models;
using DotnetHomework.Utility;

namespace DotnetHomework.Data
{
    public interface IDocumentsRepository
    {

        Task Add(Document document, string storageType);
        Task Add(Document document);
        Task<Result> GetDocument(string id, string storageType, string fileType);
        Task<Document> GetDocument(string id);

    }
}

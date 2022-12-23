using DotnetHomework.Models;

namespace DotnetHomework.Utility
{
    public interface IStorage
    {
        Task SaveData(Document document);
        Task<string> GetData(string id);
    }
}

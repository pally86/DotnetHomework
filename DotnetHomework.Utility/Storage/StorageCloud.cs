using DotnetHomework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetHomework.Utility
{
    public class StorageCloud : IStorage
    {
        public Task<string> GetData(string id)
        {
            throw new NotImplementedException();
        }

        public Task SaveData(Document document)
        {
            throw new NotImplementedException();
        }
    }
}

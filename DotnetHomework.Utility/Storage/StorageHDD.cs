using DotnetHomework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetHomework.Utility
{
    public class StorageHDD : IStorage
    {
        public async Task<string> GetData(string id)
        {
            string data;
            var path = Path.Combine(SD.Folder, GetFileName(id));

            using (StreamReader reader = new StreamReader(path))
                data = await reader.ReadToEndAsync();

            return data;
        }

        private string GetFileName(string id)
        {
            Dictionary<string, string> ids = new Dictionary<string, string>();
            foreach (string line in File.ReadLines(SD.Dictionary))
            {
                ids.Add(line.Split(':')[0], line.Split(':')[1]);
            }
            return ids[id];
        }
        public async Task SaveData(Document document)
        {
            InitialPath();
            if (!IsUniqueId(document.Id))
                return;
            
            Guid fileName = Guid.NewGuid();
            using (StreamWriter sw = File.AppendText(SD.Dictionary))
            {
                sw.WriteLine($"{document.Id}:{fileName}");
            }

            var path = Path.Combine(SD.Folder, fileName.ToString());

            using FileStream createStream = File.Create(path);
            await System.Text.Json.JsonSerializer.SerializeAsync(createStream, document);
        }

        private void InitialPath()
        {
            
            if (!Directory.Exists(SD.Folder))
            {
                Directory.CreateDirectory(SD.Folder);
            }
            var dictionary = Path.Combine(SD.Folder, "dictionary");
            if (!File.Exists(dictionary))
            {
                File.Create(dictionary);
            }
        }

        private bool IsUniqueId(string id)
        {
            IList<string> ids = new List<string>();
            foreach (string line in File.ReadLines(SD.Dictionary))
            {
                ids.Add(line.Split(':')[0]);
            }
            return !ids.Contains(id);
        }
    }
}

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
        private static readonly string Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UploadFiles");
        private static readonly string Dictionary = Path.Combine(Folder, "dictionary");

        public async Task<string> GetData(string id)
        {
            string data;
            var path = Path.Combine(Folder, GetFileName(id));

            using (StreamReader reader = new StreamReader(path))
                data = await reader.ReadToEndAsync();

            return data;
        }

        private string GetFileName(string id)
        {
            Dictionary<string, string> ids = new Dictionary<string, string>();
            foreach (string line in File.ReadLines(Dictionary))
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
            using (StreamWriter sw = File.AppendText(Dictionary))
            {
                sw.WriteLine($"{document.Id}:{fileName}");
            }

            var path = Path.Combine(Folder, fileName.ToString());

            using FileStream createStream = File.Create(path);
            await System.Text.Json.JsonSerializer.SerializeAsync(createStream, document);
        }

        private void InitialPath()
        {
            
            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }
            var dictionary = Path.Combine(Folder, "dictionary");
            if (!File.Exists(dictionary))
            {
                File.Create(dictionary);
            }
        }

        private bool IsUniqueId(string id)
        {
            IList<string> ids = new List<string>();
            foreach (string line in File.ReadLines(Dictionary))
            {
                ids.Add(line.Split(':')[0]);
            }
            return !ids.Contains(id);
        }
    }
}

namespace DotnetHomework.Utility
{
    public static class SD
    {
        public enum FileType
        {
            XML,
            JSON,
            MessagePack
        }

        public enum StorageType
        {
            Cloud,
            HDD,
            InMemory
        }

        public enum StatusCode
        {
            Success = 200,
            Created = 201,
            InternalServerError = 500
        }

        public static bool IsFileLocked(FileInfo file, string path, string dictionary)
        {
            while (!Directory.Exists(path)) { }
            while (!File.Exists(dictionary)) { }
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }



        public static void DeleteFilesAfterTest(string path)
        {
            foreach (var item in Directory.GetFiles(path))
            {
                File.Delete(item);
            };
            Directory.Delete(path);
        }

    }


}

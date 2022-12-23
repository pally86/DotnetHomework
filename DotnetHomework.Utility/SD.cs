namespace DotnetHomework.Utility
{
    public static class SD
    {
        public static string Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"UploadFiles");
        public static string Dictionary = Path.Combine(Folder, "dictionary");
        
        public enum FileType
        {
            XML,
            JSON
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
    }

    
}

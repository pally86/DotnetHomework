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
    }

    
}

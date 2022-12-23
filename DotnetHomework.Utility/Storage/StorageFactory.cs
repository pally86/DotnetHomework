

namespace DotnetHomework.Utility
{
    

    public class StorageFactory : IStorageFactory
    {
        private readonly IEnumerable<IStorage> _storageServices;

        public StorageFactory(IEnumerable<IStorage> storageServices)
        {
            _storageServices = storageServices;
        }

        public IStorage GetInstance(string token)
        {
            return token switch
            {
                "hdd" => this.GetService(typeof(StorageHDD)),
                "cloud" => this.GetService(typeof(StorageCloud)),
                "inmemory" => this.GetService(typeof(StorageFactory)),
                _ => throw new InvalidOperationException()
            }; ;
        }

        IStorage GetService(Type type)
        {
            return _storageServices.FirstOrDefault(x => x.GetType() == type)!;
        }

        
    }

}

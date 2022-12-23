using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetHomework.Utility
{
    public interface IStorageFactory
    {
        IStorage GetInstance(string token);
    }
}

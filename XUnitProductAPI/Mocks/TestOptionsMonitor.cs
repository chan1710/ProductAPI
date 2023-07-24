using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Product_API.Models;

namespace XUnitProductAPI.Mocks
{
    public class TestOptionsMonitor<T> : IOptionsMonitor<T>
    where T : class, new()
    {
        public TestOptionsMonitor(T currentValue)
        {
            CurrentValue = currentValue;
        }

        public T Get(string name)
        {
            return CurrentValue;
        }

        public IDisposable OnChange(Action<T, string> listener)
        {
            throw new NotImplementedException();
        }

        public T CurrentValue { get; }
    }
}

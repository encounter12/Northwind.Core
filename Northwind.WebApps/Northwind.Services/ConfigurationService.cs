using Northwind.Services.Contracts;

namespace Northwind.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string NorthwindApiUrl { get; set; }
    }
}

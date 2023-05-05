using CodeTest.Controllers.ViewModels;
using CodeTest.Models;

namespace CodeTest.Services
{
    public interface IPackageService
    {
        Package GetById(string id);
        Package Add(CreatePackageViewModel packageVM);
        IEnumerable<Package> GetRange();
    }
}

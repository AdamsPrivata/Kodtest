using CodeTest.Models;

namespace CodeTest.DAL
{
    public interface IPackageRepository
    {
        Package GetById(string id);
        IEnumerable<Package> GetRange();
        Package Add(Package package);
    }
}

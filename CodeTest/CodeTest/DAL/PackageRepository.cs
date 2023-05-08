using CodeTest.Models;

namespace CodeTest.DAL
{
    public class PackageRepository : IPackageRepository
    {
        // Fake storage
        private List<Package> Packages { get; set; } = new List<Package>();

        private const string START_ID = "999000000000000001";

        public Package Add(Package package)
        {
            return AddPackage(package);
        }

        public Package GetById(string id)
        {
            return Packages.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Package> GetRange()
        {
            return Packages.ToList();
        }

        // Fake incrementing id from db
        private Package AddPackage(Package package)
        {
            if (Packages.Any())
            {
                var latestId = Packages.Select(p => p.Id).Max();
                long.TryParse(latestId, out var latestIdLong);
                package.Id = (++latestIdLong).ToString();
            }
            else
            {
                package.Id = START_ID;
            }

            Packages.Add(package);
            return package;
        }
    }
}

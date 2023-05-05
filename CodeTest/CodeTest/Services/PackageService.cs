using CodeTest.Controllers.ViewModels;
using CodeTest.DAL;
using CodeTest.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeTest.Services
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _repository;

        public PackageService(IPackageRepository repository)
        {
            _repository = repository;
        }

        public Package Add(CreatePackageViewModel packageVM)
        {
            var package = new Package(packageVM.Weight, packageVM.Height, packageVM.Width, packageVM.Length);

            return _repository.Add(package);
        }

        public Package GetById(string id)
        {
            // TODO : Validate id
            // 18 digits
            // numbers only
            // Begins with 999
            // ValidationException

            var package = _repository.GetById(id);

            if (package == null)
            {
                // Custom exception? NotFoundException
                //throw new ValidationException("Package not found!");
            }

            return package;
        }

        public IEnumerable<Package> GetRange()
        {
            return _repository.GetRange();
        }
    }
}

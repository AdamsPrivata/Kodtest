﻿using CodeTest.Controllers.ViewModels;
using CodeTest.DAL;
using CodeTest.Models;
using CodeTest.Utilities;
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

            if (!package.IsValid)
            {
                throw new ValidationException("The provided package was not within allowed dimensions and/or weight!");
            }

            return _repository.Add(package);
        }

        public Package GetById(string id)
        {
            ValidateId(id);

            var package = _repository.GetById(id);

            if (package == null)
            {
                throw new PackageNotFoundException("Package with provided id could not be found!");
            }

            return package;
        }

        public IEnumerable<Package> GetRange()
        {
            return _repository.GetRange();
        }

        private void ValidateId(string id)
        {
            if (id.Length != 18)
            {
                throw new ValidationException("Package id must contain exactly 18 characters!");
            }
            else if (id.Any(c => !char.IsDigit(c)))
            {
                throw new ValidationException("Package id must contain numerical values only!");
            }
            else if (!id.Substring(0, 3).Equals("999"))
            {
                throw new ValidationException("Package id must begin with 999!");
            }
        }
    }
}

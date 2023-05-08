using CodeTest.Controllers.ViewModels;
using CodeTest.DAL;
using CodeTest.Models;
using CodeTest.Services;
using CodeTest.Utilities;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace Tests
{
    public class PackageServiceTests
    {
        private readonly Mock<IPackageRepository> _packageRepository;

        private readonly IPackageService _packageService;


        public PackageServiceTests()
        {
            _packageRepository = new Mock<IPackageRepository>();
            _packageService = new PackageService(_packageRepository.Object );
        }

        [Fact]
        public void Add_Should_Throw_ValidationException_If_Package_Invalid()
        {
            var vm = new CreatePackageViewModel
            {
                Height = 80,
                Width = 10,
                Length = 10,
                Weight = 10
            };

            var package = new Package(vm.Weight, vm.Height, vm.Width, vm.Length);
            _packageRepository.Setup(o => o.Add(package)).Returns(package);

            var ex = Assert.Throws<ValidationException>(() => _packageService.Add(vm));

            Assert.Equal("The provided package was not within allowed dimensions and/or weight!", ex.Message);
        }

        [Fact]
        public void Add_Should_Return_AddedPackage()
        {
            var vm = new CreatePackageViewModel
            {
                Height = 20,
                Width = 30,
                Length = 25,
                Weight = 10000
            };

            var package = new Package(vm.Weight, vm.Height, vm.Width, vm.Length);
            _packageRepository.Setup(o => o.Add(It.IsAny<Package>())).Returns(package);

            var res = _packageService.Add(vm);

            Assert.Equal(res.Height, package.Height);
            Assert.Equal(res.Width, package.Width);
            Assert.Equal(res.Length, package.Length);
            Assert.Equal(res.Weight, res.Weight);
        }

        [Fact]
        public void GetById_Should_Throw_ValidationException_For_TooLong_Id()
        {
            var packageId = "9990000000000000001";

            var package = new Package(1, 1, 1, 1);
            _packageRepository.Setup(o => o.GetById(packageId)).Returns(package);

            var ex = Assert.Throws<ValidationException>(() => _packageService.GetById(packageId));

            Assert.Equal("Package id must contain exactly 18 characters!", ex.Message);
        }

        [Fact]
        public void GetById_Should_Throw_ValidationException_For_TooShort_Id()
        {
            var packageId = "999000000001";

            var package = new Package(1, 1, 1, 1);
            _packageRepository.Setup(o => o.GetById(packageId)).Returns(package);

            var ex = Assert.Throws<ValidationException>(() => _packageService.GetById(packageId));

            Assert.Equal("Package id must contain exactly 18 characters!", ex.Message);
        }

        [Fact]
        public void GetById_Should_Throw_ValidationException_For_Id_With_Letters()
        {
            var packageId = "99900000000000000b";

            var package = new Package(1, 1, 1, 1);
            _packageRepository.Setup(o => o.GetById(packageId)).Returns(package);

            var ex = Assert.Throws<ValidationException>(() => _packageService.GetById(packageId));

            Assert.Equal("Package id must contain numerical values only!", ex.Message);
        }

        [Fact]
        public void GetById_Should_Throw_ValidationException_For_Id_With_InvalidBeginning()
        {
            var packageId = "199000000000000001";

            var package = new Package(1, 1, 1, 1);
            _packageRepository.Setup(o => o.GetById(packageId)).Returns(package);

            var ex = Assert.Throws<ValidationException>(() => _packageService.GetById(packageId));

            Assert.Equal("Package id must begin with 999!", ex.Message);
        }

        [Fact]
        public void GetById_Should_Throw_PackageNotFoundException_If_Package_NotFound()
        {
            var packageId = "999000000000000001";

            var ex = Assert.Throws<PackageNotFoundException>(() => _packageService.GetById(packageId));

            Assert.Equal("Package with provided id could not be found!", ex.Message);
        }

        [Fact]
        public void GetById_Should_Return_Package()
        {
            var packageId = "999000000000000001";

            var package = new Package(10000, 10, 25, 30);
            _packageRepository.Setup(o => o.GetById(packageId)).Returns(package);

            var res = _packageService.GetById(packageId);

            _packageRepository.Verify(o => o.GetById(packageId), Times.Once);
            Assert.Equal(res.Height, package.Height);
            Assert.Equal(res.Width, package.Width);
            Assert.Equal(res.Length, package.Length);
            Assert.Equal(res.Weight, res.Weight);
        }
    }
}
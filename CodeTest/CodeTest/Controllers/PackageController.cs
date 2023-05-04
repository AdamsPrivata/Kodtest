using CodeTest.Controllers.ViewModels;
using CodeTest.Models;
using CodeTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeTest.Controllers
{
    [Route("/package")]
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet("{id}")]
        public ActionResult<Package> Get(int id)
        {
            var package = new Package
            {
                Id = 1,
                Height = 1,
                Length = 1,
                Weight = 1,
                Width = 1
            };

            return package;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Package>> GetRange()
        {
            return new List<Package>() { new Package { Id = 1}, new Package { Id = 2 } };
        }

        [HttpPost]
        public ActionResult<Package> Create([FromBody] CreatePackageViewModel vm)
        {
            //Maybe map
            //_packageService.Add

            return new Package { Id = 5 };
        }
    }

   
}

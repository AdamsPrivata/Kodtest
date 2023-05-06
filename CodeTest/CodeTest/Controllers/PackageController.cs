using CodeTest.Controllers.ViewModels;
using CodeTest.Models;
using CodeTest.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public IActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            try
            {
                var package = _packageService.GetById(id);
                return Ok(package);
            }
            catch (ValidationException ex)
            {
                return ValidationProblem(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetRange()
        {
            var packages = _packageService.GetRange();
            return Ok(packages);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePackageViewModel vm)
        {
            if (vm == null)
            {
                return BadRequest();
            }

            //Maybe map
            var createdPackage = _packageService.Add(vm);
            return Ok(createdPackage);
        }
    }

   
}

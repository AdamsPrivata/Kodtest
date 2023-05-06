using CodeTest.Controllers.ViewModels;
using CodeTest.Models;
using CodeTest.Services;
using CodeTest.Utilities;
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
            catch (PackageNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return ValidationProblem(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public IActionResult GetRange()
        {
            try
            {
                var packages = _packageService.GetRange();
                return Ok(packages);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePackageViewModel vm)
        {
            if (vm == null)
            {
                return BadRequest();
            }

            try
            {
                var createdPackage = _packageService.Add(vm);
                return Ok(createdPackage);
            }
            catch (ValidationException ex)
            {
                return ValidationProblem(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }

   
}

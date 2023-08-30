using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using BIS_project.Dtos;

namespace BIS_project.Models
{
	public class EmployeeModel
	{
        [FromForm(Name = "employee")]
        public EmployeeDto Employee { get; set; } 

        [FromForm(Name = "file")]
        public IFormFile File { get; set; }
    }
}


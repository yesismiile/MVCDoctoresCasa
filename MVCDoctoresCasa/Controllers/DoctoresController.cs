using ApiDoctoresPruebaCasa.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCDoctoresCasa.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDoctoresCasa.Controllers
{
    public class DoctoresController : Controller
    {
        private ServiceDoctores service;
        private ServiceS3 bucketService;

        public DoctoresController(ServiceDoctores service,ServiceS3 bucketService)
        {
            this.service = service;
            this.bucketService = bucketService;
        }

        public async Task<IActionResult> IndexDoctores()
        {
            List<Doctor> doctores = await this.service.GetDoctoresAsync();
            return View(doctores);
        }

        public async Task<IActionResult> GetDoctor(string id)
        {
            Doctor doc = await this.service.FindDoctor(id);
            return View(doc);
        }

        public IActionResult CreateDoctor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(string IdDoctor, string IdHospital, string Apellido, string Especialidad, string Salario, IFormFile Imagen)
        {
            string extension = Imagen.FileName.Split(".")[1];
            string fileName = IdDoctor.Trim() + "." + extension;
            using (Stream stream = Imagen.OpenReadStream())
            {
                await this.bucketService.UploadFileAsync(stream, fileName);
            }

            await this.service.InsertDoctorAsync(IdDoctor, IdHospital, Apellido, Especialidad, int.Parse(Salario), fileName);
            return RedirectToAction("IndexDoctores");
        }

        public async Task<IActionResult> DeleteDoctor(string id)
        {
            Doctor doc = await this.service.FindDoctor(id);
            await this.bucketService.DeleteFileAsync(doc.Imagen);
            await this.service.DeleteDoctor(id);
            return RedirectToAction("IndexDoctores");
        }
    }
}

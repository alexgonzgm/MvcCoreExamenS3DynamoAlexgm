using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCoreExamenS3DynamoAlexgm.Helpers;
using MvcCoreExamenS3DynamoAlexgm.Models;
using MvcCoreExamenS3DynamoAlexgm.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreExamenS3DynamoAlexgm.Controllers
{
    public class UsuariosController : Controller
    {
        public ServiceDynamoDb serviceDynamoDb;
        UploadHelper uploadHelper;
        public ServiceAWSS3 serviceAWSS3;
        public UsuariosController(ServiceDynamoDb serviceDynamoDb, UploadHelper uploadHelper, ServiceAWSS3 ServiceAWSS3)
        {
            this.serviceDynamoDb = serviceDynamoDb;
            this.uploadHelper = uploadHelper;
            this.serviceAWSS3 = ServiceAWSS3;
        }



        public async Task<IActionResult> Index()
        {
            return View(await this.serviceDynamoDb.GetUsuarios());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await this.serviceDynamoDb.FindUsuario(id));
        }

        public async Task<IActionResult> Edit(int id)
        {

            return View(await this.serviceDynamoDb.FindUsuario(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Usuario usuario, string includefoto, IFormFile file, string titulo)
        {

            string path = await this.uploadHelper.UploadFileAsync(file, Folders.Images);
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                bool respuesta = await this.serviceAWSS3.UploadFile(stream, file.FileName);
            }
            if (includefoto != null)
            {

                usuario.Fotos = new Fotos();
                usuario.Fotos.Imagen = "https://bucket-full-exam.s3.amazonaws.com/" + file.FileName.ToString();
                usuario.Fotos.Titulo = titulo;


            }
            await this.serviceDynamoDb.UpdateUsuario(usuario);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            await this.serviceDynamoDb.DeleteUsuario(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario, string includefoto, IFormFile file, string titulo)
        {
            string path = await this.uploadHelper.UploadFileAsync(file, Folders.Images);
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                bool respuesta = await this.serviceAWSS3.UploadFile(stream, file.FileName);
            }
            if (includefoto != null)
            {

                usuario.Fotos = new Fotos();
                usuario.Fotos.Imagen = "https://bucket-full-exam.s3.amazonaws.com/" + file.FileName.ToString();
                usuario.Fotos.Titulo = titulo;


            }
            await this.serviceDynamoDb.CreateUsuario(usuario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ListFilesAWS()
        {
            List<String> files = await this.serviceAWSS3.GetS3Files();
            return View(files);
        }


    }
}

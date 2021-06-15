using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sunat.WebApi.Models;
using Tesseract;


namespace Sunat.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContribuyentesController : ControllerBase
    {
        private IWebHostEnvironment _environment;
        private IServiceClient serviceClient;
        private static string captcha = string.Empty;
        private static string tipoDoc = "1";

        public ContribuyentesController(IWebHostEnvironment environment,
            IServiceClient serviceClient)
        {
            _environment = environment;
            this.serviceClient = serviceClient;
        }
        // GET: api/<PersonasController>
        [HttpGet("{ruc}", Name = "Contribuyentes")]
        public async Task<ServiceResult> Get(string ruc)
        {
            var service = new ServiceResult();

            //if (ruc.Length == 8)
            //{
            //    tipoDoc = ""
            //}

            var modelo = new Contribuyente();
            var captchaPams = new Dictionary<string, object>()
                    {
                        {"accion","image" }
                    };

            string paramCap = WebHelper.ConcatParams(captchaPams);
            var responseCap = await serviceClient.GetAsync("cl-ti-itmrconsruc/captcha", paramCap);
            if (responseCap.IsSuccessStatusCode)
            {
                var randomResult = await responseCap.Content.ReadAsStreamAsync();

                using (var engine = new TesseractEngine(Path.Combine(_environment.WebRootPath, "tessdata"), "eng", EngineMode.Default))
                {
                    using (var image = new Bitmap(randomResult))
                    {

                        using (var page = engine.Process(image))
                        {

                            captcha = page.GetText();
                        }

                    }
                }

            }

            var contents = new Dictionary<string, object>()
                    {
                        {"accion","consPorRuc" },
                        {"nroRuc",ruc },
                        {"codigo", captcha },
                        {"tipdoc",tipoDoc }
                    };


            string param = WebHelper.ConcatParams(contents);
            var response = await serviceClient.GetAsync("cl-ti-itmrconsruc/jcrS00Alias", param);

            if (response.IsSuccessStatusCode)
            {
                var htmlBody = await response.Content.ReadAsStringAsync();
                

                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                document.LoadHtml(htmlBody);
                var nodeTables = document.DocumentNode
                                        .SelectNodes("//table")
                                        .FirstOrDefault();

                if (nodeTables != null)

                {
                    var listNodeTr = nodeTables.Elements("tr").ToArray();
                    if (listNodeTr != null)
                    {
                        var tipoContri = listNodeTr[1].Elements("td").ToArray();
                        var nodeRazonSocial = listNodeTr[0].Elements("td").ToArray();
                        var nodeNombrCom = listNodeTr[2].Elements("td").ToArray();
                        var fechaInscrip = listNodeTr[3].Elements("td").ToArray();
                        var estado = listNodeTr[5].Elements("td").ToArray();
                        var condicion = listNodeTr[5].Elements("td").ToArray();
                        if (nodeRazonSocial != null)
                        {
                            string cliente = CleanEmptySpace(nodeRazonSocial[1].InnerHtml.Trim());
                            modelo.RUC = cliente.Substring(0, 11).Trim();
                            modelo.RazonSocial = cliente.Substring(13, cliente.Length - 13).Trim();
                            modelo.NombreComercial = nodeNombrCom[1].InnerHtml.Trim();
                            modelo.Condicion = condicion[1].InnerHtml.Trim();
                            modelo.Tipo = tipoContri[1].InnerHtml.Trim();
                            modelo.Inscripcion = fechaInscrip[1].InnerHtml.Trim();
                            modelo.Estado = estado[1].InnerHtml.Trim();
                        }
                        var nodeDireccion = listNodeTr[6].Elements("td").ToArray();
                        if (ruc.StartsWith("10"))
                        {
                            nodeDireccion = listNodeTr[7].Elements("td").ToArray();
                        }
                        if (nodeDireccion != null)
                        {
                            string direccion = CleanEmptySpace(nodeDireccion[1].InnerHtml.Trim());
                            modelo.Direccion = direccion.Trim();
                        }
                    }
                }

            }

            service.Data = modelo;

            return service;
        }

        private string CleanEmptySpace(string cadena)
        {
            while (cadena.Contains("  "))
            {
                cadena = cadena.Replace("  ", " ");
            }
            return cadena;
        }


    }
}

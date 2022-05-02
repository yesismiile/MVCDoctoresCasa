using ApiDoctoresPruebaCasa.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVCDoctoresCasa.Service
{
    public class ServiceDoctores
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceDoctores(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("URLApis:ApiDoctores");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task DeleteDoctor(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Doctores/DeleteDoctor/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }

        public async Task<Doctor> FindDoctor(string id)
        {
            string request = "/api/Doctores/" + id;
            Doctor doctor =
              await this.CallApiAsync<Doctor>(request);
            return doctor;
        }

        public async Task InsertDoctorAsync
        (string IdDoctor, string IdHospital, string Apellido, string Especialidad, int Salario, string Imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                Doctor doc = new Doctor()
                {
                    Cod_Hospital = IdHospital,
                    NumDoctor = IdDoctor,
                    Apellido = Apellido,
                    Especialidad = Especialidad,
                    Salario = Salario,
                    Imagen = Imagen
                };
                string request = "/api/Doctores";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(doc);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }
        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            string request = "/api/Doctores";
            List<Doctor> doctores =
              await this.CallApiAsync<List<Doctor>>(request);
            return doctores;
        }

    }
}

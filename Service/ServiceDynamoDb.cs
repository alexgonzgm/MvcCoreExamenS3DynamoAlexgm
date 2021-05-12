using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using MvcCoreExamenS3DynamoAlexgm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreExamenS3DynamoAlexgm.Service
{
    public class ServiceDynamoDb
    {
        DynamoDBContext context;
        public ServiceDynamoDb()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            this.context = new DynamoDBContext(client);
        }

        public async Task CreateUsuario(Usuario usuario)
        {
            await this.context.SaveAsync<Usuario>(usuario);
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            var tabla = this.context.GetTargetTable<Usuario>();
            var scanOptions = new ScanOperationConfig();
            var results = tabla.Scan(scanOptions);

            List<Document> data = await results.GetNextSetAsync();
            IEnumerable<Usuario> usuarios = this.context.FromDocuments<Usuario>(data);
            return usuarios.ToList();
        }

        public async Task<Usuario> FindUsuario(int idusuario)
        {
            return await this.context.LoadAsync<Usuario>(idusuario);
        }

        public async Task DeleteUsuario(int idusuario)
        {
            await this.context.DeleteAsync<Usuario>(idusuario);
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            Usuario usuarioactualizar = await this.FindUsuario(usuario.IdUsuario);
            usuarioactualizar.Nombre = usuario.Nombre;
            usuarioactualizar.Descripcion = usuario.Descripcion;
            usuarioactualizar.FechaAlta = usuario.FechaAlta;
            usuarioactualizar.Fotos = usuario.Fotos;
            await this.context.SaveAsync<Usuario>(usuarioactualizar);

        }
    }
}

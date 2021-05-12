using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreExamenS3DynamoAlexgm.Models
{
    [DynamoDBTable("usuarios")]

    public class Usuario
    {
        [DynamoDBProperty("IdUsuario")]
        [DynamoDBHashKey]
        public int IdUsuario { get; set; }
        [DynamoDBProperty("Nombre")]
        public string Nombre { get; set; }
        [DynamoDBProperty("Descripcion")]
        public string Descripcion { get; set; }
        [DynamoDBProperty("FechaAlta")]
        public DateTime FechaAlta { get; set; }
        [DynamoDBProperty("Fotos")]
        public Fotos Fotos { get; set; }
    }
}

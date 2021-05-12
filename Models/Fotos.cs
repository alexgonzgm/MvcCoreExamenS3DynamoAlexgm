using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreExamenS3DynamoAlexgm.Models
{
    public class Fotos
    {
        [DynamoDBProperty("Titulo")]
        public string Titulo { get; set; }
        [DynamoDBProperty("Imagen")]
        public string Imagen { get; set; }
    }
}

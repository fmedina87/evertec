using EvertecPruebas.Domain.UserEntitys;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace EvertecPruebas.Api.Filters
{
    public class SwaggerFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.MemberInfo != null && context.MemberInfo.ReflectedType != null)
            {
                schema.Example = GetDataFoSwagger(context.MemberInfo.ReflectedType.Name, context.MemberInfo.Name);
            }
        }
        private static IOpenApiAny? GetDataFoSwagger(string typeName, string propetyName)
        {
            switch (typeName)
            {

                case "UsuarioRequestAdd":
                    UsuarioRequestAdd usuarioRequestAdd = new()
                    {
                        IdEstadoCivil = 0,
                        PrimerNombre = "Pruebas",
                        PrimerApellido = "Evertec",
                        SegundoNombre = null,
                        SegundoApellido = null,
                        FechaNacimiento = DateTime.MaxValue,
                        TieneHermanos = true
                    };
                    return ConverterToOpenApi(usuarioRequestAdd, propetyName);
                
                default:
                    return default;
            }
        }
        private static OpenApiString ConverterToOpenApi(object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo? property = type.GetProperty(propertyName);
            if (property != null)
            {
                object? propValue = property.GetValue(obj, null);
                if (propValue != null && !string.IsNullOrEmpty(propValue.ToString()))
                    return new OpenApiString(propValue.ToString());
            }
            return new OpenApiString(null);
        }
    }
}

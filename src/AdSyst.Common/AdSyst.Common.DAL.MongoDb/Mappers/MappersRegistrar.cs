using System.Reflection;

namespace AdSyst.Common.DAL.MongoDb.Mappers
{
    public static class MappersRegistrar
    {
        public static void RegisterMappersFromAssembly(Assembly assembly)
        {
            var classMappers = assembly
                .GetExportedTypes()
                .Where(
                    t =>
                        t.GetInterfaces()
                            .Any(
                                i =>
                                    i.IsGenericType
                                    && i.GetGenericTypeDefinition() == typeof(IClassMapper<>)
                            )
                )
                .ToList();
            foreach (var mapperType in classMappers)
            {
                object? mapperInstance = Activator.CreateInstance(mapperType);
                var methodInfo = typeof(IClassMapper<>)
                    .MakeGenericType(mapperType)
                    .GetMethod("Register");
                methodInfo?.Invoke(mapperInstance, null);
            }
        }
    }
}

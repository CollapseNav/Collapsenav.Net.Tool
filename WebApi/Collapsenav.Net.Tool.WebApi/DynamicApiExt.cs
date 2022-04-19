using System.Reflection;
using System.Reflection.Emit;
using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.WebApi;

public static class DynamicApiExt
{
    public static AssemblyBuilder Ass = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("NewController"), AssemblyBuilderAccess.Run);
    public static ModuleBuilder MB = Ass.DefineDynamicModule("NewController");

    public static TypeBuilder GenerateTypeBuilder(Type baseType, string typeName, string front = "")
    {
        var typeBuilder = MB.DefineType(typeName, TypeAttributes.Class | TypeAttributes.Public, baseType, null);
        var typeCtor = baseType.GetConstructors()[0].GetParameters().Select(item => item.ParameterType)?.ToArray();
        typeBuilder.SetCustomAttribute(new(typeof(RouteAttribute).GetConstructor(new[] { typeof(string) }), new object[] { $"{front}[controller]" }));
        var ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard | CallingConventions.HasThis, typeCtor);
        var ilGenerator = ctor.GetILGenerator();
        for (var i = 0; i <= typeCtor.Length; i++)
            ilGenerator.Emit(OpCodes.Ldarg, i);
        ilGenerator.Emit(OpCodes.Call, baseType.GetConstructors()[0]);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Ret);
        return typeBuilder;
    }

    public static Type GenerateType(Type baseType, string typeName, string front = "")
    {
        var typeBuilder = GenerateTypeBuilder(baseType, typeName, front);
#if NETSTANDARD2_0
        var type = typeBuilder.CreateTypeInfo().AsType();
#else
        var type = typeBuilder.CreateType();
#endif
        return type;
    }
    public static IServiceCollection AddQueryApi<T, GetT>(this IServiceCollection services, string route, string front = "") where T : class, IEntity where GetT : IBaseGet<T>
    {
        var typeName = $"{route}Controller";
        var type = GenerateType(typeof(QueryRepController<T, GetT>), typeName, front);
        services.AddTransient(typeof(IQueryRepository<T>), typeof(QueryRepository<T>));
        services.AddTransient(typeof(IQueryController<T, GetT>), type);
        services.AddDynamicController();
        return services;
    }
    public static IServiceCollection AddQueryApi<TKey, T, GetT>(this IServiceCollection services, string route, string front = "") where T : class, IBaseEntity<TKey> where GetT : IBaseGet<T>
    {
        var typeName = $"{route}Controller";
        var type = GenerateType(typeof(QueryRepController<TKey, T, GetT>), typeName, front);
        services.AddTransient(typeof(IQueryRepository<TKey, T>), typeof(QueryRepository<TKey, T>));
        services.AddTransient(typeof(IQueryController<TKey, T, GetT>), type);
        services.AddDynamicController();
        return services;
    }

    public static IServiceCollection AddModifyApi<T, CreateT>(this IServiceCollection services, string route, string front = "") where T : class, IEntity, new() where CreateT : IBaseCreate<T>
    {
        var typeName = $"{route}Controller";
        var type = GenerateType(typeof(ModifyRepController<T, CreateT>), typeName, front);
        services.AddTransient(typeof(IModifyRepository<T>), typeof(ModifyRepository<T>));
        services.AddTransient(typeof(IModifyController<T, CreateT>), type);
        services.AddDynamicController();
        return services;
    }
    public static IServiceCollection AddModifyApi<TKey, T, CreateT>(this IServiceCollection services, string route, string front = "") where T : class, IBaseEntity<TKey>, new() where CreateT : IBaseCreate<T>
    {
        var typeName = $"{route}Controller";
        var type = GenerateType(typeof(ModifyRepController<TKey, T, CreateT>), typeName, front);
        services.AddTransient(typeof(IModifyRepository<TKey, T>), typeof(ModifyRepository<TKey, T>));
        services.AddTransient(typeof(IModifyController<TKey, T, CreateT>), type);
        services.AddDynamicController();
        return services;
    }
    public static IServiceCollection AddCrudApi<T, CreateT, GetT>(this IServiceCollection services, string route, string front = "") where T : class, IEntity, new() where CreateT : IBaseCreate<T> where GetT : IBaseGet<T>
    {
        var typeName = $"{route}Controller";
        var type = GenerateType(typeof(CrudRepController<T, CreateT, GetT>), typeName, front);
        services.AddTransient(typeof(ICrudRepository<T>), typeof(CrudRepository<T>));
        services.AddTransient(typeof(ICrudController<T, CreateT, GetT>), type);
        services.AddDynamicController();
        return services;
    }
    public static IServiceCollection AddCrudApi<TKey, T, CreateT, GetT>(this IServiceCollection services, string route, string front = "") where T : class, IBaseEntity<TKey>, new() where CreateT : IBaseCreate<T> where GetT : IBaseGet<T>
    {
        var typeName = $"{route}Controller";
        var type = GenerateType(typeof(CrudRepController<TKey, T, CreateT, GetT>), typeName, front);
        services.AddTransient(typeof(ICrudRepository<TKey, T>), typeof(CrudRepository<TKey, T>));
        services.AddTransient(typeof(ICrudController<TKey, T, CreateT, GetT>), type);
        services.AddDynamicController();
        return services;
    }
}
using System.Reflection;
using System.Reflection.Emit;
using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.WebApi;

public static class AddNewControllerExt
{
    public static AssemblyBuilder Ass = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("NewController"), AssemblyBuilderAccess.Run);
    public static ModuleBuilder MB = Ass.DefineDynamicModule("NewController");
    public static IServiceCollection AddQueryRepController<T, GetT>(this IServiceCollection services, string route, string front = "") where T : class, IEntity where GetT : IBaseGet<T>
    {
        var typeName = $"{route}Controller";
        var typeBuilder = MB.DefineType(typeName, TypeAttributes.Class | TypeAttributes.Public, typeof(QueryRepController<T, GetT>), null);
        typeBuilder.SetCustomAttribute(new(typeof(RouteAttribute).GetConstructor(new[] { typeof(string) }), new object[] { $"{front}[controller]" }));
        var ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard | CallingConventions.HasThis, new[] { typeof(IQueryRepository<T>) });
        var ilGenerator = ctor.GetILGenerator();
        ilGenerator.Emit(OpCodes.Ldarg, 0);
        ilGenerator.Emit(OpCodes.Ldarg, 1);
        ilGenerator.Emit(OpCodes.Call, typeof(QueryRepController<T, GetT>).GetConstructors()[0]);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Ret);
#if NETSTANDARD2_0
        var type = typeBuilder.CreateTypeInfo().AsType();
#else
        var type = typeBuilder.CreateType();
#endif
        services.AddTransient(typeof(IQueryRepository<T>), typeof(QueryRepository<T>));
        services.AddTransient(typeof(IQueryController<T, GetT>), type);
        services.AddDynamicController();
        return services;
    }
    public static IServiceCollection AddQueryRepController<TKey, T, GetT>(this IServiceCollection services, string route, string front = "") where T : class, IBaseEntity<TKey> where GetT : IBaseGet<T>
    {
        var typeName = $"{route}Controller";
        var typeBuilder = MB.DefineType(typeName, TypeAttributes.Class | TypeAttributes.Public, typeof(QueryRepController<TKey, T, GetT>), null);
        typeBuilder.SetCustomAttribute(new(typeof(RouteAttribute).GetConstructor(new[] { typeof(string) }), new object[] { $"{front}[controller]" }));
        var ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard | CallingConventions.HasThis, new[] { typeof(IQueryRepository<TKey, T>) });
        var ilGenerator = ctor.GetILGenerator();
        ilGenerator.Emit(OpCodes.Ldarg, 0);
        ilGenerator.Emit(OpCodes.Ldarg, 1);
        ilGenerator.Emit(OpCodes.Call, typeof(QueryRepController<TKey, T, GetT>).GetConstructors()[0]);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Ret);
#if NETSTANDARD2_0
        var type = typeBuilder.CreateTypeInfo().AsType();
#else
        var type = typeBuilder.CreateType();
#endif
        services.AddTransient(typeof(IQueryRepository<TKey, T>), typeof(QueryRepository<TKey, T>));
        services.AddTransient(typeof(IQueryController<TKey, T, GetT>), type);
        services.AddDynamicController();
        return services;
    }

    public static IServiceCollection AddModifyRepController<T, CreateT>(this IServiceCollection services, string route, string front = "") where T : class, IEntity, new() where CreateT : IBaseCreate<T>
    {
        var typeName = $"{route}Controller";
        var typeBuilder = MB.DefineType(typeName, TypeAttributes.Class | TypeAttributes.Public, typeof(ModifyRepController<T, CreateT>), null);
        typeBuilder.SetCustomAttribute(new(typeof(RouteAttribute).GetConstructor(new[] { typeof(string) }), new object[] { $"{front}[controller]" }));
        var ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard | CallingConventions.HasThis, new[] { typeof(IModifyRepository<T>), typeof(IMap) });
        var ilGenerator = ctor.GetILGenerator();
        ilGenerator.Emit(OpCodes.Ldarg, 0);
        ilGenerator.Emit(OpCodes.Ldarg, 1);
        ilGenerator.Emit(OpCodes.Ldarg, 2);
        ilGenerator.Emit(OpCodes.Call, typeof(ModifyRepController<T, CreateT>).GetConstructors()[0]);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Ret);
#if NETSTANDARD2_0
        var type = typeBuilder.CreateTypeInfo().AsType();
#else
        var type = typeBuilder.CreateType();
#endif
        services.AddTransient(typeof(IModifyRepository<T>), typeof(ModifyRepository<T>));
        services.AddTransient(typeof(IModifyController<T, CreateT>), type);
        services.AddDynamicController();
        return services;
    }
    public static IServiceCollection AddModifyRepController<TKey, T, CreateT>(this IServiceCollection services, string route, string front = "") where T : class, IBaseEntity<TKey>, new() where CreateT : IBaseCreate<T>
    {
        var typeName = $"{route}Controller";
        var typeBuilder = MB.DefineType(typeName, TypeAttributes.Class | TypeAttributes.Public, typeof(ModifyRepController<TKey, T, CreateT>), null);
        typeBuilder.SetCustomAttribute(new(typeof(RouteAttribute).GetConstructor(new[] { typeof(string) }), new object[] { $"{front}[controller]" }));
        var ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard | CallingConventions.HasThis, new[] { typeof(IModifyRepository<TKey, T>), typeof(IMap) });
        var ilGenerator = ctor.GetILGenerator();
        ilGenerator.Emit(OpCodes.Ldarg, 0);
        ilGenerator.Emit(OpCodes.Ldarg, 1);
        ilGenerator.Emit(OpCodes.Ldarg, 2);
        ilGenerator.Emit(OpCodes.Call, typeof(ModifyRepController<T, CreateT>).GetConstructors()[0]);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Ret);
#if NETSTANDARD2_0
        var type = typeBuilder.CreateTypeInfo().AsType();
#else
        var type = typeBuilder.CreateType();
#endif
        services.AddTransient(typeof(IModifyRepository<TKey, T>), typeof(ModifyRepository<TKey, T>));
        services.AddTransient(typeof(IModifyController<TKey, T, CreateT>), type);
        services.AddDynamicController();
        return services;
    }
    public static IServiceCollection AddCrudRepController<T, CreateT, GetT>(this IServiceCollection services, string route, string front = "") where T : class, IEntity, new() where CreateT : IBaseCreate<T> where GetT : IBaseGet<T>
    {
        var typeName = $"{route}Controller";
        var typeBuilder = MB.DefineType(typeName, TypeAttributes.Class | TypeAttributes.Public, typeof(CrudRepController<T, CreateT, GetT>), null);
        typeBuilder.SetCustomAttribute(new(typeof(RouteAttribute).GetConstructor(new[] { typeof(string) }), new object[] { $"{front}[controller]" }));
        var ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard | CallingConventions.HasThis, new[] { typeof(ICrudRepository<T>), typeof(IMap) });
        var ilGenerator = ctor.GetILGenerator();
        ilGenerator.Emit(OpCodes.Ldarg, 0);
        ilGenerator.Emit(OpCodes.Ldarg, 1);
        ilGenerator.Emit(OpCodes.Ldarg, 2);
        ilGenerator.Emit(OpCodes.Call, typeof(CrudRepController<T, CreateT, GetT>).GetConstructors()[0]);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Ret);
#if NETSTANDARD2_0
        var type = typeBuilder.CreateTypeInfo().AsType();
#else
        var type = typeBuilder.CreateType();
#endif
        services.AddTransient(typeof(ICrudRepository<T>), typeof(CrudRepository<T>));
        services.AddTransient(typeof(ICrudController<T, CreateT, GetT>), type);
        services.AddDynamicController();
        return services;
    }
    public static IServiceCollection AddCrudRepController<TKey, T, CreateT, GetT>(this IServiceCollection services, string route, string front = "") where T : class, IBaseEntity<TKey>, new() where CreateT : IBaseCreate<T> where GetT : IBaseGet<T>
    {
        var typeName = $"{route}Controller";
        var typeBuilder = MB.DefineType(typeName, TypeAttributes.Class | TypeAttributes.Public, typeof(CrudRepController<TKey, T, CreateT, GetT>), null);
        typeBuilder.SetCustomAttribute(new(typeof(RouteAttribute).GetConstructor(new[] { typeof(string) }), new object[] { $"{front}[controller]" }));
        var ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard | CallingConventions.HasThis, new[] { typeof(ICrudRepository<TKey, T>), typeof(IMap) });
        var ilGenerator = ctor.GetILGenerator();
        ilGenerator.Emit(OpCodes.Ldarg, 0);
        ilGenerator.Emit(OpCodes.Ldarg, 1);
        ilGenerator.Emit(OpCodes.Ldarg, 2);
        ilGenerator.Emit(OpCodes.Call, typeof(CrudRepController<TKey, T, CreateT, GetT>).GetConstructors()[0]);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Nop);
        ilGenerator.Emit(OpCodes.Ret);
#if NETSTANDARD2_0
        var type = typeBuilder.CreateTypeInfo().AsType();
#else
        var type = typeBuilder.CreateType();
#endif
        services.AddTransient(typeof(ICrudRepository<TKey, T>), typeof(CrudRepository<TKey, T>));
        services.AddTransient(typeof(ICrudController<TKey, T, CreateT, GetT>), type);
        services.AddDynamicController();
        return services;
    }

}
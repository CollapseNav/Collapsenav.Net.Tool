using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.WebApi.Test;

public class ApplicationTest
{
    public ApplicationTest()
    {
        Provider = DIConfig.GetAppProvider();
    }
    protected readonly IServiceProvider Provider;
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact]
    public void WriteRepAppTest()
    {
    }
}
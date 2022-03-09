using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;

namespace Collapsenav.Net.Tool.WebApi;
public class AddControllerChangeProvider : IActionDescriptorChangeProvider
{
    public static AddControllerChangeProvider Instance { get; } = new AddControllerChangeProvider();

    public CancellationTokenSource TokenSource { get; private set; }

    public bool HasChanged { get; set; }

    public IChangeToken GetChangeToken()
    {
        TokenSource = new CancellationTokenSource();
        return new CancellationChangeToken(TokenSource.Token);
    }
}
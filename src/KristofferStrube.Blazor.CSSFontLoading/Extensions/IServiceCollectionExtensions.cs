using Microsoft.Extensions.DependencyInjection;

namespace KristofferStrube.Blazor.CSSFontLoading;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDocumentService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IDocumentService, DocumentService>();
    }
}

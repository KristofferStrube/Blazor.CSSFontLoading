using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.CSSFontLoading;

public class DocumentService : IDocumentService
{
    protected readonly IJSRuntime jSRuntime;

    /// <summary>
    /// Constructs a <see cref="DocumentService"/> that can be used to access the mixin part of the Document interface defined in the CSS Font Loading definition.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    public DocumentService(IJSRuntime jSRuntime)
    {
        this.jSRuntime = jSRuntime;
    }

    /// <summary>
    /// Gets the <c>fonts</c> attribute of <c>Document</c>.
    /// </summary>
    /// <returns></returns>
    public async Task<FontFaceSet> GetFontsAsync()
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("document.fonts.valueOf");
        return new FontFaceSet(jSRuntime, jSInstance);
    }
}

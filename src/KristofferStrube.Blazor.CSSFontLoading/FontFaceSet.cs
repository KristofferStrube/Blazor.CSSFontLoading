using KristofferStrube.Blazor.CSSFontLoading.Extensions;
using KristofferStrube.Blazor.DOM;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.CSSFontLoading;

public class FontFaceSet : EventTarget
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="FontFaceSet"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="FontFaceSet"/>.</param>
    /// <returns>A wrapper instance for a <see cref="FontFaceSet"/>.</returns>
    public static new FontFaceSet Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new FontFaceSet(jSRuntime, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="initialFaces">The fonts faces that will make the new <see cref="FontFaceSet"/>.</param>
    /// <returns></returns>
    public static async Task<FontFaceSet> CreateAsync(IJSRuntime jSRuntime, IList<FontFace> initialFaces)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructFontFaceSet", initialFaces.ToArray());
        return new FontFaceSet(jSRuntime, jSInstance);
    }

    internal FontFaceSet(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<FontFace[]> ValuesAsync()
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSValues = await JSReference.InvokeAsync<IJSObjectReference>("values");
        IJSObjectReference jSEntries = await helper.InvokeAsync<IJSObjectReference>("arrayFrom", jSValues);
        int length = await helper.InvokeAsync<int>("getAttribute", jSEntries, "length");
        return await Task.WhenAll(
            Enumerable
                .Range(0, length)
                .Select(async i =>
                    new FontFace(
                        jSRuntime,
                        await jSEntries.InvokeAsync<IJSObjectReference>("at", i))
                )
                .ToArray()
        );
    }

    /// <summary>
    /// Adds a <paramref name="font"/> to this <see cref="FontFaceSet"/>, if it is not already in it, and returns the <see cref="FontFaceSet"/>.
    /// </summary>
    /// <param name="font">The <see cref="FontFace"/> that will be added.</param>
    /// <returns>This <see cref="FontFaceSet"/>.</returns>
    public async Task<FontFaceSet> AddAsync(FontFace font)
    {
        var jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("add", font.JSReference);
        return new FontFaceSet(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Returns <see langword="false"/> immediately if the font is <c>CSS-connected</c> meaning it has an associated <c>@font-face</c> definition.
    /// Else if the font is in the <see cref="FontFaceSet"/>'s set of either loaded or failed fonts then it removes it and returns <see langword="true"/>.
    /// Else If the font is currently being loaded into the <see cref="FontFaceSet"/> then it removes it and returns <see langword="true"/>.
    /// ELse it returns <see langword="false"/>.
    /// </summary>
    /// <param name="font">The <see cref="FontFace"/> that will be deleted from the <see cref="FontFaceSet"/>.</param>
    /// <returns><see langword="true"/> if the font was removed; else <see langword="false"/>.</returns>
    public async Task<bool> DeleteAsync(FontFace font)
    {
        return await JSReference.InvokeAsync<bool>("delete", font.JSReference);
    }

    /// <summary>
    /// Removes all non-<c>CSS-connected</c> <see cref="FontFace"/>s from the <see cref="FontFaceSet"/>.
    /// </summary>
    /// <returns></returns>
    public async Task ClearAsync()
    {
        await JSReference.InvokeVoidAsync("clear");
    }

    /// <summary>
    /// Finds a font that matches the specification of the <paramref name="font"/> which has supporting for displaying the string given <paramref name="text"/>.
    /// </summary>
    /// <param name="font">The font in format <c>"12px arial"</c>.</param>
    /// <param name="text">Some text that you wish to check if the font can display.</param>
    /// <returns><see langword="true"/> if the font was in the <see cref="FontFaceSet"/> or a system font; else <see langword="false"/>.</returns>
    public async Task<bool> CheckAsync(string font, string text = " ")
    {
        return await JSReference.InvokeAsync<bool>("check", font, text);
    }
}
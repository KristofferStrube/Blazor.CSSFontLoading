using KristofferStrube.Blazor.CSSFontLoading.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.CSSFontLoading;

public class FontFace : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="FontFace"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="FontFace"/>.</param>
    /// <returns>A wrapper instance for a <see cref="FontFace"/>.</returns>
    public static FontFace Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new FontFace(jSRuntime, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="familiy">The font family name.</param>
    /// <param name="source">The source of the font as an CSS 'src' descriptor of the '@font-face' rule. i.e. <c>"url(http://example.com/myFont.woff)"</c>.</param>
    /// <param name="descriptors">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns></returns>
    public static async Task<FontFace> CreateAsync(IJSRuntime jSRuntime, string familiy, string source, FontFaceDescriptors? descriptors = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructFontFace", familiy, source, descriptors);
        return new FontFace(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="familiy">The font family name.</param>
    /// <param name="source">The source of the font in its raw form of a byte array.</param>
    /// <param name="descriptors">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns></returns>
    public static async Task<FontFace> CreateAsync(IJSRuntime jSRuntime, string familiy, byte[] source, FontFaceDescriptors? descriptors = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSSource = await helper.InvokeAsync<IJSObjectReference>("arrayBufferFrom", source);
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructFontFace", familiy, jSSource, descriptors);
        return new FontFace(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="familiy">The font family name.</param>
    /// <param name="source">The source of the font as a <see cref="IJSObjectReference"/> to an ArrayBuffer or an ArrayBufferView.</param>
    /// <param name="descriptors">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns></returns>
    public static async Task<FontFace> CreateAsync(IJSRuntime jSRuntime, string familiy, IJSObjectReference source, FontFaceDescriptors? descriptors = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructFontFace", familiy, source, descriptors);
        return new FontFace(jSRuntime, jSInstance);
    }

    public FontFace(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<string> GetFamilyAsync()
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "family");
    }
}
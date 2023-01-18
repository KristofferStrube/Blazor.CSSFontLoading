namespace KristofferStrube.Blazor.CSSFontLoading
{
    public interface IDocumentService
    {
        Task<FontFaceSet> GetFontsAsync();
    }
}
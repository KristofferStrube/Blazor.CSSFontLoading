using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.CSSFontLoading;

public class FontFaceDescriptors
{
    [JsonPropertyName("style")]
    public string Style = "normal";

    [JsonPropertyName("weight")]
    public string Weight = "normal";

    [JsonPropertyName("strech")]
    public string Stretch = "normal";

    [JsonPropertyName("unicodeRange")]
    public string UnicodeRange = "U+0-10FFFF";

    [JsonPropertyName("variant")]
    public string Variant = "normal";

    [JsonPropertyName("featureSettings ")]
    public string FeatureSettings = "normal";

    [JsonPropertyName("variationSettings")]
    public string VariationSettings = "normal";

    [JsonPropertyName("display")]
    public string Display = "auto";

    [JsonPropertyName("ascentOverride")]
    public string AscentOverride = "normal";

    [JsonPropertyName("descentOverride")]
    public string DescentOverride = "normal";

    [JsonPropertyName("lineGapOverride")]
    public string LineGapOverride = "normal";
}
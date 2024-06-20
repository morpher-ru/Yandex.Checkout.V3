// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Yandex.Checkout.V3;

public class Error
{
    public string Type { get; set; }
    public string Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Parameter { get; set; }

    public override string ToString()
    {
        string s = Description;
        if (Parameter != null) s += " " + Parameter;
        return s;
    }
}

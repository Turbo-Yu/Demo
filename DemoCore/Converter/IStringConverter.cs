namespace Demo.Core.Converter
{

    public interface IStringConverter
    {
        object ConvertTo(string value, out bool succeeded);
    }
}


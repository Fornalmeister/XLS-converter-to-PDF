namespace XLS_converter_to_PDF.Helpers.IHelpers
{
    public interface IChangeExtenstionHelper
    {
        Task<string> ChangeExtensionXLSToPDF(string name);
    }
}

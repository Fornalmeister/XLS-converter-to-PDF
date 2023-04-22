using XLS_converter_to_PDF.Helpers.IHelpers;

namespace XLS_converter_to_PDF.Helpers
{
    public class ChangeExtenstionHelper : IChangeExtenstionHelper
    {
        public async Task<string> ChangeExtensionXLSToPDF(string name)
        {
            string namePdf = name.Remove(name.Length - 4);
            namePdf = namePdf + ".pdf";
            return namePdf;
        }
    }
}

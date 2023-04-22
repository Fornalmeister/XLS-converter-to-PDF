using XLS_converter_to_PDF.Helpers.IHelpers;

namespace XLS_converter_to_PDF.Helpers
{
    public class CheckIsXLSHelper : ICheckIsXLSHelper
    {
        public bool IsItXlsFile(string name)
        {
            string nameFile = name.Substring(name.Length - 4);
            if (nameFile == ".xls")
                return true;
            else
                return false;


        }
    }
}

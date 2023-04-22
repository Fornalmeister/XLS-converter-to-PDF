namespace XLS_converter_to_PDF.ViewModel
{
    public class GeneratedPDF
    {
        public int Count { get; set; }
        public long Size { get; set; }
        public IEnumerable<string> FilePaths { get; set; }
        public IEnumerable<string> FileNamePDFs { get; set; }
    }
}

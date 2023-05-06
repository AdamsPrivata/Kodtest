namespace CodeTest.Utilities
{
    public class PackageNotFoundException : Exception
    {
        public PackageNotFoundException(string message) : base(message) { }
    }
}

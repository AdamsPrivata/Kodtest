namespace CodeTest.Models
{
    public class Package
    {
        private const int MAX_WEIGHT = 20000;
        private const int MAX_LENGTH = 60;
        private const int MAX_HEIGHT = 60;
        private const int MAX_WIDTH = 60;

        public Package(int weight, int height, int width, int length)
        {
            Weight = weight;
            Height = height;
            Width = width;
            Length = length;
        }

        public string Id { get; set; }
        public int Weight { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int Length { get; private set; }
        public bool IsValid 
        { 
            get 
            { 
                return Weight <= MAX_WEIGHT &&
                    Height <= MAX_HEIGHT &&
                    Width <= MAX_WIDTH &&
                    Length <= MAX_LENGTH;
            } 
        } 
    }
}

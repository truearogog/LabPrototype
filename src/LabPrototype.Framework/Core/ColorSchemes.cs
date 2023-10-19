namespace LabPrototype.Framework.Core
{
    public readonly partial record struct ColorScheme
    {
        public static ColorScheme Red => new("Red", "#c0392b", "#e74c3c");
        public static ColorScheme Blue => new("Blue", "#2980b9", "#3498db");
        public static ColorScheme Yellow => new("Yellow", "#f39c12", "#f1c40f");
        public static ColorScheme Orange => new("Orange", "#d35400", "#e67e22");
        public static ColorScheme Green => new("Green", "#27ae60", "#2ecc71");
        public static ColorScheme Purple => new("Purple", "#8e44ad", "#9b59b6");
        public static ColorScheme Turquoise => new("Turquoise", "#16a085", "#1abc9c");
        public static ColorScheme Silver => new("Silver", "#bdc3c7", "#ecf0f1");
        public static ColorScheme Midnight => new("Midnight", "#2c3e50", "#34495e");

        public static IEnumerable<ColorScheme> All => new[]
        {
            Red, 
            Blue, 
            Green, 
            Yellow, 
            Orange, 
            Green, 
            Purple, 
            Turquoise, 
            Silver, 
            Midnight,
        };
    }
}

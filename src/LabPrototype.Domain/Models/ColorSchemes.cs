namespace LabPrototype.Domain.Models
{
    public readonly partial struct ColorScheme
    {
        public static ColorScheme Red => new ColorScheme("Red", "#c0392b", "#e74c3c");
        public static ColorScheme Blue => new ColorScheme("Blue", "#2980b9", "#3498db");
        public static ColorScheme Yellow => new ColorScheme("Yellow", "#f39c12", "#f1c40f");
        public static ColorScheme Orange => new ColorScheme("Orange", "#d35400", "#e67e22");
        public static ColorScheme Green => new ColorScheme("Green", "#27ae60", "#2ecc71");
        public static ColorScheme Purple => new ColorScheme("Purple", "#8e44ad", "#9b59b6");
        public static ColorScheme Turquoise => new ColorScheme("Turquoise", "#16a085", "#1abc9c");
        public static ColorScheme Silver => new ColorScheme("Silver", "#bdc3c7", "#ecf0f1");
        public static ColorScheme Midnight => new ColorScheme("Midnight", "#2c3e50", "#34495e");
    }
}

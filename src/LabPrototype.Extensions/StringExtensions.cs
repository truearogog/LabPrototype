using System.Drawing;

namespace LabPrototype.Extensions
{
    public static class StringExtensions
    {
        public static Color ToColor(this string str)
        {
            var cstr = "FF" + str.Remove(0, 1);
            return Color.FromArgb(Convert.ToInt32(cstr, 16));
        }
    }
}

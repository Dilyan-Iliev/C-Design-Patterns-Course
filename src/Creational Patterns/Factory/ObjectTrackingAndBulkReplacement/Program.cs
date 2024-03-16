using System.Text;

namespace ObjectTrackingAndBulkReplacement
{
    public interface ITheme
    {
        string TextColor { get; }
        public string BgColor { get; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new TrackingThemeFactory();
            var theme1 = factory.CreateTheme(false);
            var theme2 = factory.CreateTheme(true);

            Console.WriteLine(factory.Info);

            var factory2 = new ReplaceableThemeFactory();
            var magicTheme = factory2.CreateTheme(true);
            Console.WriteLine(magicTheme.Value.BgColor);

            factory2.ReplaceTheme(false);
            Console.WriteLine(magicTheme.Value.BgColor);
        }
    }

    public class LightTheme : ITheme
    {
        public string TextColor => "black";

        public string BgColor => "white";
    }

    public class DarkTheme : ITheme
    {
        public string TextColor => "white";

        public string BgColor => "dark gray";
    }

    public class TrackingThemeFactory
    {
        private readonly List<WeakReference<ITheme>> themes = new();

        public ITheme CreateTheme(bool isDark)
        {
            ITheme theme = isDark ? new DarkTheme() : new LightTheme();
            themes.Add(new WeakReference<ITheme>(theme));
            return theme;
        }

        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var reference in themes)
                {
                    if (reference.TryGetTarget(out var theme))
                    {
                        bool isDark = theme is DarkTheme;
                        sb.Append(isDark ? "Dark" : "Light")
                            .AppendLine(" theme");
                    }
                }

                return sb.ToString();
            }
        }
    }

    public class Ref<T> where T : class
    {
        public T Value;

        public Ref(T value)
        {
            this.Value = value;
        }
    }

    public class ReplaceableThemeFactory
    {
        private readonly List<WeakReference<Ref<ITheme>>> themese = new();

        private ITheme CreateThemeImpl(bool isDark)
        {
            return isDark ? new DarkTheme() : new LightTheme();
        }

        public Ref<ITheme> CreateTheme(bool isDark)
        {
            var r = new Ref<ITheme>(CreateThemeImpl(isDark));
            themese.Add(new WeakReference<Ref<ITheme>>(r));
            return r;
        }

        public void ReplaceTheme(bool isDark)
        {
            foreach (var item in themese)
            {
                if (item.TryGetTarget(out var themeReference))
                {
                    themeReference.Value = CreateThemeImpl(isDark);
                }
            }
        }
    }
}

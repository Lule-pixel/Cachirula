namespace Cachirula
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
        }

        int count = 0;
        const int Meta = 15;
        Color? colorInicial;                 // se toma del estilo la primera vez
        readonly Color colorFinal = Colors.Red;

        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            if (count >= Meta)
            {
                ImagenGrande.Source = "imagesfin.jpg";
                Capa.Opacity = 0;
                Capa.IsVisible = true;
                await Capa.FadeToAsync(1, 250);
                return;
            }

            colorInicial ??= CounterBtn.BackgroundColor;   // el morado (o el del tema oscuro)
            count++;

            if (count >= Meta)
            {
                CounterBtn.Text = "Presiona para abrir contenido";
                CounterBtn.BackgroundColor = colorFinal;
            }
            else
            {
                CounterBtn.Text = count == 1 ? "Clicked 1 time" : $"Clicked {count} times";
                double t = (double)count / Meta;            // 0 → 1 conforme avanza
                CounterBtn.BackgroundColor = Mezclar(colorInicial, colorFinal, t);
            }
            
            ResetBtn.IsVisible = count > 0; 
            Imagen.Source = imagenes[count % imagenes.Length];
            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void ResetClicked(Object sender, EventArgs e)
        {
            count = 0;
            CounterBtn.Text = "Click me";
            CounterBtn.ClearValue(VisualElement.BackgroundColorProperty);  // vuelve al estilo
            ResetBtn.IsVisible = false;
            Imagen.Source = imagenes[0];
            
        }

        static Color Mezclar(Color a, Color b, double t)
        {
            float f = (float)t;
            return new Color(
                a.Red + (b.Red - a.Red) * f,
                a.Green + (b.Green - a.Green) * f,
                a.Blue + (b.Blue - a.Blue) * f,
                a.Alpha + (b.Alpha - a.Alpha) * f);
        }

        readonly string[] imagenes =
        {
            "images1.jpg",
            "images2.jpg",
            "images3.jpg",
            "images4.jpg",
            "images5.jpg",
            "images6.jpg",
            "images7.jpg",
            "images8.jpg",
            "images9.jpg",
            "images10.jpg",
            "images11.jpg",
            "images12.jpg",
            "images13.jpg",
            "images14.jpg",
            "images15.jpg"
        };

        private async void CerrarCapa(Object sender, EventArgs e)
        {
  
            count = 0;
            CounterBtn.Text = "Click me";
            CounterBtn.ClearValue(VisualElement.BackgroundColorProperty);  // vuelve al estilo
            ResetBtn.IsVisible = false;
            Imagen.Source = imagenes[0];

            await Capa.FadeToAsync(0, 200);
            Capa.IsVisible = false;
        }
    }
}

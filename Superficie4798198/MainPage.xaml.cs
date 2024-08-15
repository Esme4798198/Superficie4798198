namespace Superficie4798198
{
    public partial class MainPage : ContentPage
    {

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listview.ItemsSource = await _dbService.GetResultado());
        }

        private readonly LocalDbService _dbService;
        private int _editResultadoId;


        private async void calcularBtn_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Entryaltura.Text) || !string.IsNullOrEmpty(Entrybase.Text))
            {
                if (double.TryParse(Entryaltura.Text, out double alt) && double.TryParse(Entrybase.Text, out double bas))
                {
                    labelresultado.Text = ((alt * bas)/ 2).ToString();
                    if (_editResultadoId == 0)
                    {
                        //Agrega resultados
                        await _dbService.Create(new Triángulo
                        {
                            Altura = Entryaltura.Text,
                            Base = Entrybase.Text,
                            Superficie = labelresultado.Text
                        });
                    }
                    else
                    {
                        //Edita resultado
                        await _dbService.Update(new Triángulo
                        {
                            Id = _editResultadoId,
                            Altura = Entryaltura.Text,
                            Base = Entrybase.Text,
                            Superficie = labelresultado.Text
                        });
                        _editResultadoId = 0;

                    }
                    Entryaltura.Text = string.Empty;
                    Entrybase.Text = string.Empty;
                    labelresultado.Text = string.Empty;
                    listview.ItemsSource = await _dbService.GetResultado();
                }
                else
                {
                    labelresultado.Text = "Ingrese solamente números válidos";
                }
            }
            else
            {
                labelresultado.Text = "Llena todos los campos de lo que se le solicita";
            }
        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var triangulo = (Triángulo)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editResultadoId = triangulo.Id;
                    Entryaltura.Text = triangulo.Altura;
                    Entrybase.Text = triangulo.Base;
                    labelresultado.Text = triangulo.Superficie;
                    break;

                case "Delete":
                    await _dbService.Delete(triangulo);
                    listview.ItemsSource = await _dbService.GetResultado();
                   break;
            }
        }
    }

}

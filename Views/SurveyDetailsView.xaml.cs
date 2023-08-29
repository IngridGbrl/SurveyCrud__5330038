using _5330038.Data;
using _5330038.Models;

namespace _5330038.Views;



[QueryProperty("Item","Item")]
public partial class SurveyDetailsView : ContentPage
{
    Survey item;

    private readonly string[] teams =
      {
        "Alianza Lima",
        "América",
        "Boca Juniors",
        "Caracas FC",
        "Colo-Colo",
        "Peñarol",
        "Real Madrid",
        "Saprissa"
    };
    public Survey Item
    {
      
        get => BindingContext as Survey;
        set => BindingContext = value;
    }

 
    SurveyDatabase database;


    public SurveyDetailsView(SurveyDatabase surveyDatabase)
	{
		InitializeComponent();
        database = surveyDatabase;

	}

    async void Save_Clicked(object sender, EventArgs e)
    {
      
        if (string.IsNullOrWhiteSpace(Item.Name))
        {
            await DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
            return;
        }
        Item.FavoriteTeam = FavoriteTeamLabel.Text;
        await database.SaveItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

   async void Delete_Clicked(object sender, EventArgs e)
    {
    
        if (Item.Id == 0)
            return;
        await database.DeleteItemAsync(Item);
        await Shell.Current.GoToAsync("..");

    }
    async void Cancel_Clicked(object sender, EventArgs e)
    {
        //con este metodo nada más se llama a la siguiente funcion
        await Shell.Current.GoToAsync("..");
    }

    private async void FavoriteTeam_ClickedAsync(object sender, EventArgs e)
    {
        var favoriteteam = await DisplayActionSheet(Literals.FavoriteTeamTitle, null, null, teams);
        if (!string.IsNullOrWhiteSpace(favoriteteam))
        {
            FavoriteTeamLabel.Text = favoriteteam;
        }
    }
}
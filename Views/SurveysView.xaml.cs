using _5330038.Data;
using _5330038.Models;
using System.Collections.ObjectModel;

namespace _5330038.Views;

public partial class SurveysView : ContentPage
{
	//se crea la propiedad que nos permitira acceder a la bd
	SurveyDatabase database;

    public ObservableCollection<Survey> Items { get; set; } = new();

    public SurveysView(SurveyDatabase todoItemDatabase)
	{
		InitializeComponent();
		database = todoItemDatabase;
		BindingContext = this;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{  
        base.OnNavigatedTo(args);

		var items = await database.GetTodoItemsAsync();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            
            Items.Clear();
		foreach (var item in items)
			Items.Add(item);
	
		});
	}
    async void ItemAdded_Clicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync(nameof(SurveyDetailsView), true, new Dictionary<string, object>
		{
			["Item"] = new Survey()
		});
    }
	private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    
        if (e.CurrentSelection.FirstOrDefault() is not Survey item)
			return;

		await Shell.Current.GoToAsync(nameof(SurveyDetailsView), true, new Dictionary<string, object>{
			["Item"] = item
		});
    }
}
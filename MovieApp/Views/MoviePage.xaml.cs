namespace MovieApp.Views;

using MovieApp.Models;
using System.Collections.ObjectModel;
using System.IO;

public partial class MoviePage : ContentPage
{
    ObservableCollection<Models.Movie> movies { get; set; }
    Data.MovieDatabase database = new Data.MovieDatabase();
    public MoviePage()
	{
		InitializeComponent();
        movies = new ObservableCollection<Models.Movie>();
        
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(async () =>
        {
            movies = new ObservableCollection<Models.Movie>(
                await database.GetMoviesAsync()
                );
            MainThread.BeginInvokeOnMainThread(() => colview.ItemsSource = movies);
        });
    }

    async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.AddNewMoviePage {BindingContext = new Movie()});
    }

     async void colview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        

        Console.WriteLine(e.CurrentSelection as Movie);
        await Navigation.PushAsync(new Views.AddNewMoviePage { BindingContext = e.CurrentSelection as Movie});

    }
}
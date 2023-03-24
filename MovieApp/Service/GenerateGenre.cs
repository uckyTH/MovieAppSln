
using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MovieApp.Service
{
    public class GenerateGenre
    {
        public ObservableCollection<Movie> Genres { get; set; }
        public void Generate()
        {
            Genres = new ObservableCollection<Movie>();
            Genres.Add(new Movie { Genre = "Action" });
            Genres.Add(new Movie { Genre= "Comedy" });
            Genres.Add(new Movie { Genre = "Drama" });
            Genres.Add(new Movie { Genre = "Fantasy" });
            Genres.Add(new Movie { Genre = "Horror" });
            Genres.Add(new Movie { Genre = "Mystery" });
            Genres.Add(new Movie { Genre = "Romance" });
            Genres.Add(new Movie { Genre = "Thriller" });

        }
        /*<Picker.ItemsSource>
                <x:Array Type="{x:Type x:String}">
                    <x:String>Action</x:String>
                    <x:String>Comedy</x:String>
                    <x:String>Drama</x:String>
                    <x:String>Fantasy</x:String>
                    <x:String>Horror</x:String>
                    <x:String>Mystery</x:String>
                    <x:String>Romance</x:String>
                    <x:String>Thriller</x:String>
                </x:Array>
            </Picker.ItemsSource>*/
    }
}

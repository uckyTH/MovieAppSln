using MovieApp.Models;
using System.Collections.ObjectModel;

namespace MovieApp.Views;

public partial class AddNewMoviePage : ContentPage
{
    Service.GenerateGenre gn = new Service.GenerateGenre();
    Data.MovieDatabase db = new Data.MovieDatabase();
    List<Byte[]> imgByteArray = null;

    ObservableCollection<ImageSource> imgList = null;
    bool isPicker = false;
    public AddNewMoviePage()
	{
		InitializeComponent();
        gn.Generate();
        picker.BindingContext = gn;
    }
    private async void Save_Clicked(object sender, EventArgs e)
    {
        if (picker.SelectedIndex != -1 && EntName.Text !=null)
        {
            HasImgFolder();
            String mainDir = Path.Combine(FileSystem.Current.AppDataDirectory, "Images");

            String imgPath = null;
            foreach (var imgbyte in imgByteArray)
            {
                var photoName = Guid.NewGuid().ToString();
                imgPath = Path.Combine(mainDir, $"{photoName}.png");
                await SaveImg(imgPath, imgbyte); 

                //await DisplayAlert("Image", $"{photoName}", "ok");
            }

           await DisplayAlert("Name & Image", $"{imgPath}", "ok");
            var movie = (Movie)BindingContext;
            movie.Name = EntName.Text;
            movie.Genre = picker.Items[picker.SelectedIndex];
            movie.Description = EditDes.Text;
            movie.ImgPath = imgPath;
            await db.SaveMoiveAsync(movie);

            await Navigation.PopAsync();
        }
        await DisplayAlert("Name & Image", "Enter Name and Genre Movie", "ok");


    }

    public async Task SaveImg(string filePath, byte[] imgBytes)
    {
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await stream.WriteAsync(imgBytes, 0, imgBytes.Length);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (isPicker == false)
        {
            imgByteArray = new List<byte[]>();
            imgList = new ObservableCollection<ImageSource>();
            cvImages.ItemsSource = imgList;

        }
    }
    protected override void OnDisappearing()
    {
        isPicker = false;
        base.OnDisappearing();
    }

    async void Image_Clicked(object sender, EventArgs e)
    {
        var pickResult = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Pick Img"
        });
        if (pickResult != null)
        {
            var stream = await pickResult.OpenReadAsync();
            //pickImg.Source = ImageSource.FromStream(() => Stream);

            imgList.Add(ImageSource.FromStream(() => stream));

            var stream1 = await pickResult.OpenReadAsync();
            imgByteArray.Add(GetFileBytes(stream1)); ;
            await DisplayAlert("Image", "Compreted ", "ok");


        }
        isPicker = true;
    }

    private void HasImgFolder()
    {
        var imgFolderPath = Path.Combine(FileSystem.Current.AppDataDirectory, "Images");
        if (!Directory.Exists(imgFolderPath))
        {
            Directory.CreateDirectory(imgFolderPath);
        }
    }

    public byte[] GetFileBytes(Stream input)
    {
        using (MemoryStream ms = new MemoryStream()) { input.CopyTo(ms); return ms.ToArray(); }
    }
}
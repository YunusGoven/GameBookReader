using GameBook.Storage;

namespace GameBookViewModel.ViewModels
{
    public class ViewModelLocator
    {

        public StoryViewModel StoryVm { get; } = new StoryViewModel( null, new StorageManager());


    }
}
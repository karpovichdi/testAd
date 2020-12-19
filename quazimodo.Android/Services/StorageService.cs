using Android.Content;
using quazimodo.Droid.Services;
using quazimodo.Services.Interfaces;
using quazimodo.Utilities.Constants;
using Xamarin.Forms;

[assembly: Dependency(typeof(StorageService))]
namespace quazimodo.Droid.Services
{
    public class StorageService : IStorageService
    {
        public void SaveCount(int count)
        {
            var sharedPref = MainActivity.Instance.GetPreferences(FileCreationMode.Private);
            var editor = sharedPref?.Edit();

            if (editor != null)
            {
                if (count > AdConstants.ClicksCountBeforeAd)
                {
                    count = AdConstants.ClicksCountBeforeAd;
                }

                editor.PutInt(AdConstants.CountOfPlayedSound, count);
                editor.Apply();
            }
        }

        public int GetCount()
        {
            var sharedPref = MainActivity.Instance.GetPreferences(FileCreationMode.Private);
            if (sharedPref != null)
            {
                return sharedPref.GetInt(AdConstants.CountOfPlayedSound, 0);
            }

            return 0;
        }
    }
}
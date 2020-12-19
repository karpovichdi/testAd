using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using quazimodo.Models;
using quazimodo.Utilities.Constants;

namespace quazimodo.Services
{
    public class FirebaseService
    {
        private FirebaseClient _firebaseClient;

        public async Task Initialize()
        {
            try
            {
                _firebaseClient = new FirebaseClient(FirebaseConstants.DatabaseUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task<List<MyApp>> GetListApps()
        {
            var locale = FirebaseConstants.LocalizationEn;

            if (App.TwoLetterIsoLanguageName.Contains(FirebaseConstants.LocalizationRu))
            {
                locale = FirebaseConstants.LocalizationRu;
            }
            
            return (await _firebaseClient
                .Child(FirebaseConstants.MyAppsEndpoint).Child(locale)
                .OnceAsync<MyApp>()).Select(item => new MyApp
            {
                Name = item.Object.Name,
                Description = item.Object.Description,
                ImageUrl = item.Object.ImageUrl,
                DownloadUrl = item.Object.DownloadUrl
            }).ToList();
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MovieAPI.Models.Settings;
using MovieAPI.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public class SupabaseService:ISupabaseService
    {
        private readonly Supabase.Client _client;
        private readonly string _bucketName;

        public SupabaseService(IOptions<SupabaseSetting> options)
        {
            var settings = options.Value;
            _bucketName = settings.BucketName;

            var supabaseOptions = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = false
            };

            _client=new Supabase.Client(settings.Url,settings.Key, supabaseOptions) ;
            _client.InitializeAsync().Wait();

        }

        public async Task<string> UploadPosterAsync(IFormFile file,string movieName,int year)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName=$"movie-{movieName}-{year}{extension}";
            var filePath = $"posters/{fileName}";

            using var stream = file.OpenReadStream();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            await _client.Storage
                .From(_bucketName)
                .Upload(fileBytes, filePath, new Supabase.Storage.FileOptions
                {
                    ContentType = file.ContentType,
                    Upsert = true   
                });

            return _client.Storage
                .From(_bucketName)
                .GetPublicUrl(filePath);
        }
    }
}

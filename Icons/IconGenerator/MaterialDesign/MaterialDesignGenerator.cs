
using Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tabler.Docs.Icons;
using TabloRazor;

namespace IconGenerator.MaterialDesign
{
    public static class MaterialDesignGenerator
    {

        public static async Task<IEnumerable<GeneratedIcon>> GenerateIcons()
        {
            var failed = new System.Collections.Concurrent.ConcurrentBag<string>();
            var icons = new System.Collections.Concurrent.ConcurrentBag<GeneratedIcon>();
            var url = "https://raw.githubusercontent.com/Templarian/MaterialDesign-SVG/master/meta.json";
            var client = new HttpClient();

            var metajson = await client.GetStringAsync(url);
            var iconsMeta = JsonSerializer.Deserialize<List<MaterialDesignIcon>>(metajson);
            //Build the policy
            var retryPolicy = Policy.Handle<IOException>()
                .WaitAndRetry(retryCount: 3, sleepDurationProvider: _ => TimeSpan.FromSeconds(1));


            Parallel.ForEach(iconsMeta, iconMeta =>
            {
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                    (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    };

                var client2 = new HttpClient(handler);

                try
                {
                    retryPolicy.Execute(() =>
                    {
                        var icon = new GeneratedIcon
                        {
                            Name = iconMeta.Name,
                            Author = iconMeta.Author,
                            Tags = iconMeta.Tags
                        };

                        var iconUrl = $"https://unpkg.com/@mdi/svg/svg/{iconMeta.Name}.svg";
                        var svgContent = client2.GetStringAsync(iconUrl).GetAwaiter().GetResult();
                        icon.IconType = new MDIcon(Utilities.ExtractIconElements(svgContent));
                        icons.Add(icon);
                        Console.WriteLine($"Icon '{icon.Name}' added");
                    });

                }
                catch (Exception ex)
                {
                    var iconUrl = $"https://unpkg.com/@mdi/svg/svg/{iconMeta.Name}.svg";
                    Console.WriteLine($"Icon '{iconMeta?.Name}' couldn't download: {iconUrl}");
                    failed.Add(iconUrl);
                }
            });

            Utilities.GenerateIconsFile("MaterialDesignIcons", icons);

            return icons;
        }


    }

    public class MaterialDesignIcon
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("codepoint")]
        public string CodePoint { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("aliases")]
        public List<string> Aliases { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

}

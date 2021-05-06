using Geometrics.Basic;
using Geometrics.Object;

using System;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RayTracing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Renderer renderer = new Renderer();

            while (true)
            {
                Scene test = FromJson();

                using MemoryStream ms = new MemoryStream();

                renderer.Render(test).Save(ms, ImageFormat.Png);

                string base64string = Convert.ToBase64String(ms.ToArray());

                using var client = new HttpClient();

                var response = await client.GetStringAsync("http://45.83.107.107:1337/results");
                int id = JsonDocument.Parse(response).RootElement[0].GetProperty("id").GetInt32();

                await client.DeleteAsync($"http://45.83.107.107:1337/results/{id}");

                var jsonObject = new { image = base64string };

                JsonContent content = JsonContent.Create(jsonObject, jsonObject.GetType());

                await client.PostAsync("http://45.83.107.107:1337/results", content);

                Thread.Sleep(10000);
            }
        }

        private static Scene FromJson()
        {
            using WebClient wc = new WebClient();
            string jsonString = wc.DownloadString("http://45.83.107.107:1337/data");

            using JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
            var rootElement = jsonDocument.RootElement[0];

            var sphereJson = rootElement.GetProperty("sphere");

            Vector spherePosition = FromJsonPosition(sphereJson.GetProperty("position"));
            Color sphereColor = FromJsonColor(sphereJson.GetProperty("color"));
            double sphereRadius = sphereJson.GetProperty("radius").GetDouble();
            Material sphereMaterial = FromJsonMaterial(sphereJson.GetProperty("material"), sphereColor);

            Sphere sphere = new Sphere(spherePosition, sphereRadius, sphereMaterial);


            var lightPositionJson = rootElement.GetProperty("lightPosition");
            var lightColorJson = rootElement.GetProperty("lightColor");

            Vector lightPosition = FromJsonPosition(lightPositionJson);
            Color lightColor = FromJsonColor(lightColorJson);

            Light light = new Light(lightColor, lightPosition);


            var cameraPositionJson = rootElement.GetProperty("cameraPosition");
            var focusJson = rootElement.GetProperty("focus");
            var fov = rootElement.GetProperty("fov").GetDouble();
            var resolutionJson = rootElement.GetProperty("resolution");

            Vector cameraPosition = FromJsonPosition(cameraPositionJson);
            Vector cameraFocus = FromJsonPosition(focusJson);

            int cameraWidth = resolutionJson.GetProperty("width").GetInt32();
            int cameraHeight = resolutionJson.GetProperty("height").GetInt32();

            Camera camera = new Camera(cameraPosition, cameraFocus, fov, cameraWidth, cameraHeight);


            return new Scene(new Sphere[] { sphere }, new Light[] { light }, camera);
        }

        private static Color FromJsonColor(JsonElement jsonColor)
        {
            return new Color(jsonColor.GetProperty("red").GetDouble(), jsonColor.GetProperty("green").GetDouble(), jsonColor.GetProperty("blue").GetDouble());
        }

        private static Vector FromJsonPosition(JsonElement jsonPosition)
        {
            return new Vector(jsonPosition.GetProperty("x").GetDouble(), jsonPosition.GetProperty("y").GetDouble(), jsonPosition.GetProperty("z").GetDouble());
        }

        private static Material FromJsonMaterial(JsonElement material, Color color)
        {
            string materialName = material.GetString();

            return materialName switch
            {
                "matt" => Material.Matt(color),
                "shiny" => Material.Shiny(color),
                _ => throw new JsonException("Material not found")
            };
        }
    }
}

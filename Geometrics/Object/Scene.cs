using Geometrics.Basic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrics.Object
{
    public record Scene
    {
        public Sphere[] Objects { get; init; }
        public Light[] Lights { get; init; }
        public Camera Camera { get; init; }

        public Scene(Sphere[] spheres, Light[] lights, Camera camera)
        {
            Objects = spheres;
            Lights = lights;
            Camera = camera;
        }

        public IEnumerable<Intersection> Intersections(Ray ray)
        {
            foreach (Sphere sphere in Objects)
            {
                yield return sphere.Intersects(ray);
            }
        }

        public static Scene TestScene()
        {
            Light[] lights = { new Light(new Color(1, 1, 1), new Vector(-20, 20, -10)) };
            Sphere[] spheres = { new Sphere(new Vector(0, 0, -55), 10, Material.Shiny(new Color(1, 0.1, 0.0)))/*, new Sphere(new Vector(0, 0, -30), 10, Material.Shiny(new Color(.7, .7, .7))) */};
            Camera camera = new Camera(new Vector(0, 0, 0), new Vector(0, 0, -51), 90, 1920, 1080);

            return new Scene(spheres, lights, camera);
        }
    }
}

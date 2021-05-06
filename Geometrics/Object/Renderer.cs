using Geometrics.Basic;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Geometrics.Object
{
    public class Renderer
    {
        public Bitmap Render(Scene scene)
        {
            Camera camera = scene.Camera;

            uint[] rgb = new uint[camera.Width * camera.Height];

            Parallel.For(0, camera.Height, y =>
            {
                int strait = y * camera.Width;
                for (int x = 0; x < camera.Width; ++x)
                {
                    rgb[strait + x] = TraceRay(scene, camera.CameraRay(x, y)).ToUint();
                }
            });

            var gchPixels = GCHandle.Alloc(rgb, GCHandleType.Pinned);

            var bitmap = new Bitmap(camera.Width, camera.Height, camera.Width * sizeof(uint),
                        PixelFormat.Format32bppPArgb,
                        gchPixels.AddrOfPinnedObject());

            gchPixels.Free();

            return bitmap;
        }

        private double TestRay(Scene scene, Ray ray)
        {
            Intersection minIntersection = scene.Intersections(ray).Min();

            if (minIntersection is null)
            {
                return 0.0;
            }
            else
            {
                return minIntersection.Distance;
            }
        }

        private Color TraceRay(Scene scene, Ray ray)
        {
            Intersection minIntersection = scene.Intersections(ray).Min();

            if (minIntersection is null)
            {
                return new Color
                {
                    Red = 0.0,
                    Green = 0.0,
                    Blue = 0.0
                };
            }
            else
            {
                return NaturalSurfaceColor(minIntersection, scene);
            }
        }

        private Color NaturalSurfaceColor(Intersection intersection, Scene scene)
        {
            Color returnColor = new Color { Red = 0.0, Green = 0.0, Blue = 0.0 };

            foreach (Light light in scene.Lights)
            {
                double lightDistance = (light.Position - intersection.Position).Magnitude;
                var lightDirection = new UnitVector(light.Position - intersection.Position);

                // TestRay returns the distance of the nearest intersection on the ray
                // zero means no intersection
                double nearestIntersection = TestRay(scene, new Ray(intersection.Position, lightDirection));

                bool isShadowed = !(lightDistance < nearestIntersection || 
                    nearestIntersection == 0);

                if (!isShadowed)
                {
                    Material material = intersection.Object.Material;

                    // ambient color calculation
                    Color ambientColor = material.Ambient * light.Ambient;

                    // diffuse color calculation
                    UnitVector surfaceNormal = intersection.Object.Normal(intersection.Position);
                    double diffuse = Math.Max(surfaceNormal.Dot(lightDirection), 0.0);
                    Color diffuseColor = (material.Diffuse * light.Diffuse).Scale(diffuse);

                    // specular color calculation
                    double specular = Math.Pow(Math.Max(lightDirection.Dot(surfaceNormal), 0.0), material.Roughness);
                    Color specularColor = (material.Specular * light.Specular)
                        .Scale(specular);

                    returnColor += (ambientColor + diffuseColor + specularColor);
                }
            }

            return returnColor;
        }

        private Color ReflectionColor(Intersection intersection, Scene scene, int depth)
        {
            return TraceRay(scene, new Ray(intersection.Position, intersection.Reflection)).Scale(intersection.Object.Material.Shininess);
        }
    }
}

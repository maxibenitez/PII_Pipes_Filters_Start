using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using TwitterUCU;
using CognitiveCoreUCU;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ejercicio 1, 2 y 3
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"luke.jpg");

            IFilter desaturar = new FilterGreyscale();
            IFilter negativo = new FilterNegative();

            IPipe filtro = new PipeNull();

            IPipe filtroNegativo = new PipeSerial(negativo, filtro);
            IPicture imagenFiltroNegativo = filtroNegativo.Send(picture);
            provider.SavePicture(imagenFiltroNegativo, @"lukeToFiltroNegativo.jpg");
            TwitterImage posteoNegativo = new TwitterImage();
            posteoNegativo.PublishToTwitter("No entiendo nada", @"lukeToFiltroNegativo.jpg");
            
            IPipe filtroDesaturar = new PipeSerial(desaturar, filtroNegativo);
            IPicture imagenFiltroDesaturar = filtroDesaturar.Send(picture);
            provider.SavePicture(imagenFiltroDesaturar, @"lukeToFiltroDesaturar.jpg");
            TwitterImage posteoDesaturar = new TwitterImage();
            posteoDesaturar.PublishToTwitter("No entiendo nada 2.0", @"lukeToFiltroDesaturar.jpg");

            //Ejercicio 4
            IFilter filtrofacial = new FilterConditional();
            IPipe filtro1 = new PipeNull();

            IPipe reconocimiento = new PipeSerial(filtrofacial, filtro1);
            IPicture imagenFiltroReconocimiento = reconocimiento.Send(picture);
            provider.SavePicture(imagenFiltroReconocimiento, @"lukeToFiltroReconocimiento.jpg");
            TwitterImage posteoFacial = new TwitterImage();
            posteoFacial.PublishToTwitter("No entiendo nada 3.0", @"lukeToFiltroReconocimiento.jpg");
        }
    }
}

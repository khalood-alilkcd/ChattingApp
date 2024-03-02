using Imagekit.Sdk;
using System.Runtime.CompilerServices;

namespace ChattingApp.Extensions
{
    public static class ImageKitExt
    {
        public static void AddAuthenticationForImageKit(this IServiceCollection services)
        {
            services.AddSingleton(new ImagekitClient(
                urlEndPoint: "https://ik.imagekit.io/gveuvk8oi/",
                publicKey: "public_X2jsOowkR0fnx2+54mc/4TMjVGU=",
                privateKey: "private_KqEVgcggB05W/kfRNWsmJcVZTXc="
                ));
            
        }
    }
}

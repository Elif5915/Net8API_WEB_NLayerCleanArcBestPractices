using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace App_Services.ExceptionHandlers; // bu benim kendi kritik hatalarımda action fırlatmak istediğim için kullanacağım sınıfım. örneğin hata oluşunca response dönmeden sms mail göndermek istemek.
public class CriticalExceptionHandler(): IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        if (exception is CriticalException)
        {
            Console.WriteLine("İlgili hata için sms gönderildi.");
        }

        //BUSİNESS LOGİC!
        return ValueTask.FromResult(false); // geriye false dönersem ise şu anlama geliyor ben bu hatayı aldım gerekli sms mailimi gönderdim, bundan sonra BU HATAMI BİR SONRAKİ HANDLERIMA AKTARMAK İSTİYORUM veya ilgili middleware gitsin.

        // return ValueTask.FromResult(true); // bunun anlamı bundan sonra bu hatayı ben ele alacağım bundan sonra diğer handler ım artık aktif olarak görev almayacak.Ve bundan sonrada ben kendi result ımı belirlemem gerek 

    }
}

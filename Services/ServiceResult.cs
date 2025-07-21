using System.Net;

namespace App_Services;
public class ServiceResult<T> //result poattern implemantasyonudur.
{
    public T? Data { get; set; } //başarılı olduğunda data dolacak

    public List<string>? ErrorMessage { get; set; } //başarısız/hata olunca hata  mesajı dolacak

    public bool IsSuccess =>  ErrorMessage == null || ErrorMessage.Count == 0; //sadece geti olan property oldu

    public bool IsFail => !IsSuccess;

    public HttpStatusCode StatusCode { get; set; }

    //aşağıdakilere ne denir static factory method olarak adlandırılır, aşağıdaki methodlarla new lemeyi kontrol altına aldık.
    public static ServiceResult<T> Success(T data, HttpStatusCode code = HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            StatusCode = code
        };
    }
    public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode code = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = errorMessage,
            StatusCode = code
        };
    }

    public static ServiceResult<T> Fail (string errorMessage, HttpStatusCode code = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = [errorMessage],
            StatusCode = code
        };
    }
}

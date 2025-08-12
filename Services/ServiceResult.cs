using System.Net;
using System.Text.Json.Serialization;

namespace App_Services;
public class ServiceResult<T> //result pattern implemantasyonudur.
{
    public T? Data { get; set; } //başarılı olduğunda data dolacak

    public List<string>? ErrorMessage { get; set; } //başarısız/hata olunca hata  mesajı dolacak

    [JsonIgnore]
    public bool IsSuccess =>  ErrorMessage == null || ErrorMessage.Count == 0; //sadece geti olan property oldu

    [JsonIgnore]
    public bool IsFail => !IsSuccess;

    [JsonIgnore]
    public HttpStatusCode StatusCode { get; set; }

    [JsonIgnore]
    public string? UrlAsCreated { get; set; }

    //aşağıdakilere ne denir static factory method olarak adlandırılır, aşağıdaki methodlarla new lemeyi kontrol altına aldık.
    public static ServiceResult<T> Success(T data, HttpStatusCode code = HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            StatusCode = code
        };
    }

    public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            StatusCode = HttpStatusCode.Created,
            UrlAsCreated = urlAsCreated
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

public class ServiceResult
{
    public List<string>? ErrorMessage { get; set; }

    [JsonIgnore] // bu atribute ile serialiaze etmemesini sağlıyoruz
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;

    [JsonIgnore]
    public bool IsFail => !IsSuccess;

    [JsonIgnore]
    public HttpStatusCode StatusCode { get; set; }

    
    public static ServiceResult Success(HttpStatusCode code = HttpStatusCode.OK)
    {
        return new ServiceResult()
        {
            StatusCode = code
        };
    }
    public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode code = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessage = errorMessage,
            StatusCode = code
        };
    }

    public static ServiceResult Fail(string errorMessage, HttpStatusCode code = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessage = [errorMessage],
            StatusCode = code
        };
    }
}
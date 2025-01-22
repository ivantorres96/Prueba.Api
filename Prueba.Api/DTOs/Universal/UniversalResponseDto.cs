namespace Prueba.Api.DTOs.Universal
{
    public class UniversalResponseDto<T>
    {
        public string? Code { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }
    }
}

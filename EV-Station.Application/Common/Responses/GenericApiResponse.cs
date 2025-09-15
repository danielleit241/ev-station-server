namespace EV_Station.Application.Common.Responses
{
    public class GenericApiResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static GenericApiResponse<T> SuccessResponse(T data, string message = "Success")
        {
            return new GenericApiResponse<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        public static GenericApiResponse<T> FailResponse(string message)
        {
            return new GenericApiResponse<T>
            {
                IsSuccess = false,
                Message = message,
                Data = default
            };
        }
    }
}

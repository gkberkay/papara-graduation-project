namespace DigiSopAPI.Base.Response
{
    public partial class ApiResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime ServerDate { get; set; } = DateTime.UtcNow;
        public Guid ReferenceNumber { get; set; } = Guid.NewGuid();

        public ApiResponse(string message = null, bool isSuccess = false)
        {
            if (string.IsNullOrEmpty(message))
            {
                IsSuccess = true;
            }
            else
            {
                IsSuccess = isSuccess;
                Message = message;
            }
        }
    }

    public partial class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime ServerDate { get; set; } = DateTime.UtcNow;
        public Guid ReferenceNumber { get; set; } = Guid.NewGuid();

        public ApiResponse(T data, string message = "Success")
        {
            IsSuccess = true;
            Data = data;
            Message = message;
        }

        public ApiResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Data = default;
            Message = isSuccess ? "Success" : "Error";
        }

        public ApiResponse(string error)
        {
            IsSuccess = true;
            Data = default;
            Message = error;
        }

    }
}
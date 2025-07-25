namespace gumfa.services.AuthAPI.Models.DTO
{
    public class APIResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}

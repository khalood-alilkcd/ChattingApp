namespace ChattingApp.Error_Model
{
    public class ApiExciptionResponse : ApiReposnse
    {
        public string? Detail { get; set; }
        public ApiExciptionResponse(
            int statusCode, 
            string? message = null, 
            string? detail = null
            ):base(statusCode, message)
        {
            Detail=detail;
        }


    }
}

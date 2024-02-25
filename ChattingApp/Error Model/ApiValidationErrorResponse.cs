namespace ChattingApp.Error_Model
{
    public class ApiValidationErrorResponse : ApiReposnse
    {

        public IEnumerable<string>? Errors { get; set; }
        public ApiValidationErrorResponse():base(400)
        {
           
        }
        
    }
}

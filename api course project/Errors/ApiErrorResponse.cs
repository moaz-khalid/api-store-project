namespace api_course_project.Errors
{
    public class ApiErrorResponse
    {


        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public ApiErrorResponse(int statusCode,string? meessage = null)
        {
            StatusCode = statusCode;

            Message = meessage ?? GetDefaultMessageForStatusCode(statusCode);


        }

        private string? GetDefaultMessageForStatusCode (int statusCode)
        {



            var message =  statusCode switch 
            {
            400 => "a bad request, you have made",
            401 => "Authorized ,you are not",
            404 => "Resource was not found",
            500 => "server error",
            _=>null
            };

            return message;
        }




    }
}

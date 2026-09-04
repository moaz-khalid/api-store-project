namespace api_course_project.Errors
{
    public class ApiValidationErrorResponse :ApiErrorResponse
    {
        public IEnumerable<string> Errors { get; set; }=new List<string>();

        public ApiValidationErrorResponse() :base(400)
        {
            
        }

    }
}

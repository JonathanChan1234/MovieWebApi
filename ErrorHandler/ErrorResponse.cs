namespace NetApi.ErrorHandler
{
    public class ErrorResponse
    {
        public ErrorResponse(int errorno, string message)
        {
            this.errorno = errorno;
            this.message = message;
            this.success = 0;
        }

        public ErrorResponse(int success, int errorno, string message)
        {
            this.success = success;
            this.errorno = errorno;
            this.message = message;
        }
        public int success = 0;
        public int errorno { get; set; }
        public string message { get; set; }
    }
}
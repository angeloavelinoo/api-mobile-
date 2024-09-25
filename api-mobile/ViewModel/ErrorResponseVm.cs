namespace api_mobile.ViewModel
{
    public class ErrorResponseVm
    {
        public string TraceId { get; private set; }
        public List<ErrorDetailsVm> Errors { get; private set; }

        public ErrorResponseVm()
        {
            TraceId = Guid.NewGuid().ToString();
            Errors = new List<ErrorDetailsVm>();
        }

        public ErrorResponseVm(string logref, string message)
        {
            TraceId = Guid.NewGuid().ToString();
            Errors = new List<ErrorDetailsVm>();
            AddError(logref, message);
        }


        public class ErrorDetailsVm
        {
            public string Id { get; private set; }
            public string Logref { get; private set; }

            public string Message { get; private set; }

            public ErrorDetailsVm(string logref, string message)
            {
                Id = Guid.NewGuid().ToString();
                Logref = logref;
                Message = message;
            }
        }


        public void AddError(string logref, string message)
        {
            Errors.Add(new ErrorDetailsVm(logref, message));
        }
    }
}
namespace MemoGlobal_BackendHomeTest.Models.Response
{
    public abstract  class BasicResponse
    {
        public bool IsSuccess{ get; set; }
        public string ResponseMessage { get; set; }
        
        public BasicResponse()
        {
                
        }

        public BasicResponse(bool isSuccess, string responseMessage)
        {
            IsSuccess = isSuccess;
            ResponseMessage = responseMessage;

        }
    }
}

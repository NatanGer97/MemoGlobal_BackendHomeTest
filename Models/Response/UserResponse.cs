using MemoGlobal_BackendHomeTest.Models.Entity;

namespace MemoGlobal_BackendHomeTest.Models.Response
{
    public class UserResponse : BasicResponse
    {
        public User? User { get; set; }

        private UserResponse(bool isSuccess, string responseMessage, User? user) : base(isSuccess, responseMessage)
        {
            User = user;
        }


        // success response
        public UserResponse(User user)
            :this(true, "", user)
        {

        }

        // bad resonse
        public UserResponse(string responseMessage)
            : this(false, responseMessage, null)
        {

        }
    }
}

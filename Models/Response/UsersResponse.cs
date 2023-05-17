using MemoGlobal_BackendHomeTest.Models.Entity;

namespace MemoGlobal_BackendHomeTest.Models.Response
{
    public class UsersResponse :  BasicResponse
    {
        public List<User> Users { get; set; }

        private UsersResponse(bool isSuccess, string responseMessage, List<User> users) : base(isSuccess, responseMessage)
        {
            Users = users;
        }


        // success response
        public UsersResponse(List<User> users)
            : this(true, "", users)
        {

        }

        // bad resonse
        public UsersResponse(string responseMessage)
            : this(false, responseMessage, null)
        {

        }
    }
}

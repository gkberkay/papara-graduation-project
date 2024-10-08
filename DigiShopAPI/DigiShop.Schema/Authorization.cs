﻿using DigiShop.Base.Schema;

namespace DigiShop.Schema
{
    public class AuthorizationRequest : BaseRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
    public class AuthorizationResponse
    {
        public DateTime ExpireTime { get; set; }
        public string AccessToken { get; set; }
        public string UserName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dsp.Web
{
    [Authorize]
    public class TestController : ApiController
    {
        [Route("api/Test")]
        public int Get()
        {
            return 1;
        }
    }
}
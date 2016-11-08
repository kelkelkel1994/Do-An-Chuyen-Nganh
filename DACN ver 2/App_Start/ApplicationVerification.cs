using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DACN_ver_2.App_Start
{
    public class ApplicationVerification
    {
        public static void Check()
        {
            if (WebConfigurationManager.AppSettings["RecaptchaPublicKey"].ToUpper() == "CHANGEME") { throw new Exception("Web Config is missing a Recaptcha Public Key"); }
            if (WebConfigurationManager.AppSettings["RecaptchaPrivateKey"].ToUpper() == "CHANGEME") { throw new Exception("Web Config is missing a Recaptcha Private Key"); }
        }
    }
}
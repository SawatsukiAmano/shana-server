using BLL;

namespace API.Controllers
{
    public class AuthController : BaseController
    {

        [HttpGet]
        public string TestConf(string str)
        {
            return Appsettings.ReadNode(str);
        }


        [HttpPost]
        public ActionResult Login()
        {
            var one = new BaseBLL<SysUser>().FirstOrDefaultSync(x => x.UserName == "1").Result;
            if(one == null) Content("false");
            if (one != null)
            {

            }
            return Content("false");
        }


        string GenerateJwtToken(string id, string name, string role)
        {

            // 1. 设置加密算法,生成签名证书,钥长度至少为16位
            string encrypt = SecurityAlgorithms.HmacSha256;
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Appsettings.ReadNode("JWT:ScrectKey"))); //私钥
            SigningCredentials signing = new SigningCredentials(key, encrypt);

            // 3. 构造Claims
            Claim[] claims = new[]
             {
            new Claim(JwtRegisteredClaimNames.NameId,id),
            new Claim(JwtRegisteredClaimNames.Name, name),
            new Claim(ClaimTypes.Role, role),
            //new Claim(JwtRegisteredClaimNames.Sub, "client_brower"), //jwt所面向的用户
            };

            string issuer = "https://newreport.top";//发行
            string audience = "https://shana.newreport.top";//接收
            DateTime notBefore = DateTime.Now;
            DateTime expires = notBefore.AddHours(9);
            JwtSecurityToken jwtToken = new JwtSecurityToken(issuer, audience, claims, notBefore, expires, signing);

            string strToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return strToken;
        }
    }
}

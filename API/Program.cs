

using BLL;
using IBLL;
using DAL;
using IDAL;
using DBUtility;

var builder = WebApplication.CreateBuilder(args);

#region 服务注册
//读取appsettings配置
builder.Services.AddSingleton(new Appsettings(builder.Configuration));
{
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//获取请求信息，即访问者ip
    builder.Services.AddAutoMapper(typeof(MyProfile));//设置模型映射
    builder.Services.AddScoped<EFSqlContext>();
    builder.Services.AddScoped<IBLLSysUser, BLLSysUser>();
    builder.Services.AddScoped<IBLLSysUser, BLLSysUserA1>();
    builder.Services.AddScoped<IDALSysUser, DALSysUser>();
    builder.Services.AddScoped<IDALSysUser, DALSysUserA1>();
}
//controller 自动反序列化
builder.Services.AddControllers();
//返回
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    //大驼峰
    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    //小驼峰
    //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    //蛇形
    options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

});
builder.Services.AddEndpointsApiExplorer();//swagger 导出
builder.Services.AddSwaggerGen(options => {
    var path = Path.Combine(AppContext.BaseDirectory, "API.xml");
    options.IncludeXmlComments(path, true);
    options.OrderActionsBy(s => s.RelativePath);
        
});//swagger 生成


//JWT 鉴权
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
         options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 ValidateLifetime = true,
                 //ValidAudience = null,
                 //ValidIssuer=null,
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Appsettings.ReadNode("JWT:ScrectKey"))),
             };
         }
         );
new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    //options.LowercaseQueryStrings = true;
});

//HTTP 日志记录中间件
builder.Logging.AddDebug();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Warning);


//路由前缀
//builder.Services.AddMvc(opt =>
//{
//    opt.Conventions.Insert(0, new RouteConvention(new RouteAttribute("xxxx")));
//});

#endregion


var app = builder.Build();
app.UseRouting();

app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpLogging();//start the http logs
app.MapControllers();

app.Run();
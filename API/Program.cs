

using BLL;
using IBLL;
using DAL;
using IDAL;
using API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using DBUtility;
using Microsoft.Extensions.Logging;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

#region 服务注册
//读取appsettings配置
builder.Services.AddSingleton(new Appsettings(builder.Configuration));
{
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//获取请求信息，即访问者ip
    builder.Services.AddAutoMapper(typeof(MyProfile));//设置模型映射
    builder.Services.AddScoped<EFSqlContext>();
    builder.Services.AddScoped<IBLLSysUser, BLLSysUser>();
    builder.Services.AddScoped<IDALSysUser, DALSysUser>();
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
builder.Services.AddSwaggerGen();//swagger 生成


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
var log = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
log.LogDebug("调试日志");
log.Log(LogLevel.Information, "sadas");
log.LogInformation("信息日志");
log.LogWarning("警日志");
log.LogCritical("严重日志");
log.LogError("错误日志");


//builder.Services.AddHttpLogging(logging =>
//{
//    logging.LoggingFields = HttpLoggingFields.All;
//    logging.RequestHeaders.Add("sec-ch-ua");
//    logging.ResponseHeaders.Add("MyResponseHeader");
//    logging.MediaTypeOptions.AddText("application/javascript");
//    logging.RequestBodyLogLimit = 4096;
//    logging.ResponseBodyLogLimit = 4096;
//});


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
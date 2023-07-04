var builder = WebApplication.CreateBuilder(args);

#region 服务注册
builder.Services.AddControllers().AddNewtonsoftJson();//controller 自动反序列化
builder.Services.AddEndpointsApiExplorer();//swagger 导出
builder.Services.AddSwaggerGen();//swagger 生成
builder.Services.AddSingleton(new Appsettings(builder.Configuration));//读取config配置
builder.Services.AddAutoMapper(typeof(MyProfile));//设置模型映射
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//获取请求信息，即访问者ip
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
#endregion


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseHttpLogging();//start the http logs
app.MapControllers();

app.Run();



using BLL;
using IBLL;
using DAL;
using IDAL;
using DBUtility;

var builder = WebApplication.CreateBuilder(args);

#region ����ע��
//��ȡappsettings����
builder.Services.AddSingleton(new Appsettings(builder.Configuration));
{
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//��ȡ������Ϣ����������ip
    builder.Services.AddAutoMapper(typeof(MyProfile));//����ģ��ӳ��
    builder.Services.AddScoped<EFSqlContext>();
    builder.Services.AddScoped<IBLLSysUser, BLLSysUser>();
    builder.Services.AddScoped<IBLLSysUser, BLLSysUserA1>();
    builder.Services.AddScoped<IDALSysUser, DALSysUser>();
    builder.Services.AddScoped<IDALSysUser, DALSysUserA1>();
}
//controller �Զ������л�
builder.Services.AddControllers();
//����
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    //���շ�
    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    //С�շ�
    //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    //����
    options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

});
builder.Services.AddEndpointsApiExplorer();//swagger ����
builder.Services.AddSwaggerGen(options => {
    var path = Path.Combine(AppContext.BaseDirectory, "API.xml");
    options.IncludeXmlComments(path, true);
    options.OrderActionsBy(s => s.RelativePath);
        
});//swagger ����


//JWT ��Ȩ
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

//HTTP ��־��¼�м��
builder.Logging.AddDebug();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Warning);


//·��ǰ׺
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
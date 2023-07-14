


using API;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

#region ����ע��
builder.Services.AddSingleton(new Appsettings(builder.Configuration));//��ȡconfig����
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

});//controller �Զ������л�
builder.Services.AddEndpointsApiExplorer();//swagger ����
builder.Services.AddSwaggerGen();//swagger ����
builder.Services.AddAutoMapper(typeof(MyProfile));//����ģ��ӳ��
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//��ȡ������Ϣ����������ip
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

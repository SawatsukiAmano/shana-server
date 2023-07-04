var builder = WebApplication.CreateBuilder(args);

#region ����ע��
builder.Services.AddControllers().AddNewtonsoftJson();//controller �Զ������л�
builder.Services.AddEndpointsApiExplorer();//swagger ����
builder.Services.AddSwaggerGen();//swagger ����
builder.Services.AddSingleton(new Appsettings(builder.Configuration));//��ȡconfig����
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

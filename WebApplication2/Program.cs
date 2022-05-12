using Autofac;
using Autofac.Extensions.DependencyInjection;
using IService;
using Re;
using Service;
using SqlSugar.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ServiceCollection se = new ServiceCollection();

se.AddScoped<IMyService, MyService>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => {
    builder.RegisterType<MyService>().As<IMyService>();  // ֱ��ע��ĳһ����ͽӿ�,��ߵ���ʵ���࣬�ұߵ�As�ǽӿ�MyRe 


    builder.RegisterType<MyRe>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SugarIocServices.AddSqlSugar(new IocConfig()
{
    //ConfigId="db01"  ���⻧�õ�
    ConnectionString = " server=localhost;Database=mysql;Uid=root;Pwd=123456;",
    DbType = IocDbType.MySql,
    IsAutoCloseConnection = true//�Զ��ͷ�
}); //�����ʹ�List<IocConfig>

//���ò���
SugarIocServices.ConfigurationSugar(db =>
{
    db.Aop.OnLogExecuting = (sql, p) =>
    {
        Console.WriteLine(sql);
    };
    //���ø������Ӳ���
    //db.CurrentConnectionConfig.XXXX=XXXX
    //db.CurrentConnectionConfig.MoreSettings=new ConnMoreSettings(){}
    //������������
    //db.CurrentConnectionConfig.ConfigureExternalServices = new ConfigureExternalServices()
    //{
    // DataInfoCacheService = myCache //�������Ǵ����Ļ�����
    //}
    //��д��������
    //laveConnectionConfigs = new List<SlaveConnectionConfig>(){...}

    /*���⻧ע��*/
    //������db.CurrentConnectionConfig 
    //���⻧��Ҫdb.GetConnection(configId).CurrentConnectionConfig 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

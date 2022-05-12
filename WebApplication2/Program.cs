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
    builder.RegisterType<MyService>().As<IMyService>();  // 直接注册某一个类和接口,左边的是实现类，右边的As是接口MyRe 


    builder.RegisterType<MyRe>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SugarIocServices.AddSqlSugar(new IocConfig()
{
    //ConfigId="db01"  多租户用到
    ConnectionString = " server=localhost;Database=mysql;Uid=root;Pwd=123456;",
    DbType = IocDbType.MySql,
    IsAutoCloseConnection = true//自动释放
}); //多个库就传List<IocConfig>

//配置参数
SugarIocServices.ConfigurationSugar(db =>
{
    db.Aop.OnLogExecuting = (sql, p) =>
    {
        Console.WriteLine(sql);
    };
    //设置更多连接参数
    //db.CurrentConnectionConfig.XXXX=XXXX
    //db.CurrentConnectionConfig.MoreSettings=new ConnMoreSettings(){}
    //二级缓存设置
    //db.CurrentConnectionConfig.ConfigureExternalServices = new ConfigureExternalServices()
    //{
    // DataInfoCacheService = myCache //配置我们创建的缓存类
    //}
    //读写分离设置
    //laveConnectionConfigs = new List<SlaveConnectionConfig>(){...}

    /*多租户注意*/
    //单库是db.CurrentConnectionConfig 
    //多租户需要db.GetConnection(configId).CurrentConnectionConfig 
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

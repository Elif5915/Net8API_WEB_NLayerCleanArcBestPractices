using App_Repositories.Extensions;
using App_Services;
using App_Services.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
options.Filters.Add<FluentValidationFilter>(); //yazdýðýmýz filterýmýzý global için ekledi.
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; //nonnullable referans tipliler için baskýla sen bunu kontrol etme demiþolduk.
    // her string deðiþkenin önüne ? koymak yerine bu özeliði global olarak kapatýp baskýlayarak sne onu kontrol etme demiþ olduk. default uyarý kapatsak ile sitring bir deðerimiz request boþ ise the is name.. kendi uayarýsýný hala veriyordu.

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

var app = builder.Build();

//eðer exception için middleware tanýmladýysanýz UseExceptionHandler yere gider return false demiþ olduðun için CriticalExceptionHandler da.
app.UseExceptionHandler(x => { }); //bunu eklemeyi unutma çünkü critical da global handlerýmýzda buna baðlý!

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

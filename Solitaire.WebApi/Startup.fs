namespace Solitaire.WebApi

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open GameService
open Solitaire.Infrastructure.Repositories
open Microsoft.OpenApi.Models
open System.Text.Json.Serialization

type Startup(configuration: IConfiguration) =
    member _.Configuration = configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member _.ConfigureServices(services: IServiceCollection) =
        // Add framework services.

        services.AddSwaggerGen(fun c ->
            let cfg = OpenApiInfo()
            cfg.Title <- "Solitaire.WebApi"
            cfg.Version <- "V1"
            c.SwaggerDoc("v1", cfg) |> ignore
        ) |> ignore
        
        let enumConverter = JsonStringEnumConverter  

        services
            .AddControllers()
            .AddJsonOptions(fun opt ->
                opt.JsonSerializerOptions.Converters.Add(JsonStringEnumConverter())
            ) |> ignore
        services.AddTransient<GameService, GameService>() |> ignore
        services.AddTransient<IGameRepository, GameRepository>() |> ignore


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member _.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
            app.UseSwagger() |> ignore
            app.UseSwaggerUI(fun c -> c.SwaggerEndpoint("/swagger/v1/swagger.json", "Solitaire.Api v1")) |> ignore
            app.UseCors(fun opt ->
                opt.WithOrigins("http://localhost:3000").AllowAnyMethod() |> ignore
            ) |> ignore

        app.UseRouting()
           .UseAuthorization()
           .UseEndpoints(fun endpoints ->
                 endpoints.MapControllers() |> ignore
             ) |> ignore

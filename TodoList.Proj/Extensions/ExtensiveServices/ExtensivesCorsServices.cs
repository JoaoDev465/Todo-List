// using System.Linq.Expressions;
//
// namespace TodoList.Proj.Extensions.ExtensiveServices;
//
// public static class ExtensivesCorsServices 
// {
//     public static void CorsServices(this WebApplicationBuilder builder)
//     {
//             builder.Services.AddCors(x => x.AddPolicy(
//                 "Allow", policyBuilder =>
//                     policyBuilder.WithOrigins("http://localhost:5245")
//                         .AllowAnyHeader().AllowAnyMethod()));
//         
//     }
// }
 using System.Net.Http.Headers;
 using System.Security.Claims;
 using System.Text.Json;
 using Microsoft.AspNetCore.Components.Authorization;
 using Microsoft.IdentityModel.JsonWebTokens;
 using Microsoft.JSInterop;
 using TodoListWeb.Handlers;

 namespace TodoListWeb.Security;

public class JwtSecurityProvider: AuthenticationStateProvider
{
  private readonly IJSRuntime _jsRuntime;
  private readonly HttpClient _httpClient;
  private const string TokenKey = "authToken";
 public JwtSecurityProvider(IJSRuntime jsRuntime, HttpClient httpClient)
 {
  _jsRuntime = jsRuntime;
  _httpClient = httpClient;
 }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync()
  {
   var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
   if (string.IsNullOrWhiteSpace(token))
   {
    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
   }
   
   var claim = ParseClaimsFromJwt(token);
   var identity = new ClaimsIdentity(claim, "jwt");
   var user = new ClaimsPrincipal(identity);

   _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
   
   return new AuthenticationState(user);
  }
  public void NotifyAuthenticationStateChanged()
   => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());


  public async Task MarkUserIsAuth(string token)
  {
   await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);

   var claim = ParseClaimsFromJwt(token);
   var identity = new ClaimsIdentity(claim, "jwt");
   var user = new ClaimsPrincipal(identity);

   _httpClient.DefaultRequestHeaders.Authorization = 
    new AuthenticationHeaderValue("Bearer", token);
   
   NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
  
  }
  public async Task MarkUserAuthLoggedOut()
  {
   await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
   _httpClient.DefaultRequestHeaders.Authorization = null;

   var user = new ClaimsPrincipal(new ClaimsIdentity());
   NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
  }

  private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
  {
   var handler = new JsonWebTokenHandler();
   var token = handler.ReadJsonWebToken(jwt);

   return token.Claims;
  }
}
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tareas.Data;
using Tareas.Models;
using Tareas.Business;

namespace Tareas.Business
{
    public class WebsiteAuthenticator : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        public WebsiteAuthenticator(ProtectedLocalStorage protectedLocalStorage)
        {
            _protectedLocalStorage = protectedLocalStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();

            try
            {
                var storedPrincipal = await _protectedLocalStorage.GetAsync<string>("identity");

                if (storedPrincipal.Success && storedPrincipal.Value is not null)
                {
                    var user = JsonConvert.DeserializeObject<UsuarioEntity>(storedPrincipal.Value);
                    var lookupResult = LookUpUser(user?.Email ?? "", user?.Password ?? "", false);

                    var userInDb = lookupResult.Item1;
                    var isLookUpSuccess = lookupResult.Item2;

                    if (isLookUpSuccess && userInDb is not null)
                    {
                        var identity = CreateIdentityFromUser(userInDb);
                        principal = new(identity);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Ocurrió un error");
            }
            return new AuthenticationState(principal);
        }

        private ClaimsIdentity CreateIdentityFromUser(UsuarioEntity user)
        {
            if (user is not null)
            {
                var result = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name,user.Nombre),
                new(ClaimTypes.Surname, user.Apellidos),
                                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Rol),
                new(ClaimTypes.NameIdentifier, user.UsuarioId.ToString())
                }, "iluminaedi");
                return result;
            }
            else
            {
                return new ClaimsIdentity();
            }
        }

        public async Task<UsuarioEntity> LoginAsync(string userName, string userPassword)
        {
            var (userInDatabase, isSuccess) = LookUpUser(userName, userPassword, true);
            var principal = new ClaimsPrincipal();

            if (isSuccess && userInDatabase is not null)
            {
                var identity = CreateIdentityFromUser(userInDatabase);
                if (identity is not null)
                {
                    principal = new ClaimsPrincipal(identity);
                    await _protectedLocalStorage.SetAsync("identity", JsonConvert.SerializeObject(userInDatabase));
                }
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
            return userInDatabase ?? new UsuarioEntity(); // Devuelve un objeto UsuarioEntity vacío si userInDatabase es nulo
        }

        public async Task LogoutAsync()
        {
            await _protectedLocalStorage.DeleteAsync("identity");
            var principal = new ClaimsPrincipal();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        private (UsuarioEntity?, bool) LookUpUser(string username, string password, bool cifrado)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Usuarios.FirstOrDefault(u => u.Email== username);

                if (result != null)
                {
                    if (cifrado)
                    {
                        bool claveMatch = CifradoClass.ChecaClave(password, result.Password);
                        if (!claveMatch)
                        {
                            result = null;
                        }
                    }
                    else
                    {
                        if (password != result.Password)
                        {
                            result = null;
                        }
                    }
                }

                return (result, result is not null);
            }
        }

    }
}


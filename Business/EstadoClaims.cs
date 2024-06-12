using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Tareas.Business
{
    public class EstadoClaims
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public EstadoClaims(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<AuthenticationData> GetAuthenticationData()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user is not null)
            {
                var userIdentity = user.Identity;
                if (userIdentity is not null && userIdentity.IsAuthenticated)
                {
                    var userName = userIdentity.Name;
                    if (userName is not null)
                    {
                        var nameIdentifierClaim = user?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
                        var roleClaim = user?.FindFirst(c => c.Type == ClaimTypes.Role);

                        var idUsuario = nameIdentifierClaim?.Value;
                        var rol = roleClaim?.Value;

                        // Verificamos que user.Claims no sea nulo antes de asignarlo
                        var claims = user?.Claims;

                        if (idUsuario is not null && rol is not null && claims is not null)
                        {
                            return new AuthenticationData
                            {
                                IsAuthenticated = true,
                                UserName = userName,
                                Claims = claims,
                                Nombre = userName,
                                IdUsuario = idUsuario,
                                Rol = rol
                            };
                        }
                    }
                }
            }

            return new AuthenticationData
            {
                IsAuthenticated = false
            };
        }
    }
}

        public class AuthenticationData
{
    public bool IsAuthenticated { get; set; }
    public string UserName { get; set; } = string.Empty;
    public IEnumerable<Claim> Claims { get; set; }
    public string IdUsuario { get; set; } = string.Empty;  
public string Nombre { get; set; } = string.Empty; 
    public string Surname { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty; 
    public string Email { get; set; } = string.Empty;

    public AuthenticationData()
    {
        Claims = new List<Claim>(); // Inicializamos Claims como una lista vacía
    }
}


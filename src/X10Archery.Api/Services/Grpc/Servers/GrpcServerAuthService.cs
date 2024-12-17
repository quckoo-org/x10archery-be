using Google.Apis.Auth;
using Grpc.Core;
using X10Archery.CustomEnums.V1;
using X10Archery.X10Auth.V1;

namespace X10Archery.Api.Services.Grpc.Servers;

public class GrpcServerAuthService(ILogger<GrpcServerAuthService> logger, IConfiguration configuration)
    : AuthenticateService.AuthenticateServiceBase
{
    private readonly ILogger<GrpcServerAuthService> _logger = logger;
    private readonly string? _googleClientId = configuration.GetValue<string>("Google:ClientId");

    public override async Task<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request, ServerCallContext context)
    {
        var response = new ValidateTokenResponse
        {
            IsValid = false,
            OperationStatus = OperationStatus.Unspecified
        };
        
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _googleClientId }
            });

            if (payload is not null)
            {
                response.IsValid = true;
                response.UserInfo = new UserInfo
                {
                    Name = payload.Name,
                    Email = payload.Email,
                    Picture = payload.Picture
                };
                response.OperationStatus = OperationStatus.Ok;
            }
        }
        catch (Exception e)
        {
            response.Error = e.Message;
            response.OperationStatus = OperationStatus.Error;
            
            _logger.LogError("An error was occured | Exception {Exception} | InnerException {InnerException}", e.Message, e.InnerException?.Message);
            
            return response;
        }
        
        return response;
    }
}
using Application.Common.Options.Auth;
using Microsoft.Extensions.Options;

namespace MinimalApi.OptionsSetup;

public class JwtOptionSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;
    
    public JwtOptionSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(JwtOptions.Jwt)
            .Bind(options);
    }
}
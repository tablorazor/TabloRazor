using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TabloRazor;

public class TabloRazorBuilder
{
    private readonly IServiceCollection services;

    public TabloRazorBuilder(IServiceCollection services) => this.services = services;

    public TabloRazorBuilder AddValidation<T>() where T : class, IFormValidator
    {
        services.Replace(ServiceDescriptor.Transient<IFormValidator, T>());
        return this;
    }
}
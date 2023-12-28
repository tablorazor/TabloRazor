using Microsoft.AspNetCore.Components.Forms;

namespace TabloRazor;

public interface IFormValidator
{
    Type Component { get; }
    Task<bool> ValidateAsync(object validatorInstance, EditContext editContext);
    bool Validate(object validatorInstance, EditContext editContext);
}
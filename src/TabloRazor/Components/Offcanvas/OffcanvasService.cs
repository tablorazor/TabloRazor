using System.Reflection;
using TabloRazor.Components.Offcanvas;

namespace TabloRazor
{
    public class OffcanvasService : IOffcanvasService
    {

        public event Action OnChanged;
        private Stack<OffcanvasModel> models = new Stack<OffcanvasModel>();
        public IEnumerable<OffcanvasModel> Models => models;

        public Task<OffcanvasResult> ShowAsync<TComponent>(string title, RenderComponent<TComponent> component, OffcanvasOptions options = null) where TComponent : IComponent
        {
            var offcanvasModel = new OffcanvasModel
            {
                Title = title,
                Contents = component.Contents,
                Options = options ?? new OffcanvasOptions()
            };
            models.Push(offcanvasModel);
            OnChanged?.Invoke();
            return offcanvasModel.Task;
        }

        public void Close()
        {

            if (models.Any())
            {
                models.Pop();
            }

            OnChanged?.Invoke();
        }

    }

}


using Wholesaler.Frontend.Presentation.Interfaces;

namespace Wholesaler.Frontend.Presentation.Views
{
    internal abstract class View : IView
    {
        protected abstract Task RenderView();

        public async Task Render()
        {
            Console.Clear();

            await RenderView();

            Console.Clear();
        }
    }
}

using Wholesaler.Frontend.Presentation.Interfaces;
using Wholesaler.Frontend.Presentation.States;

namespace Wholesaler.Frontend.Presentation.Views.Generic
{
    internal abstract class View : IView
    {
        protected readonly ApplicationState State;

        protected View(ApplicationState state)
        {
            State = state;
        }

        protected abstract Task RenderViewAsync();

        public async Task RenderAsync()
        {
            Console.Clear();

            await RenderViewAsync();

            Console.Clear();
        }
    }
}

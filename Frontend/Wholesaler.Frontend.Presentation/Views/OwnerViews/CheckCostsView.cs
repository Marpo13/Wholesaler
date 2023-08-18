using Wholesaler.Frontend.Domain.Interfaces;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.Components;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.OwnerViews
{
    internal class CheckCostsView : View
    {
        private readonly IStorageRepository _repository;
        private readonly CheckCostsState _state;

        public CheckCostsView(IStorageRepository repository, ApplicationState state) : base(state)
        {
            _repository = repository;
            _state = state.GetOwnerViews().GetCheckCostsState();
            _state.Initialize();
        }

        protected async override Task RenderViewAsync()
        {
            var getCosts = await _repository.GetCosts();

            if (!getCosts.IsSuccess)
            {
                var errorPage = new ErrorPageComponent(getCosts.Message);
                errorPage.Render();
            }

            _state.GetCosts(getCosts.Payload);

            Console.WriteLine($"Costs of mushrooms: {getCosts.Payload} zł");
            Console.ReadLine();
        }
    }
}

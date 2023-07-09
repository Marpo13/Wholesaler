using Wholesaler.Frontend.Domain.Interfaces;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.Components;
using Wholesaler.Frontend.Presentation.Views.Generic;
using Wholesaler.Frontend.Presentation.Views.ManagerViews.Components;

namespace Wholesaler.Frontend.Presentation.Views.ManagerViews
{
    internal class RequirementProgressView : View
    {
        private readonly IRequirementRepository _requirementRepository;
        private readonly RequirementProgressState _state;

        public RequirementProgressView(IRequirementRepository requirementRepository, ApplicationState state) : base(state)
        {
            _requirementRepository = requirementRepository;
            _state = state.GetManagerViews().GetRequirementProgress();
            _state.Initialize();
        }

        protected async override Task RenderViewAsync()
        {
            var ongoingRequirements = await _requirementRepository.GetOngoingRequirements();
           
            if (!ongoingRequirements.IsSuccess)
            {
                var errorPage = new ErrorPageComponent(ongoingRequirements.Message);
                errorPage.Render();
            }

            var completedRequirements = await _requirementRepository.GetCompletedRequirements();

            if (!completedRequirements.IsSuccess)
            {
                var errorPage = new ErrorPageComponent(completedRequirements.Message);
                errorPage.Render();
            }

            _state.GetOngoingRequirements(ongoingRequirements.Payload);
            _state.GetCompletedRequirements(completedRequirements.Payload);

            var displayRequirements = new DisplayRequirementsComponent(completedRequirements.Payload, ongoingRequirements.Payload);
            displayRequirements.Render();
        }
    }
}

using Wholesaler.Frontend.Domain.Interfaces;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.Components;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.ManagerViews
{
    internal class AddRequirementView : View
    {
        private readonly IRequirementRepository _requirementRepository;
        private readonly IClientRepository _clientRepository;
        private readonly AddRequirementState _state;

        public AddRequirementView(IRequirementRepository requirementRepository, IClientRepository clientRepository, ApplicationState state) : base(state)
        {
            _requirementRepository = requirementRepository;
            _clientRepository = clientRepository;
            _state = State.GetManagerViews().GetAddRequirement();
            _state.Initialize();
        }

        protected override async Task RenderViewAsync()
        {
            bool addRequirementSuccesfully = false;

            while(addRequirementSuccesfully is false)
            {
                Console.WriteLine("Quantity: ");
                var quantityInput = Console.ReadLine();
                var getClients = await _clientRepository.GetAll();

                if (!getClients.IsSuccess)
                {
                    var errorPage = new ErrorPageComponent(getClients.Message);
                    errorPage.Render();
                }

                var selectClient = new SelectClientComponent(getClients.Payload);
                var client = selectClient.Render();

                if (int.TryParse(quantityInput, out int quantity))
                {
                    var requirement = await _requirementRepository.Add(quantity, client.Id);
                    if (requirement.IsSuccess)
                    {
                        _state.GetValues(requirement.Payload.Quantity, requirement.Payload.ClientId);
                        Console.WriteLine("----------------------------");
                        Console.WriteLine($"You add requirement to a client: {requirement.Payload.ClientId} quantity: {requirement.Payload.Quantity}");
                        Console.ReadLine();
                        break;
                    }
                }
            }            
        }
    }
}

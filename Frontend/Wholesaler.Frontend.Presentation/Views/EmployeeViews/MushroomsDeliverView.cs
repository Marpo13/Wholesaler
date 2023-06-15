using Wholesaler.Frontend.Domain.Interfaces;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.Components;
using Wholesaler.Frontend.Presentation.Views.Generic;
using Wholesaler.Frontend.Presentation.Views.ManagerViews.Components;

namespace Wholesaler.Frontend.Presentation.Views.EmployeeViews
{
    internal class MushroomsDeliverView : View
    {
        private readonly IStorageRepository _storageRepository;
        private readonly IRequirementRepository _requirementRepository;
        private readonly MushroomsDeliverState _state;

        public MushroomsDeliverView(IStorageRepository storageRepository, IRequirementRepository requirementRepository, ApplicationState state) : base(state)
        {
            _storageRepository = storageRepository;
            _requirementRepository = requirementRepository;
            _state = State.GetEmployeeViews().GetMushroomsDelivery();
            _state.Initialize();
        }

        protected async override Task RenderViewAsync()
        {
            var role = State.GetLoggedInUser().Role;
            if (role != "Employee")
                throw new InvalidOperationException($"You can not deliver mushrooms with role {role}. Valid role is Employee.");

            bool deliverySuccess = false;

            while (deliverySuccess is false)
            {
                var getStorages = await _storageRepository.GetAllStorages();

                if (!getStorages.IsSuccess)
                {
                    var errorPage = new ErrorPageComponent(getStorages.Message);
                    errorPage.Render();
                }

                var selectStorage = new SelectStorageComponent(getStorages.Payload);
                var storage = selectStorage.Render();

                Console.WriteLine("Enter quantity of mushrooms you want to deliver: ");

                if (int.TryParse(Console.ReadLine(), out int quantity))
                {
                    var delivery = await _storageRepository.Deliver(storage.Id, quantity);
                    if (delivery.IsSuccess)
                    {
                        _state.GetValues(delivery.Payload.Id, quantity);
                        Console.WriteLine("----------------------------");
                        Console.WriteLine($"You delivered {quantity} mushrooms to a storage: {delivery.Payload.Id}");
                        Console.ReadLine();
                        break;
                    }
                }
                continue;
            }
        }
    }
}

using Wholesaler.Frontend.Domain.Interfaces;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.Components;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.EmployeeViews
{
    internal class MushroomsDeliveryView : View
    {
        private readonly IStorageRepository _storageRepository;
        private readonly MushroomsDeliveryState _state;

        public MushroomsDeliveryView(IStorageRepository storageRepository, ApplicationState state) : base(state)
        {
            _storageRepository = storageRepository;
            _state = State.GetEmployeeViews().GetMushroomsDelivery();
            _state.Initialize();
        }

        protected async override Task RenderViewAsync()
        {
            var role = State.GetLoggedInUser().Role;
            if(role != "Employee")
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
                
                if(int.TryParse(Console.ReadLine(), out int quantity))
                {
                    var delivery = await _storageRepository.Delivery(storage.Id, quantity);
                    if(delivery.IsSuccess)
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

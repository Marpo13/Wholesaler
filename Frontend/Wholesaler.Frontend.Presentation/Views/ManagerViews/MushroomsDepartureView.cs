using Wholesaler.Frontend.Domain.Interfaces;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.Components;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.ManagerViews
{
    internal class MushroomsDepartureView : View
    {
        private readonly IStorageRepository _storageRepository;
        private readonly MushroomsDepartureState _state;

        public MushroomsDepartureView(IStorageRepository storageRepository, ApplicationState state) : base(state)
        {
            _storageRepository = storageRepository;
            _state = State.GetManagerViews().GetMushroomsDeparture();
            _state.Initialize();
        }

        protected async override Task RenderViewAsync()
        {
            var role = State.GetLoggedInUser().Role;
            if (role != "Manager")
                throw new InvalidOperationException($"You can not deliver mushrooms with role {role}. Valid role is Manager.");

            bool departureSuccess = false;

            while (departureSuccess is false)
            {
                var getStorages = await _storageRepository.GetAllStorages();

                if (!getStorages.IsSuccess)
                {
                    var errorPage = new ErrorPageComponent(getStorages.Message);
                    errorPage.Render();
                }

                var selectStorage = new SelectStorageComponent(getStorages.Payload);
                var storage = selectStorage.Render();

                Console.WriteLine("Enter quantity of mushrooms you want to departure: ");

                if (int.TryParse(Console.ReadLine(), out int quantity))
                {
                    var departure = await _storageRepository.Departure(storage.Id, quantity);
                    if (departure.IsSuccess)
                    {
                        _state.GetValues(departure.Payload.Id, quantity);
                        Console.WriteLine("----------------------------");
                        Console.WriteLine($"You delivered {quantity} mushrooms to a storage: {departure.Payload.Id}");
                        Console.ReadLine();
                        break;
                    }
                }
                continue;
            }
        }
    }
}

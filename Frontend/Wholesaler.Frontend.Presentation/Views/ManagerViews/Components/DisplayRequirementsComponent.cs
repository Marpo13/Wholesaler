using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.ManagerViews.Components
{
    internal class DisplayRequirementsComponent : Component
    {
        private readonly List<RequirementDto> _completedRequirements;
        private readonly List<RequirementDto> _ongoingRequirements;

        public DisplayRequirementsComponent(List<RequirementDto> completedRequirements, List<RequirementDto> ongoingRequirements)
        {
            _completedRequirements = completedRequirements;
            _ongoingRequirements = ongoingRequirements;
        }

        public override void Render()
        {
            Console.WriteLine("Ongoing requirements:");

            foreach(var requirement in _ongoingRequirements)
            {
                Console.WriteLine(
                    $"\nId: {requirement.Id}," +
                    $"\nQuantity: {requirement.Quantity}," +
                    $"\nClientId: {requirement.ClientId}," +
                    $"\nStorageId: {requirement.StorageId}," +
                    $"\nStatus: {requirement.Status}");
            }

            Console.WriteLine("\nCompleted requirements:");

            foreach (var requirement in _completedRequirements)
            {
                Console.WriteLine(
                    $"\nId: {requirement.Id}," +
                    $"\nQuantity: {requirement.Quantity}," +
                    $"\nClientId: {requirement.ClientId}," +
                    $"\nStorageId: {requirement.StorageId}," +
                    $"\nStatus: {requirement.Status}" +
                    $"\nDeliveryDate: {requirement.DeliveryDate}");
            }

            Console.ReadLine();
        }
    }
}

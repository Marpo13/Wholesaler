﻿using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.ManagerViews.Components
{
    internal class SelectRequirementComponent : Component<RequirementDto>
    {
        private readonly List<RequirementDto> _requirements;

        public SelectRequirementComponent(List<RequirementDto> requirements)
        {
            _requirements = requirements;
        }

        public override RequirementDto Render()
        {
            bool wasCorrectValueProvided = false;
            RequirementDto? requirementDto = null;

            while (wasCorrectValueProvided is false)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("Requirements:");

                foreach (var requirement in _requirements)
                    Console.WriteLine($"{_requirements.IndexOf(requirement) + 1}: {requirement.Id}");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Enter an id of a requirement you want to choose: ");
                if (!int.TryParse(Console.ReadLine(), out int requirementNumber))
                {
                    Console.WriteLine("You entered an invalid value.");
                    continue;
                }

                var index = requirementNumber - 1;
                requirementDto = _requirements
                    .Where(x => _requirements.IndexOf(x) == index)
                    .FirstOrDefault();

                if (requirementDto == null)
                {
                    Console.WriteLine("You entered an invalid value.");
                    continue;
                }

                wasCorrectValueProvided = true;
            }

            return requirementDto;
        }    
    }
}

using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.ManagerViews.Components
{
    internal class SelectStatusComponent : Component<string>
    {
        public override string Render()
        {
            var wasCorrectValueProvided = false;
            var status = "";

            while (wasCorrectValueProvided is false)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("Enter an index of a status of requirement you want to choose:" +
                    "\n1) Ongoing" +
                    "\n2) Completed");
                if (!int.TryParse(Console.ReadLine(), out int statusNumber))
                {
                    Console.WriteLine("You entered an invalid value.");
                    continue;
                }

                switch (statusNumber) 
                {
                    case 1:
                        status = "ongoing";
                        break;

                    case 2:
                        status = "completed";
                        break;
                }

                wasCorrectValueProvided = true;
            }

            return status;
        }
    }
}

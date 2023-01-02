using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Frontend.Presentation.Views
{
    internal abstract class View
    {
        protected abstract Task RenderView();

        public async Task Render()
        {
            Console.Clear();

            await RenderView();

            Console.Clear();
        }
    }
}

using IconGenerator.MaterialDesign;
using IconGenerator.Tabler;
using System.Threading.Tasks;

namespace IconGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await TablerGenerator.GenerateFlags();
            await TablerGenerator.GenerateIcons();
            await MaterialDesignGenerator.GenerateIcons();
        }


    }
}

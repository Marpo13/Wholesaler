using Wholesaler.Backend.Domain.Exceptions;

namespace Wholesaler.Backend.Domain.Requests.Delivery
{
    public class GetCostsRequest
    {
        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }

        public GetCostsRequest(int? day, int? month, int? year)
        {
            if (day.HasValue && month.HasValue && year.HasValue)
            {
                Day = day;
                Month = month;
                Year = year;
            }

            else if (!day.HasValue && month.HasValue && year.HasValue)
            {
                Day = null;
                Month = month; 
                Year = year;
            }

            else if (!day.HasValue && !month.HasValue && year.HasValue)
            {
                Day = null;
                Month = null;
                Year = year;
            }

            else if (!day.HasValue && !month.HasValue && !year.HasValue)
            {
                Day = null;
                Month = null;
                Year = null;
            }

            else
            {
                throw new InvalidDataProvidedException("You provided wrong values.");
            }
        }
    }
}

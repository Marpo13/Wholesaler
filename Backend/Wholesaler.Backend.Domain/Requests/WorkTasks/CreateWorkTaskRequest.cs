namespace Wholesaler.Backend.Domain.Requests.WorkTasks
{
    public class CreateWorkTaskRequest
    {
        public int Row { get; }

        public CreateWorkTaskRequest(int row)
        {
            Row = row;
        }
    }
}

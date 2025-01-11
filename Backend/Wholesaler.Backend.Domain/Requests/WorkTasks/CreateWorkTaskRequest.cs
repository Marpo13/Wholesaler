namespace Wholesaler.Backend.Domain.Requests.WorkTasks;

public class CreateWorkTaskRequest
{
    public CreateWorkTaskRequest(int row)
    {
        Row = row;
    }

    public int Row { get; }
}

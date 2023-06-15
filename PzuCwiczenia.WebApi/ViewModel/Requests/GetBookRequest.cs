namespace PzuCwiczenia.WebApi.ViewModel.Requests;

public class GetBookRequest
{
    public string Title { get; set; }

    public int MinimumPages { get; set; }


    public int PageNumber { get; set; }

    public int PageCount { get; set; }
}

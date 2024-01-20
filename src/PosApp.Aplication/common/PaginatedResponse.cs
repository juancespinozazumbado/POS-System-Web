namespace PosApp.Aplication.Common;

public sealed class PaginateList<TResponse>
{
 
  public PaginateList(IEnumerable<TResponse> items, int pageZise, int currentPage, int totalItems)
  {
    this.PageZise = pageZise;
    this.CurrentPage = currentPage;
    this.TotalItems = totalItems;
    this.Items = items.ToList();
  }

  public int PageZise {get; }
  public int TotalItems {get; }
  public int CurrentPage {get; }

  public bool HasNextPage => PageZise * CurrentPage < TotalItems;
  public bool HastPreviusPage => CurrentPage > 1;

  public IReadOnlyCollection<TResponse>? Items {get; private set;}

}
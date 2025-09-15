namespace SharedKernel.Domain.Criteria;

public sealed record Pagination
{
    public int Size { get; init; }
    public int Page { get; init; }

    public Pagination(int Size, int Page)
    {
        EnsureSizeIsGreaterThanZero(Size);
        EnsurePageIsGreaterThanZero(Page);
        this.Size = Size;
        this.Page = Page;
    }

    private void EnsureSizeIsGreaterThanZero(int size)
    {
        if (size <= 0)
        {
            throw new ArgumentException("Size must be greater than 0");
        }
    }

    private void EnsurePageIsGreaterThanZero(int page)
    {
        if (page <= 0)
        {
            throw new ArgumentException("Page must be greater than 0");
        }
    }
}
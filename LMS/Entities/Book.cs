using System;
using System.Collections.Generic;

namespace LibraryManager.Entities;

public partial class Book
{
    public int? Id { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public int? Isbn { get; set; }

    public DateTime? Date { get; set; }

    public int? RackId { get; set; }
}

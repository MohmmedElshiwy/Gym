using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public abstract class BaseEntity
{
	public  int Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
}

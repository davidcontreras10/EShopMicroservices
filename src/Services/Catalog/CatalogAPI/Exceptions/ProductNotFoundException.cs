﻿namespace CatalogAPI.Exceptions
{
	public class ProductNotFoundException(Guid id) : Exception($"Product with id {id} not found")
	{
	}
}

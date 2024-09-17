using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using BookStore.Models;
public static class SessionHelper
{
	private const string CartSessionKey = "_Cart";

	public static void SetCartItems(HttpContext context, List<OrderDetail> cartItems)
	{
		context.Session.SetString(CartSessionKey, JsonConvert.SerializeObject(cartItems));
	}

	public static List<OrderDetail> GetCartItems(HttpContext context)
	{
		var sessionData = context.Session.GetString(CartSessionKey);
		return sessionData == null ? new List<OrderDetail>() : JsonConvert.DeserializeObject<List<OrderDetail>>(sessionData);
	}

	public static int GetCartItemCount(HttpContext context)
	{
		return GetCartItems(context).Count;
	}

	public static void ClearCart(HttpContext context)
	{
		context.Session.Remove(CartSessionKey);
	}
}

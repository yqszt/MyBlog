using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


/// <summary>
/// Handler 的摘要说明
/// </summary>
public abstract class Handler
{
	public Handler(HttpContext context)
	{
        this.Request = context.Request;
        this.Response = context.Response;
        this.Context = context;
        //this.Server = context;
	}

    public abstract Task Process();

    protected async Task WriteJson(object response)
    {
        string jsonpCallback = Request.Query["callback"],
            json = JsonConvert.SerializeObject(response);
        if (String.IsNullOrWhiteSpace(jsonpCallback))
        {
            Response.Headers.Add("Content-Type", "application/json");
            await Response.WriteAsync(json);
        }
        else 
        {
            Response.Headers.Add("Content-Type", "application/javascript");
            await Response.WriteAsync(String.Format("{0}({1});", jsonpCallback, json));
        }
        //Response.End();
    }

    public HttpRequest Request { get; private set; }
    public HttpResponse Response { get; private set; }
    public HttpContext Context { get; private set; }
   // public HttpServerUtility Server { get; private set; }
}
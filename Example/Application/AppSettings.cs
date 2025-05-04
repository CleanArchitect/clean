using Clean.Net;
using System.ComponentModel.DataAnnotations;

namespace Example.Application;

internal sealed class AppSettings : Settings
{
    [Required]
    public string ConnectionString { get; private set; }
}